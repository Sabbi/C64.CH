using C64.Services.Archive;
using System.Linq;

namespace C64.Services.ViceLoader
{
    public class ViceDepackerCustomPrgFileName : ViceDepacker, IViceDepacker
    {
        private readonly string startFile;

        public ViceDepackerCustomPrgFileName(int productionFileId, ArchiveInfo archiveInfo, string startFile) : base(productionFileId, archiveInfo)
        {
            this.startFile = startFile;
        }

        public override (string SetupEmu, object SetupEmuParameters, bool enableDiskChange) ProcessFile()
        {
            var lists = GenerateLists(productionFileId, archiveInfo);
            var prg = archiveInfo.CompressedFileInfos.FirstOrDefault(p => p.FileName.Equals(startFile));
            var indexToLoad = archiveInfo.CompressedFileInfos.ToList().IndexOf(prg);
            return ("setupEmu", new object[] { new[] { $"{productionFileId}-{indexToLoad}.bin" }, new[] { 0 } }, false);
        }
    }
}