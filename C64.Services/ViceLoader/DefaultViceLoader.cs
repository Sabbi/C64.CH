using C64.Services.Archive;
using System.Text;

namespace C64.Services.ViceLoader
{
    public class DefaultViceLoader : IViceLoader
    {
        private readonly IArchiveService archiveService;

        public DefaultViceLoader(IArchiveService archiveService)
        {
            this.archiveService = archiveService;
        }

        public (string SetupEmu, object SetupEmuParameters) ProcessFile(int productionFileId, byte[] fileData)
        {
            IViceDepacker viceDepacker;

            archiveService.Load(fileData);

            switch (Md5Hash(fileData))
            {
                // productions/1466/Biba_2_-_Dream_Injection
                case "37d16933646118bc3da83bdbec6e1c40":
                    viceDepacker = new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "?demo*");
                    break;

                // productions/241/Airdance_4
                case "b4a1b36b4dbe8d9fadd5792959fb0d6d":
                    viceDepacker = new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "air*");
                    break;

                // productions/89/Dawnfall
                case "6ff1aa9c0ba15f2bd8ade9019f8ab1c2":
                    viceDepacker = new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "dawnfall");
                    break;

                // productions/3623/Blaze_Of_Glory
                case "0e9531d4fc6299839783dafc9a8eb13b":
                    viceDepacker = new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, " blaze*");
                    break;

                default:
                    viceDepacker = new ViceDepacker(productionFileId, archiveService.ArchiveInfo);
                    break;
            }

            var result = viceDepacker.ProcessFile();
            return (result.SetupEmu, result.SetupEmuParameters);
        }

        private string Md5Hash(byte[] fileData)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            var hash = md5.ComputeHash(fileData);

            var result = new StringBuilder(hash.Length * 2);

            for (int i = 0; i < hash.Length; i++)
                result.Append(hash[i].ToString("x2"));
            return result.ToString();
        }
    }
}