using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace it
{
    [DefaultEvent("ClipboardChanged")]
    public class ClipboardMonitor : Control
    {
        private IntPtr nextClipboardViewer;

        public ClipboardMonitor()
        {
            BackColor = Color.Red;
            Visible = false;

            nextClipboardViewer = (IntPtr) SetClipboardViewer((int) Handle);
        }

        /// <summary>
        ///     Clipboard contents changed.
        /// </summary>
        public event EventHandler<ClipboardChangedEventArgs> ClipboardChanged;

        protected override void Dispose(bool disposing)
        {
            try
            {
                ChangeClipboardChain(Handle, nextClipboardViewer);
            }
            catch (Exception)
            {
            }
        }

        [DllImport("User32.dll")]
        private static extern int SetClipboardViewer(int hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        protected override void WndProc(ref Message m)
        {
            // defined in winuser.h
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    OnClipboardChanged();
                    SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                case WM_CHANGECBCHAIN:
                    if (m.WParam == nextClipboardViewer)
                        nextClipboardViewer = m.LParam;
                    else
                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void OnClipboardChanged()
        {
            try
            {
                var iData = Clipboard.GetDataObject();
                ClipboardChanged?.Invoke(this, new ClipboardChangedEventArgs(iData));
            }
            catch (Exception e)
            {
                // Swallow or pop-up, not sure
                // Trace.Write(e.ToString());
                MessageBox.Show(e.ToString());
            }
        }
    }

    public class ClipboardChangedEventArgs : EventArgs
    {
        public readonly IDataObject DataObject;

        public ClipboardChangedEventArgs(IDataObject dataObject)
        {
            DataObject = dataObject;
        }
    }
}