using C64.Services.Archive;
using System.Linq;

namespace C64.Services.ViceLoader
{
    public class ViceDepackerDifferentD64StartFile : ViceDepacker, IViceDepacker
    {
        private readonly string startFile;

        public ViceDepackerDifferentD64StartFile(int productionFileId, ArchiveInfo archiveInfo, string startFile) : base(productionFileId, archiveInfo)
        {
            this.startFile = startFile;
        }

        public override (string SetupEmu, object SetupEmuParameters, bool enableDiskChange) ProcessFile()
        {
            var lists = GenerateLists(productionFileId, archiveInfo);
            return ("setupEmu", new object[] { lists.list, lists.flipList, startFile }, lists.list.Count() > 1);
        }
    }
}