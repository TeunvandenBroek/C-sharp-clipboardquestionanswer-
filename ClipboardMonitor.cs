namespace it
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [DefaultEvent(nameof(ClipboardChanged))]
    public sealed class ClipboardMonitor : Control, IEquatable<ClipboardMonitor>
    {
        private IntPtr nextClipboardViewer;

        public ClipboardMonitor()
        {
            BackColor = Color.Red;
            Visible = false;

            nextClipboardViewer = (IntPtr)SetClipboardViewer((int)Handle);
        }

        /// <summary>
        ///     Clipboard contents changed.
        /// </summary>
        public event EventHandler<ClipboardChangedEventArgs> ClipboardChanged;

        protected override void Dispose(bool disposing)
        {
            try
            {
                _ = ChangeClipboardChain(Handle, nextClipboardViewer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            base.Dispose(disposing);
        }

        protected override void WndProc(ref Message m)
        {
            // defined in winuser.h
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    {
                        OnClipboardChanged();
                        _ = SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        break;
                    }
                case WM_CHANGECBCHAIN:
                    {
                        if (m.WParam == nextClipboardViewer)
                        {
                            nextClipboardViewer = m.LParam;
                        }
                        else
                        {
                            _ = SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }

                        break;
                    }
                default:
                    {
                        base.WndProc(ref m);
                        break;
                    }
            }
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        private static extern int SetClipboardViewer(int hWndNewViewer);

        private void OnClipboardChanged()
        {
            try
            {
                IDataObject iData = Clipboard.GetDataObject();
                ClipboardChanged?.Invoke(this, new ClipboardChangedEventArgs(iData));
            }
            catch (Exception e)
            {
                _ = MessageBox.Show(e.ToString());
            }
        }

        public bool Equals(ClipboardMonitor other)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class ClipboardChangedEventArgs : EventArgs
    {
        internal readonly IDataObject DataObject;

        internal ClipboardChangedEventArgs(IDataObject dataObject = null)
        {
            if (dataObject is null)
            {
                throw new ArgumentNullException(nameof(dataObject));
            }

            DataObject = dataObject;
        }
    }
}