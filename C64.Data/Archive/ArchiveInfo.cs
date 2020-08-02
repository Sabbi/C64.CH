using System.Collections.Generic;

namespace C64.Data.Archive
{
    /// <summary>
    /// Contains information about an archive
    /// </summary>
    public class ArchiveInfo
    {
        /// <summary>
        /// Filesize in bytes
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// Number of files found in the archive
        /// </summary>
        public int NumberOfFiles { get; set; }

        /// <summary>
        /// Numer of d64-Files found in the archive
        /// </summary>
        public int NumberOfD64Files { get; set; }

        public IEnumerable<CompressedFileInfo> CompressedFileInfos { get; set; }
    }
}