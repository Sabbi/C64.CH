using System;
using System.Threading.Tasks;

namespace OldDataImporter
{
    internal class Program
    {
        private async static Task Main(string[] args)
        {
            var importerMenu = new ImporterMenu();

            var importer = new Importer();

            while (true)
            {
                importerMenu.DisplayMenu();

                var input = Console.ReadLine();

                if (input == "x" || input == "q")
                    return;

                int.TryParse(input, out var selection);

                switch (selection)
                {
                    case 1:
                        Console.WriteLine("Starting UserImport");
                        await importer.ImportUserAsync();
                        Console.WriteLine("UserImport Finished!");
                        break;

                    case 2:
                        Console.WriteLine("Trying to add roles...");
                        var result = await importer.AddRoles();
                        Console.WriteLine("...done");

                        if (!result)
                            Console.WriteLine("There where issues adding the roles, maybe they're already there...");
                        Console.WriteLine("Add Sabbi to Admin / Moderator...");
                        await importer.AddToAdmin();
                        Console.WriteLine("...done");
                        break;

                    case 3:
                        Console.WriteLine("Importing guestbook...");
                        await importer.ImportGuestbook();
                        Console.WriteLine("...done");
                        break;

                    case 4:
                        Console.WriteLine("Importing siteInfos...");
                        await importer.ImportSiteInfos();
                        Console.WriteLine("...done");

                        break;

                    case 5:
                        Console.WriteLine("Importing links...");
                        await importer.ImportLinks();
                        Console.WriteLine("...done");
                        break;

                    case 6:
                        Console.WriteLine("Store demo files...");
                        await importer.StoreFiles();
                        Console.WriteLine("...done");
                        break;

                    case 7:

                        Console.WriteLine("Store demo pictures...");
                        await importer.StorePictures();
                        Console.WriteLine("...done");
                        break;

                    case 8:
                        Console.WriteLine("Importing groups...");
                        await importer.ImportGroups();
                        Console.WriteLine("...done");
                        break;

                    case 9:
                        Console.WriteLine("Importing parties...");
                        await importer.ImportParties();
                        Console.WriteLine("...done");
                        break;

                    case 10:
                        Console.WriteLine("Importing Demos...");
                        await importer.ImportDemos();
                        Console.WriteLine("...done");
                        break;

                    case 11:
                        Console.WriteLine("Importing Demos -> Group relation...");
                        await importer.DemosToGroups();
                        Console.WriteLine("...done");

                        break;

                    case 12:
                        Console.WriteLine("Importing Demos -> Party relation...");
                        await importer.ImportDemoParty();
                        Console.WriteLine("...done");
                        break;

                    case 13:
                        Console.WriteLine("Importing Ratings...");
                        await importer.ImportVotes();
                        Console.WriteLine("...done");

                        break;

                    case 14:
                        Console.Write("Sorting images...");
                        await importer.SortImagesAsync();
                        Console.WriteLine("...done");
                        break;

                    case 15:
                        Console.Write("Importing Old Downloads...");
                        await importer.ImportOldDownloads();
                        Console.WriteLine("...done");
                        break;

                    case 16:
                        Console.Write("Importing Remaining Downloads...");
                        await importer.ImportDownloads();
                        Console.WriteLine("...done");
                        break;

                    default:
                        Console.WriteLine("Invalid Selection");
                        break;
                }
            }
        }
    }

    public class ImporterMenu
    {
        public void DisplayMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("C64 Old Data Importer (C) 2020 Sabbi / C64.CH");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Note: Must be started inside the data-Directory of the old c64.ch - Otherwise pictures and demos cannot be imported!");
            Console.WriteLine("Note2: Download-Table must be fixed with 'update downloads set Datum = From_UnixTime(0) where Datum = \"\" and ID > 0'");
            Console.WriteLine("ALTER TABLE downloads; CHANGE COLUMN `Datum` `Datum` DATETIME NOT NULL DEFAULT '1970-01-01';");
            Console.WriteLine("");
            Console.WriteLine(" 1 - Users");
            Console.WriteLine(" 2 - Roles / Admin2Sabbi");
            Console.WriteLine(" 3 - Guestbook");
            Console.WriteLine(" 4 - SiteInfos");
            Console.WriteLine(" 5 - Links");
            Console.WriteLine(" 6 - Demo Files");
            Console.WriteLine(" 7 - Demo Pictures");
            Console.WriteLine(" 8 - Groups");
            Console.WriteLine(" 9 - Parties");
            Console.WriteLine("10 - Demos");
            Console.WriteLine("11 - Demo -> Group");
            Console.WriteLine("12 - Demo -> Party");
            Console.WriteLine("13 - Ratings");
            Console.WriteLine("14 - Sort Images");
            Console.WriteLine("15 - Old Downloads");
            Console.WriteLine("16 - Remaining Downloads");
            Console.WriteLine("");
            Console.WriteLine("x  - Exit");
            Console.WriteLine("");
            Console.Write("Please choose: ");
        }
    }
}