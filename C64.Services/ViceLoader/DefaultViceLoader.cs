﻿using C64.Services.Archive;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C64.Services.ViceLoader
{
    public class DefaultViceLoader : IViceLoader
    {
        private readonly IArchiveService archiveService;

        public DefaultViceLoader(IArchiveService archiveService)
        {
            this.archiveService = archiveService;
        }

        public (string SetupEmu, object SetupEmuParameters, bool enableDiskChange) ProcessFile(int productionFileId, string filename, byte[] fileData)
        {
            archiveService.Load(fileData);

            var specialCase = viceLoaderSpecialCases.FirstOrDefault(p => p.FileName.Equals(filename, StringComparison.OrdinalIgnoreCase))?.Depacker;

            IViceDepacker viceDepacker = specialCase == null ? new ViceDepacker(productionFileId, archiveService.ArchiveInfo) : specialCase.Invoke(productionFileId, archiveService);

            return viceDepacker.ProcessFile();
        }

        private IEnumerable<ViceLoaderSpecialCase> viceLoaderSpecialCases = new List<ViceLoaderSpecialCase>() {
            // productions/1466/Biba_2_-_Dream_Injection
            new ViceLoaderSpecialCase("arise-biba_2.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "?demo*")),

            // productions/241/Airdance_4
            new ViceLoaderSpecialCase("tat-airdance4.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "air*")),

            // productions/89/Dawnfall
            new ViceLoaderSpecialCase("oxyron_cml-downfall.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "dawnfall")),

            // productions/3623/Blaze_Of_Glory
            new ViceLoaderSpecialCase("house_designs-blaze_of_glory.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, " blaze*")),

            // productions/7543/Artillery_100_percent
            new ViceLoaderSpecialCase("shape-artillery_100_percent.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "artillery*")),

            //  productions/7346/Artillery_85_percent
            new ViceLoaderSpecialCase("shape-artillery_85_percent.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "artillery*")),

            // productions/207/Trip_to_Nepal_25
            new ViceLoaderSpecialCase("cascade-trip_to_nepal_2.5.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "t.t.*")),

            // productions/205/Trip_to_Nepal
            new ViceLoaderSpecialCase("cascade-trip_to_nepal_1.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "trip*")),

            // productions/206/Trip_to_Nepal_2
            new ViceLoaderSpecialCase("cascade-trip_to_nepal_2.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "trip*")),

            // productions/4880/Beavis_Butthead_-_The_Dentro
            new ViceLoaderSpecialCase("agnusdei-beavis___butthead_-_the_dentro.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerCustomPrgFileName(productionFileId, archiveService.ArchiveInfo, "bbdentro.prg")),

            // productions/717/Lazer_Link (Issue #192)
            new ViceLoaderSpecialCase("tat-lazerlink.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "?tat*")),

            // productions/24/Fierce_Creations (Issue #180)
            new ViceLoaderSpecialCase("fierce.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "fierce*")),

            // productions/1344/Illmatic (Issue #199)
            new ViceLoaderSpecialCase("elysium-illmatic.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "? l l*")),

            // productions/6601/2nd_Demo
            new ViceLoaderSpecialCase("hondani-2nd_demo.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "a.*")),

            // /productions/2716/Amiga_Ball
            new ViceLoaderSpecialCase("various-amiga_ball.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "amiga-ball*")),

            // /productions/8336/Crystal_Waters
            new ViceLoaderSpecialCase("Crystal Waters - TNC.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "-cry*")),

            // /productions/6835/One_Day_Demo
            new ViceLoaderSpecialCase("hype-one_day_demo.zip", (int productionFileId, IArchiveService archiveService) =>  new ViceDepackerDifferentD64StartFile(productionFileId, archiveService.ArchiveInfo, "the one day demo")),
        };
    }

    public class ViceLoaderSpecialCase
    {
        public string FileName { get; set; }
        public Func<int, IArchiveService, IViceDepacker> Depacker { get; set; }

        public ViceLoaderSpecialCase(string fileName, Func<int, IArchiveService, IViceDepacker> depacker)
        {
            FileName = fileName;
            Depacker = depacker;
        }
    }
}