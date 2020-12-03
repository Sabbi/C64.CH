using C64.Services.Archive;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C64.Services.ViceLoader
{
    public interface IViceLoader
    {
        (string SetupEmu, object SetupEmuParameters) ProcessFile(int productionFileId, byte[] fileData);
    }

    public class DefaultViceLoader : IViceLoader
    {
        private readonly IArchiveService archiveService;

        public DefaultViceLoader(IArchiveService archiveService)
        {
            this.archiveService = archiveService;
        }

        public (string SetupEmu, object SetupEmuParameters) ProcessFile(int productionFileId, byte[] fileData)
        {
            archiveService.Load(fileData);

            var fileIndexesToLoad = new List<int>();

            var archiveInfo = archiveService.ArchiveInfo;
            var arcl = archiveInfo.CompressedFileInfos.ToList();
            if (archiveInfo.NumberOfD64Files == 1)
            {
                var fileInfo = archiveInfo.CompressedFileInfos.FirstOrDefault(p => p.IsD64);
                fileIndexesToLoad.Add(arcl.IndexOf(fileInfo));
            }
            else if (archiveInfo.NumberOfD64Files > 1)
            {
                foreach (var fileInfo in archiveInfo.CompressedFileInfos.Where(p => p.IsD64).OrderBy(p => p.FileName))
                {
                    fileIndexesToLoad.Add(arcl.IndexOf(fileInfo));
                }
            }
            else
            {
                // if no d64-Files, load first prg, t64, p00 or t00-file
                var prg = archiveInfo.CompressedFileInfos.FirstOrDefault(p => p.FileName.EndsWith(".prg", StringComparison.OrdinalIgnoreCase));
                if (prg == null)
                    prg = archiveInfo.CompressedFileInfos.FirstOrDefault(p => p.FileName.EndsWith(".t64", StringComparison.OrdinalIgnoreCase));
                if (prg == null)
                    prg = archiveInfo.CompressedFileInfos.FirstOrDefault(p => p.FileName.EndsWith(".p00", StringComparison.OrdinalIgnoreCase));
                if (prg == null)
                    prg = archiveInfo.CompressedFileInfos.FirstOrDefault(p => p.FileName.EndsWith(".t00", StringComparison.OrdinalIgnoreCase));

                fileIndexesToLoad.Add(arcl.IndexOf(prg));
            }

            var list = new List<string>();
            var flipList = new List<int>();

            if (fileIndexesToLoad.Any())
            {
                foreach (var indexToLoad in fileIndexesToLoad)
                {
                    list.Add($"{productionFileId}-{indexToLoad}.bin");
                }

                if (fileIndexesToLoad.Count > 1)
                {
                    for (var i = 1; i < fileIndexesToLoad.Count + 1; i++)
                    {
                        flipList.Insert(0, i);
                    }
                }

                flipList.Reverse();
            }

            return ("setupEmu", new object[] { list, flipList });
        }
    }
}