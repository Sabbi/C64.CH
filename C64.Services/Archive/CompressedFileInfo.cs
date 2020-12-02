using System;

namespace C64.Services.Archive
{
    public class CompressedFileInfo
    {
        public long Size { get; set; }
        public long CompressedSize { get; set; }
        public string FileName { get; set; }
        public DateTime Created { get; set; }
        public bool IsD64 { get; set; }
    }
}