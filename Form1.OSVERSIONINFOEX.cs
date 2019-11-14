namespace it
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Defines the <see cref="Form1" />
    /// </summary>
    public partial class Form1
    {
        /// <summary>
        /// Defines the <see cref="Osversioninfoex" />
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Osversioninfoex : System.IEquatable<Osversioninfoex>
        {
            /// <summary>
            /// Defines the OSVersionInfoSize
            /// </summary>
            internal int OSVersionInfoSize;

            /// <summary>
            /// Defines the MajorVersion
            /// </summary>
            internal int MajorVersion;

            /// <summary>
            /// Defines the MinorVersion
            /// </summary>
            internal int MinorVersion;

            /// <summary>
            /// Defines the BuildNumber
            /// </summary>
            internal int BuildNumber;

            /// <summary>
            /// Defines the PlatformId
            /// </summary>
            internal int PlatformId;

            /// <summary>
            /// Defines the CSDVersion
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            internal readonly string CSDVersion;

            /// <summary>
            /// Defines the ServicePackMajor
            /// </summary>
            internal ushort ServicePackMajor;

            /// <summary>
            /// Defines the ServicePackMinor
            /// </summary>
            internal ushort ServicePackMinor;

            /// <summary>
            /// Defines the SuiteMask
            /// </summary>
            internal short SuiteMask;

            /// <summary>
            /// Defines the ProductType
            /// </summary>
            internal byte ProductType;

            /// <summary>
            /// Defines the Reserved
            /// </summary>
            internal byte Reserved;

            /// <summary>
            /// The Equals
            /// </summary>
            /// <param name="other">The other<see cref="Osversioninfoex"/></param>
            /// <returns>The <see cref="bool"/></returns>
            public bool Equals(Osversioninfoex other)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
