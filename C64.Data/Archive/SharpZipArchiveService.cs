using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace C64.Data.Archive
{
    public class SharpZipArchiveService : BaseArchiveService, IArchiveService
    {
        protected byte[] archiveData;
        private readonly ILogger<SharpZipArchiveService> logger;

        public SharpZipArchiveService(ILogger<SharpZipArchiveService> logger)
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
            using (var byteStream = new MemoryStream())
            {
                byteStream.Write(archiveData, 0, archiveData.Length);

                try
                {
                    using (var archive = new ZipFile(byteStream))
                    {
                        archive.BeginUpdate();

                        var index = archive.FindEntry("file_id.diz", true);

                        if (archive.FindEntry("file_id.diz", true) >= 0)
                            archive.Delete("file_id.diz");

                        archive.Add(new StringStaticDataSource(fileIdDiz), "file_id.diz");
                        archive.SetComment(fileIdDiz);

                        archive.CommitUpdate();
                        archive.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("AddFileId Failed: " + e.Message);
                }
                return byteStream.ToArray();
            }
        }

        public byte[] GetFile(string fileName)
        {
            using (var byteStream = new MemoryStream(archiveData))
            {
                using (var zf = new ZipFile(byteStream))
                {
                    var file = zf.GetEntry(fileName);

                    if (file == null)
                        throw new FileNotFoundException("Cannot find archived file");

                    using (var reader = new StreamReader(zf.GetInputStream(file)))
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
                    using (var archive = new ZipFile(byteStream))
                    {
                        foreach (ZipEntry entry in archive)
                        {
                            if (!entry.Name.Equals("file_id.diz"))
                            {
                                var isD64 = entry.Name.EndsWith(".d64", StringComparison.OrdinalIgnoreCase);
                                retVal.Add(new CompressedFileInfo { Created = entry.DateTime, FileName = entry.Name, Size = entry.Size, CompressedSize = entry.CompressedSize, IsD64 = isD64 });
                            }
                        }
                    }
                    return retVal;
                }
            }
            catch
            {
                logger.LogWarning("Cannot list files in archive {archiveData}", archiveData);
                return retVal;
            }
        }

        private class StringStaticDataSource : IStaticDataSource
        {
            private readonly byte[] data;

            public StringStaticDataSource(string fileIdData)
            {
                var encoder = new System.Text.ASCIIEncoding();
                data = encoder.GetBytes(fileIdData);
            }

            public Stream GetSource()
            {
                return new MemoryStream(data);
            }
        }
    }
}