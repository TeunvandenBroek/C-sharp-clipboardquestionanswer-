using System.Runtime.InteropServices;

namespace it
{
    public partial class Form1
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Osversioninfoex : System.IEquatable<Osversioninfoex>
        {
            internal int OSVersionInfoSize;
            internal int MajorVersion;
            internal int MinorVersion;
            internal int BuildNumber;
            internal int PlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            internal readonly string CSDVersion;
            internal ushort ServicePackMajor;
            internal ushort ServicePackMinor;
            internal short SuiteMask;
            internal byte ProductType;
            internal byte Reserved;

            public bool Equals(Osversioninfoex other)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
