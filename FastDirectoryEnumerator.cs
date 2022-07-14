using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;

/// <summary>
/// A fast enumerator of files in a directory. Use this if you need to get attributes for all files
/// in a directory.
/// </summary>
public static class FastDirectoryEnumerator
{
    /// <summary>
    /// Gets <see cref="FileData" /> for all the files in a directory.
    /// </summary>
    /// <param name="path">
    /// The path to search.
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable{FileData}" />
    /// </returns>
    public static IEnumerable<FileData> EnumerateFiles(string path)
    {
        return FastDirectoryEnumerator.EnumerateFiles(path, "*");
    }

    /// <summary>
    /// Gets <see cref="FileData" /> for all the files in a directory that match a specific filter.
    /// </summary>
    /// <param name="path">
    /// The path to search.
    /// </param>
    /// <param name="searchPattern">
    /// The search string to match against files in the path.
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable{FileData}" />
    /// </returns>
    public static IEnumerable<FileData> EnumerateFiles(string path, string searchPattern)
    {
        return FastDirectoryEnumerator.EnumerateFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    /// Gets <see cref="FileData" /> for all the files in a directory that match a specific filter,
    /// optionally including all sub directories.
    /// </summary>
    /// <param name="path">
    /// The path to search.
    /// </param>
    /// <param name="searchPattern">
    /// The search string to match against files in the path.
    /// </param>
    /// <param name="searchOption">
    /// The searchOption <see cref="SearchOption" />
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable{FileData}" />
    /// </returns>
    public static IEnumerable<FileData> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
    {
        if (path == null)
        {
            throw new ArgumentNullException("path");
        }
        if (searchPattern == null)
        {
            throw new ArgumentNullException("searchPattern");
        }
        if ((searchOption != SearchOption.TopDirectoryOnly) && (searchOption != SearchOption.AllDirectories))
        {
            throw new ArgumentOutOfRangeException("searchOption");
        }

        string fullPath = Path.GetFullPath(path);

        return new FileEnumerable(fullPath, searchPattern, searchOption);
    }

    /// <summary>
    /// Gets <see cref="FileData" /> for all the files in a directory that match a specific filter.
    /// </summary>
    /// <param name="path">
    /// The path to search.
    /// </param>
    /// <param name="searchPattern">
    /// The search string to match against files in the path.
    /// </param>
    /// <param name="searchOption">
    /// The searchOption <see cref="SearchOption" />
    /// </param>
    /// <returns>
    /// The <see cref="FileData[]" />
    /// </returns>
    public static FileData[] GetFiles(string path, string searchPattern, SearchOption searchOption)
    {
        IEnumerable<FileData> e = FastDirectoryEnumerator.EnumerateFiles(path, searchPattern, searchOption);
        List<FileData> list = new List<FileData>(e);

        FileData[] retval = new FileData[list.Count];
        list.CopyTo(retval);

        return retval;
    }

    /// <summary>
    /// Provides the implementation of the <see cref="T:System.Collections.Generic.IEnumerable`1" /> interface
    /// </summary>
    private class FileEnumerable : IEnumerable<FileData>
    {
        /// <summary>
        /// Defines the m_filter
        /// </summary>
        private readonly string m_filter;

        /// <summary>
        /// Defines the m_path
        /// </summary>
        private readonly string m_path;

        /// <summary>
        /// Defines the m_searchOption
        /// </summary>
        private readonly SearchOption m_searchOption;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileEnumerable" /> class.
        /// </summary>
        /// <param name="path">
        /// The path to search.
        /// </param>
        /// <param name="filter">
        /// The search string to match against files in the path.
        /// </param>
        /// <param name="searchOption">
        /// The searchOption <see cref="SearchOption" />
        /// </param>
        public FileEnumerable(string path, string filter, SearchOption searchOption)
        {
            m_path = path;
            m_filter = filter;
            m_searchOption = searchOption;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator{FileData}" />
        /// </returns>
        public IEnumerator<FileData> GetEnumerator()
        {
            return new FileEnumerator(m_path, m_filter, m_searchOption);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// The <see cref="System.Collections.IEnumerator" />
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new FileEnumerator(m_path, m_filter, m_searchOption);
        }
    }

    /// <summary>
    /// Provides the implementation of the <see cref="T:System.Collections.Generic.IEnumerator`1" /> interface
    /// </summary>
    [System.Security.SuppressUnmanagedCodeSecurity]
    private class FileEnumerator : IEnumerator<FileData>
    {
        /// <summary>
        /// Defines the m_contextStack
        /// </summary>
        private Stack<SearchContext> m_contextStack;

        /// <summary>
        /// Defines the m_currentContext
        /// </summary>
        private SearchContext m_currentContext;

        /// <summary>
        /// Defines the m_filter
        /// </summary>
        private string m_filter;

        /// <summary>
        /// Defines the m_hndFindFile
        /// </summary>
        private SafeFindHandle m_hndFindFile;

        /// <summary>
        /// Defines the m_path
        /// </summary>
        private string m_path;

        /// <summary>
        /// Defines the m_searchOption
        /// </summary>
        private SearchOption m_searchOption;

        /// <summary>
        /// Defines the m_win_find_data
        /// </summary>
        private WIN32_FIND_DATA m_win_find_data = new WIN32_FIND_DATA();

        /// <summary>
        /// Initializes a new instance of the <see cref="FileEnumerator" /> class.
        /// </summary>
        /// <param name="path">
        /// The path to search.
        /// </param>
        /// <param name="filter">
        /// The search string to match against files in the path.
        /// </param>
        /// <param name="searchOption">
        /// The searchOption <see cref="SearchOption" />
        /// </param>
        public FileEnumerator(string path, string filter, SearchOption searchOption)
        {
            m_path = path;
            m_filter = filter;
            m_searchOption = searchOption;
            m_currentContext = new SearchContext(path);

            if (m_searchOption == SearchOption.AllDirectories)
            {
                m_contextStack = new Stack<SearchContext>();
            }
        }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public FileData Current
        {
            get { return new FileData(m_path, m_win_find_data); }
        }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        object System.Collections.IEnumerator.Current
        {
            get { return new FileData(m_path, m_win_find_data); }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (m_hndFindFile != null)
            {
                m_hndFindFile.Dispose();
            }
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// The <see cref="bool" />
        /// </returns>
        public bool MoveNext()
        {
            bool retval = false;

            //If the handle is null, this is first call to MoveNext in the current
            // directory.  In that case, start a new search.
            if (m_currentContext.SubdirectoriesToProcess == null)
            {
                if (m_hndFindFile == null)
                {
                    new FileIOPermission(FileIOPermissionAccess.PathDiscovery, m_path).Demand();

                    string searchPath = Path.Combine(m_path, m_filter);
                    m_hndFindFile = FindFirstFile(searchPath, m_win_find_data);
                    retval = !m_hndFindFile.IsInvalid;
                }
                else
                {
                    //Otherwise, find the next item.
                    retval = FindNextFile(m_hndFindFile, m_win_find_data);
                }
            }

            //If the call to FindNextFile or FindFirstFile succeeded...
            if (retval)
            {
                if (((FileAttributes)m_win_find_data.dwFileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    //Ignore folders for now.   We call MoveNext recursively here to
                    // move to the next item that FindNextFile will return.
                    return MoveNext();
                }
            }
            else if (m_searchOption == SearchOption.AllDirectories)
            {
                //SearchContext context = new SearchContext(m_hndFindFile, m_path);
                //m_contextStack.Push(context);
                //m_path = Path.Combine(m_path, m_win_find_data.cFileName);
                //m_hndFindFile = null;

                if (m_currentContext.SubdirectoriesToProcess == null)
                {
                    string[] subDirectories = Directory.GetDirectories(m_path);
                    m_currentContext.SubdirectoriesToProcess = new Stack<string>(subDirectories);
                }

                if (m_currentContext.SubdirectoriesToProcess.Count > 0)
                {
                    string subDir = m_currentContext.SubdirectoriesToProcess.Pop();

                    m_contextStack.Push(m_currentContext);
                    m_path = subDir;
                    m_hndFindFile = null;
                    m_currentContext = new SearchContext(m_path);
                    return MoveNext();
                }

                //If there are no more files in this directory and we are
                // in a sub directory, pop back up to the parent directory and
                // continue the search from there.
                if (m_contextStack.Count > 0)
                {
                    m_currentContext = m_contextStack.Pop();
                    m_path = m_currentContext.Path;
                    if (m_hndFindFile != null)
                    {
                        m_hndFindFile.Close();
                        m_hndFindFile = null;
                    }

                    return MoveNext();
                }
            }

            return retval;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset()
        {
            m_hndFindFile = null;
        }

        /// <summary>
        /// The FindFirstFile
        /// </summary>
        /// <param name="fileName">
        /// The fileName <see cref="string" />
        /// </param>
        /// <param name="data">
        /// The data <see cref="WIN32_FIND_DATA" />
        /// </param>
        /// <returns>
        /// The <see cref="SafeFindHandle" />
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern SafeFindHandle FindFirstFile(string fileName,
                [In, Out] WIN32_FIND_DATA data);

        /// <summary>
        /// The FindNextFile
        /// </summary>
        /// <param name="hndFindFile">
        /// The hndFindFile <see cref="SafeFindHandle" />
        /// </param>
        /// <param name="lpFindFileData">
        /// The lpFindFileData <see cref="WIN32_FIND_DATA" />
        /// </param>
        /// <returns>
        /// The <see cref="bool" />
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool FindNextFile(SafeFindHandle hndFindFile,
                    [In, Out, MarshalAs(UnmanagedType.LPStruct)] WIN32_FIND_DATA lpFindFileData);

        /// <summary>
        /// Hold context information about where we current are in the directory search.
        /// </summary>
        private class SearchContext
        {
            /// <summary>
            /// Defines the Path
            /// </summary>
            public readonly string Path;

            /// <summary>
            /// Defines the SubdirectoriesToProcess
            /// </summary>
            public Stack<string> SubdirectoriesToProcess;

            /// <summary>
            /// Initializes a new instance of the <see cref="SearchContext" /> class.
            /// </summary>
            /// <param name="path">
            /// The path <see cref="string" />
            /// </param>
            public SearchContext(string path)
            {
                this.Path = path;
            }
        }
    }

    /// <summary>
    /// Wraps a FindFirstFile handle.
    /// </summary>
    private sealed class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeFindHandle" /> class.
        /// </summary>
        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        internal SafeFindHandle()
                : base(true)
        {
        }

        /// <summary>
        /// When overridden in a derived class, executes the code required to free the handle.
        /// </summary>
        /// <returns>
        /// The <see cref="bool" />
        /// </returns>
        protected override bool ReleaseHandle()
        {
            return FindClose(base.handle);
        }

        /// <summary>
        /// The FindClose
        /// </summary>
        /// <param name="handle">
        /// The handle <see cref="IntPtr" />
        /// </param>
        /// <returns>
        /// The <see cref="bool" />
        /// </returns>
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("kernel32.dll")]
        private static extern bool FindClose(IntPtr handle);
    }
}

/// <summary>
/// Contains information about a file returned by the <see cref="FastDirectoryEnumerator" /> class.
/// </summary>
[Serializable]
public class FileData
{
    /// <summary>
    /// Attributes of the file.
    /// </summary>
    public readonly FileAttributes Attributes;

    /// <summary>
    /// File creation time in UTC
    /// </summary>
    public readonly DateTime CreationTimeUtc;

    /// <summary>
    /// File last access time in UTC
    /// </summary>
    public readonly DateTime LastAccessTimeUtc;

    /// <summary>
    /// File last write time in UTC
    /// </summary>
    public readonly DateTime LastWriteTimeUtc;

    /// <summary>
    /// Name of the file
    /// </summary>
    public readonly string Name;

    /// <summary>
    /// Full path to the file.
    /// </summary>
    public readonly string Path;

    /// <summary>
    /// Size of the file in bytes
    /// </summary>
    public readonly long Size;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileData" /> class.
    /// </summary>
    /// <param name="dir">
    /// The directory that the file is stored at
    /// </param>
    /// <param name="findData">
    /// The findData <see cref="WIN32_FIND_DATA" />
    /// </param>
    internal FileData(string dir, WIN32_FIND_DATA findData)
    {
        this.Attributes = findData.dwFileAttributes;

        this.CreationTimeUtc = ConvertDateTime(findData.ftCreationTime_dwHighDateTime,
                                            findData.ftCreationTime_dwLowDateTime);

        this.LastAccessTimeUtc = ConvertDateTime(findData.ftLastAccessTime_dwHighDateTime,
                                            findData.ftLastAccessTime_dwLowDateTime);

        this.LastWriteTimeUtc = ConvertDateTime(findData.ftLastWriteTime_dwHighDateTime,
                                            findData.ftLastWriteTime_dwLowDateTime);

        this.Size = CombineHighLowInts(findData.nFileSizeHigh, findData.nFileSizeLow);

        this.Name = findData.cFileName;
        this.Path = System.IO.Path.Combine(dir, findData.cFileName);
    }

    /// <summary>
    /// Gets the CreationTime
    /// </summary>
    public DateTime CreationTime
    {
        get { return this.CreationTimeUtc.ToLocalTime(); }
    }

    /// <summary>
    /// Gets the last access time in local time.
    /// </summary>
    public DateTime LastAccesTime
    {
        get { return this.LastAccessTimeUtc.ToLocalTime(); }
    }

    /// <summary>
    /// Gets the last access time in local time.
    /// </summary>
    public DateTime LastWriteTime
    {
        get { return this.LastWriteTimeUtc.ToLocalTime(); }
    }

    public static implicit operator string(FileData v)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </summary>
    /// <returns>
    /// The <see cref="string" />
    /// </returns>
    public override string ToString()
    {
        return this.Name;
    }

    /// <summary>
    /// The CombineHighLowInts
    /// </summary>
    /// <param name="high">
    /// The high <see cref="uint" />
    /// </param>
    /// <param name="low">
    /// The low <see cref="uint" />
    /// </param>
    /// <returns>
    /// The <see cref="long" />
    /// </returns>
    private static long CombineHighLowInts(uint high, uint low)
    {
        return (((long)high) << 0x20) | low;
    }

    /// <summary>
    /// The ConvertDateTime
    /// </summary>
    /// <param name="high">
    /// The high <see cref="uint" />
    /// </param>
    /// <param name="low">
    /// The low <see cref="uint" />
    /// </param>
    /// <returns>
    /// The <see cref="DateTime" />
    /// </returns>
    private static DateTime ConvertDateTime(uint high, uint low)
    {
        long fileTime = CombineHighLowInts(high, low);
        return DateTime.FromFileTimeUtc(fileTime);
    }
}

/// <summary>
/// Contains information about the file that is found by the FindFirstFile or FindNextFile functions.
/// </summary>
[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto), BestFitMapping(false)]
internal class WIN32_FIND_DATA
{
    /// <summary>
    /// Defines the cAlternateFileName
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
    public string cAlternateFileName;

    /// <summary>
    /// Defines the cFileName
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
    public string cFileName;

    /// <summary>
    /// Defines the dwFileAttributes
    /// </summary>
    public FileAttributes dwFileAttributes;

    /// <summary>
    /// Defines the dwReserved0
    /// </summary>
    public int dwReserved0;

    /// <summary>
    /// Defines the dwReserved1
    /// </summary>
    public int dwReserved1;

    /// <summary>
    /// Defines the ftCreationTime_dwHighDateTime
    /// </summary>
    public uint ftCreationTime_dwHighDateTime;

    /// <summary>
    /// Defines the ftCreationTime_dwLowDateTime
    /// </summary>
    public uint ftCreationTime_dwLowDateTime;

    /// <summary>
    /// Defines the ftLastAccessTime_dwHighDateTime
    /// </summary>
    public uint ftLastAccessTime_dwHighDateTime;

    /// <summary>
    /// Defines the ftLastAccessTime_dwLowDateTime
    /// </summary>
    public uint ftLastAccessTime_dwLowDateTime;

    /// <summary>
    /// Defines the ftLastWriteTime_dwHighDateTime
    /// </summary>
    public uint ftLastWriteTime_dwHighDateTime;

    /// <summary>
    /// Defines the ftLastWriteTime_dwLowDateTime
    /// </summary>
    public uint ftLastWriteTime_dwLowDateTime;

    /// <summary>
    /// Defines the nFileSizeHigh
    /// </summary>
    public uint nFileSizeHigh;

    /// <summary>
    /// Defines the nFileSizeLow
    /// </summary>
    public uint nFileSizeLow;

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </summary>
    /// <returns>
    /// The <see cref="string" />
    /// </returns>
    public override string ToString()
    {
        return "File name=" + cFileName;
    }
}
