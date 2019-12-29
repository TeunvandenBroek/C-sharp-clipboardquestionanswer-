namespace it
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [DefaultEvent("ClipboardChanged")]
    internal sealed class ClipboardMonitor : Control
    {
        public ClipboardMonitor()
        {
            CreateHandle();
            try
            {
                NextViewerPtr = NativeMethods.SetClipboardViewer(Handle);
            }
            catch (EntryPointNotFoundException) { }
        }

        ~ClipboardMonitor()
        {
            Dispose(disposing: false);
        }

        private IntPtr NextViewerPtr;

        public event EventHandler<ClipboardChangedEventArgs> ClipboardChanged;


        protected override void Dispose(bool disposing)
        {
            if (NextViewerPtr != IntPtr.Zero)
            {
                _ = NativeMethods.ChangeClipboardChain(Handle, NextViewerPtr);
                NextViewerPtr = IntPtr.Zero;
            }

            base.Dispose(disposing);
        }


        protected override void WndProc(ref Message m)
        {
            {
                switch (m.Msg)
                {
                    case NativeMethods.WM_DRAWCLIPBOARD:
                        _ = NativeMethods.SendMessage(NextViewerPtr, m.Msg, m.WParam, m.LParam);
                        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                        GC.WaitForPendingFinalizers();
                        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                        OnClipboardChanged();
                        break;

                    case NativeMethods.WM_CHANGECBCHAIN:
                        if (m.WParam == NextViewerPtr)
                        {
                            NextViewerPtr = m.LParam;
                        }
                        else
                        {
                            _ = NativeMethods.SendMessage(NextViewerPtr, m.Msg, m.WParam, m.LParam);
                        }
                        break;

                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
        }


        private void OnClipboardChanged()
        {
            try
            {
                IDataObject dataObject = Clipboard.GetDataObject();
                if (dataObject != null)
                {
                    ClipboardChanged?.Invoke(this, new ClipboardChangedEventArgs(dataObject));
                }
            }
            catch (ExternalException) { }
        }



        private static class NativeMethods
        {

            internal const int WM_DRAWCLIPBOARD = 0x0308;
            internal const int WM_CHANGECBCHAIN = 0x030D;


            [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

            [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

            [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);


        }
    }


    internal sealed class ClipboardChangedEventArgs : EventArgs
    {
        public ClipboardChangedEventArgs(IDataObject dataObject)
        {
            DataObject = dataObject;
        }

        public IDataObject DataObject { get; }
    }
}