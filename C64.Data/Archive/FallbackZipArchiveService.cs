using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace C64.Data.Archive
{
    public class FallbackArchiveService : BaseArchiveService, IArchiveService, IFallbackArchiveService
    {
        protected byte[] archiveData;
        private readonly ILogger<FallbackArchiveService> logger;

        public FallbackArchiveService(ILogger<FallbackArchiveService> logger)
        {
            this.logger = logger;
        }

        public void Load(byte[] archiveData)
        {
            this.archiveData = archiveData;
        }

        public ArchiveInfo ArchiveInfo
        {
            get
            {
                if (archiveData == null)
                    return null;
                var retVal = new ArchiveInfo
                {
                    CompressedFileInfos = ListFilesInArchive(),
                    FileSize = archiveData.Length
                };
                retVal.NumberOfFiles = retVal.CompressedFileInfos.Count();
                retVal.NumberOfD64Files = retVal.CompressedFileInfos.Count(p => p.FileName.EndsWith(".d64", StringComparison.OrdinalIgnoreCase));
                return retVal;
            }
        }

        public byte[] AddFileId()
        {
            throw new NotImplementedException();
        }

        public byte[] GetFile(string fileName)
        {
            using (var byteStream = new MemoryStream(archiveData))
            {
                using (var zf = new ZipArchive(byteStream))
                {
                    var file = zf.GetEntry(fileName);

                    if (file == null)
                        throw new FileNotFoundException("Cannot find archived file");

                    using (var reader = new StreamReader(file.Open()))
                    {
                        using (var ms = new MemoryStream())
                        {
                            reader.BaseStream.CopyTo(ms);
                            return ms.ToArray();
                        }
                    }
                }
            }
        }

        private IEnumerable<CompressedFileInfo> ListFilesInArchive()
        {
            var retVal = new List<CompressedFileInfo>();
            try
            {
                using (var byteStream = new MemoryStream(archiveData))
                {
                    using (var archive = new ZipArchive(byteStream))
                    {
                        foreach (var entry in archive.Entries)
                        {
                            if (!entry.Name.Equals("file_id.diz") && !entry.Name.StartsWith("__MACOSX/", StringComparison.OrdinalIgnoreCase))
                            {
                                var isD64 = entry.Name.EndsWith(".d64", StringComparison.OrdinalIgnoreCase);
                                retVal.Add(new CompressedFileInfo { Created = entry.LastWriteTime.UtcDateTime, FileName = entry.Name, Size = entry.Length, CompressedSize = entry.CompressedLength, IsD64 = isD64 });
                            }
                        }
                    }
                    return retVal;
                }
            }
            catch (Exception e)
            {
                logger.LogWarning("Cannot list files in archive {archiveData}", archiveData);
                logger.LogWarning("{e}", e);
                return retVal;
            }
        }
    }
}