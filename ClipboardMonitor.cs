namespace it
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [DefaultEvent(nameof(ClipboardChanged))]
    public sealed class ClipboardMonitor : Control
    {
        private IntPtr nextClipboardViewer;

        public ClipboardMonitor()
        {
            this.BackColor = Color.Red;
            this.Visible = false;

            this.nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
        }

        /// <summary>
        ///     Clipboard contents changed.
        /// </summary>
        public event EventHandler<ClipboardChangedEventArgs> ClipboardChanged;

        protected override void Dispose(bool disposing)
        {
            try
            {
                ChangeClipboardChain(this.Handle, this.nextClipboardViewer);
            }
            catch (Exception)
            {
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
                        this.OnClipboardChanged();
                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                        break;
                    }
                case WM_CHANGECBCHAIN:
                    {
                        if (m.WParam == this.nextClipboardViewer)
                        {
                            this.nextClipboardViewer = m.LParam;
                        }
                        else
                            SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
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
        private static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        private static extern int SetClipboardViewer(int hWndNewViewer);

        private void OnClipboardChanged()
        {
            try
            {
                var iData = Clipboard.GetDataObject();
                this.ClipboardChanged?.Invoke(this, new ClipboardChangedEventArgs(iData));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }

    public sealed class ClipboardChangedEventArgs : EventArgs
    {
        internal readonly IDataObject DataObject;

        public ClipboardChangedEventArgs(IDataObject dataObject)
        {
            if (dataObject is null)
            {
                throw new ArgumentNullException(nameof(dataObject));
            }

            this.DataObject = dataObject;
        }
    }
}