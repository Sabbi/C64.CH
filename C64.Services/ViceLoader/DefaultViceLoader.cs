using C64.Services.Archive;

namespace C64.Services.ViceLoader
{
    public class DefaultViceLoader : IViceLoader
    {
        private readonly IArchiveService archiveService;

        public DefaultViceLoader(IArchiveService archiveService)
        {
            this.archiveService = archiveService;
        }

        public (string SetupEmu, object SetupEmuParameters) ProcessFile(int productionFileId, string filename, byte[] fileData)
        {
            IViceDepacker viceDepacker;

            archiveService.Load(fileData);

            switch (filename)
            {
                // productions/1466/Biba_2_-_Dream_Injection
                case "arise-biba_2.zip":
                    viceDepacker = new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "?demo*");
                    break;

                // productions/241/Airdance_4
                case "tat-airdance4.zip":
                    viceDepacker = new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "air*");
                    break;

                // productions/89/Dawnfall
                case "oxyron_cml-downfall.zip":
                    viceDepacker = new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "dawnfall");
                    break;

                // productions/3623/Blaze_Of_Glory
                case "house_designs-blaze_of_glory.zip":
                    viceDepacker = new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, " blaze*");
                    break;

                // productions/4880/Beavis_Butthead_-_The_Dentro
                case "agnusdei-beavis___butthead_-_the_dentro.zip":
                    viceDepacker = new ViceDepackerCustomPrgFileName(productionFileId, archiveService.ArchiveInfo, "bbdentro.prg");
                    break;

                // productions/7543/Artillery_100_percent
                // productions/7346/Artillery_85_percent
                case "shape-artillery_100_percent.zip":
                case "shape-artillery_85_percent.zip":
                    viceDepacker = new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "artillery*");
                    break;

                default:
                    viceDepacker = new ViceDepacker(productionFileId, archiveService.ArchiveInfo);
                    break;
            }

            var result = viceDepacker.ProcessFile();
            return (result.SetupEmu, result.SetupEmuParameters);
        }
    }
}