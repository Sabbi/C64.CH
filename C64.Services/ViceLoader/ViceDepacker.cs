using C64.Services.Archive;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C64.Services.ViceLoader
{
    public class ViceDepacker : IViceDepacker
    {
        protected readonly int productionFileId;
        protected readonly ArchiveInfo archiveInfo;

        public ViceDepacker(int productionFileId, ArchiveInfo archiveInfo)
        {
            this.productionFileId = productionFileId;
            this.archiveInfo = archiveInfo;
        }

        public virtual (string SetupEmu, object SetupEmuParameters, bool enableDiskChange) ProcessFile()
        {
            var lists = GenerateLists(productionFileId, archiveInfo);
            return ("setupEmu", new object[] { lists.list, lists.flipList }, lists.list.Count() > 1);
        }

        protected (IEnumerable<string> list, IEnumerable<int> flipList) GenerateLists(int productionFileId, ArchiveInfo archiveInfo)
        {
            var fileIndexesToLoad = new List<int>();
            var arcl = archiveInfo.CompressedFileInfos.ToList();
            if (archiveInfo.NumberOfD64Files == 1)
            {
                var fileInfo = archiveInfo.CompressedFileInfos.FirstOrDefault(p => p.IsD64);
                fileIndexesToLoad.Add(arcl.IndexOf(fileInfo));
            }
            else if (archiveInfo.NumberOfD64Files > 1)
            {
                foreach (var fileInfo in archiveInfo.CompressedFileInfos.Where(p => p.IsD64).OrderBy(p => System.IO.Path.GetFileNameWithoutExtension(p.FileName)))
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
                if (prg == null)
                    prg = archiveInfo.CompressedFileInfos.FirstOrDefault(p => !p.FileName.EndsWith(".txt"));

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

            return (list, flipList);
        }
    }
}