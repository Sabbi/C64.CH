using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class TablesNamesToLowercase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Countries_CountryId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Sceners_ScenerId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Productions_ProductionId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Downloads_AspNetUsers_UserId",
                table: "Downloads");

            migrationBuilder.DropForeignKey(
                name: "FK_Downloads_ProductionFiles_ProductionFileId",
                table: "Downloads");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_AddedById",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Countries_CountryId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GuestbookEntries_AspNetUsers_UserId",
                table: "GuestbookEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_HiddenParts_Productions_ProductionId",
                table: "HiddenParts");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryRecords_AspNetUsers_UserId",
                table: "HistoryRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryRecords_Groups_AffectedGroupId",
                table: "HistoryRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryRecords_Parties_AffectedPartyId",
                table: "HistoryRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryRecords_Productions_AffectedProductionId",
                table: "HistoryRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryRecords_Sceners_AffectedScenerId",
                table: "HistoryRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Links_LinkCategories_LinkCategoryId",
                table: "Links");

            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Countries_CountryId",
                table: "Parties");

            migrationBuilder.DropForeignKey(
                name: "FK_PartiesGroups_Groups_GroupId",
                table: "PartiesGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_PartiesGroups_Parties_PartyId",
                table: "PartiesGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_PartiesSceners_Groups_GroupId",
                table: "PartiesSceners");

            migrationBuilder.DropForeignKey(
                name: "FK_PartiesSceners_Parties_PartyId",
                table: "PartiesSceners");

            migrationBuilder.DropForeignKey(
                name: "FK_PartiesSceners_Sceners_ScenerId",
                table: "PartiesSceners");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionCredits_Productions_ProductionId",
                table: "ProductionCredits");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionCredits_Sceners_ScenerId",
                table: "ProductionCredits");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionFiles_Productions_ProductionId",
                table: "ProductionFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionPictures_Productions_ProductionId",
                table: "ProductionPictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_AspNetUsers_SubmitterUserId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_AspNetUsers_UserId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionsGroups_Groups_GroupId",
                table: "ProductionsGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionsGroups_Productions_ProductionId",
                table: "ProductionsGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionsParties_Parties_PartyId",
                table: "ProductionsParties");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionsParties_PartyCategories_PartyCategoryId",
                table: "ProductionsParties");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionsParties_Productions_ProductionId",
                table: "ProductionsParties");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionVideos_Productions_ProductionId",
                table: "ProductionVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Productions_ProductionId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_ScenerGroupJob_ScenersGroups_ScenersGroupsId",
                table: "ScenerGroupJob");

            migrationBuilder.DropForeignKey(
                name: "FK_Sceners_Countries_CountryId",
                table: "Sceners");

            migrationBuilder.DropForeignKey(
                name: "FK_ScenerSceners_Sceners_ScenerToId",
                table: "ScenerSceners");

            migrationBuilder.DropForeignKey(
                name: "FK_ScenersGroups_Groups_GroupId",
                table: "ScenersGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ScenersGroups_Sceners_ScenerId",
                table: "ScenersGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ScenersJobs_Sceners_ScenerId",
                table: "ScenersJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteInfos_AspNetUsers_UserId",
                table: "SiteInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_AspNetUsers_UserId",
                table: "UserFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_Productions_ProductionId",
                table: "UserFavorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFavorites",
                table: "UserFavorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tools",
                table: "Tools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SiteInfos",
                table: "SiteInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScenersJobs",
                table: "ScenersJobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScenersGroups",
                table: "ScenersGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScenerSceners",
                table: "ScenerSceners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sceners",
                table: "Sceners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScenerGroupJob",
                table: "ScenerGroupJob");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionVideos",
                table: "ProductionVideos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionsParties",
                table: "ProductionsParties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionsGroups",
                table: "ProductionsGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Productions",
                table: "Productions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionPictures",
                table: "ProductionPictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionFiles",
                table: "ProductionFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionCredits",
                table: "ProductionCredits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartyCategories",
                table: "PartyCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartiesSceners",
                table: "PartiesSceners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartiesGroups",
                table: "PartiesGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parties",
                table: "Parties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Links",
                table: "Links");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LinkCategories",
                table: "LinkCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryRecords",
                table: "HistoryRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HiddenParts",
                table: "HiddenParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GuestbookEntries",
                table: "GuestbookEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Downloads",
                table: "Downloads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbFiles",
                table: "DbFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "UserFavorites",
                newName: "userfavorites");

            migrationBuilder.RenameTable(
                name: "Tools",
                newName: "tools");

            migrationBuilder.RenameTable(
                name: "SiteInfos",
                newName: "siteinfos");

            migrationBuilder.RenameTable(
                name: "ScenersJobs",
                newName: "scenersjobs");

            migrationBuilder.RenameTable(
                name: "ScenersGroups",
                newName: "scenersgroups");

            migrationBuilder.RenameTable(
                name: "ScenerSceners",
                newName: "scenersceners");

            migrationBuilder.RenameTable(
                name: "Sceners",
                newName: "sceners");

            migrationBuilder.RenameTable(
                name: "ScenerGroupJob",
                newName: "scenergroupjob");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "ratings");

            migrationBuilder.RenameTable(
                name: "ProductionVideos",
                newName: "productionvideos");

            migrationBuilder.RenameTable(
                name: "ProductionsParties",
                newName: "productionsparties");

            migrationBuilder.RenameTable(
                name: "ProductionsGroups",
                newName: "productionsgroups");

            migrationBuilder.RenameTable(
                name: "Productions",
                newName: "productions");

            migrationBuilder.RenameTable(
                name: "ProductionPictures",
                newName: "productionpictures");

            migrationBuilder.RenameTable(
                name: "ProductionFiles",
                newName: "productionfiles");

            migrationBuilder.RenameTable(
                name: "ProductionCredits",
                newName: "productioncredits");

            migrationBuilder.RenameTable(
                name: "PartyCategories",
                newName: "partycategories");

            migrationBuilder.RenameTable(
                name: "PartiesSceners",
                newName: "partiessceners");

            migrationBuilder.RenameTable(
                name: "PartiesGroups",
                newName: "partiesgroups");

            migrationBuilder.RenameTable(
                name: "Parties",
                newName: "parties");

            migrationBuilder.RenameTable(
                name: "Links",
                newName: "links");

            migrationBuilder.RenameTable(
                name: "LinkCategories",
                newName: "linkcategories");

            migrationBuilder.RenameTable(
                name: "HistoryRecords",
                newName: "historyrecords");

            migrationBuilder.RenameTable(
                name: "HiddenParts",
                newName: "hiddenparts");

            migrationBuilder.RenameTable(
                name: "GuestbookEntries",
                newName: "guestbookentries");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "groups");

            migrationBuilder.RenameTable(
                name: "Downloads",
                newName: "downloads");

            migrationBuilder.RenameTable(
                name: "DbFiles",
                newName: "dbfiles");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "countries");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "comments");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "aspnetusertokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "aspnetusers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "aspnetuserroles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "aspnetuserlogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "aspnetuserclaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "aspnetroles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "aspnetroleclaims");

            migrationBuilder.RenameIndex(
                name: "IX_UserFavorites_ProductionId",
                table: "userfavorites",
                newName: "IX_userfavorites_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Tools_Show",
                table: "tools",
                newName: "IX_tools_Show");

            migrationBuilder.RenameIndex(
                name: "IX_SiteInfos_UserId",
                table: "siteinfos",
                newName: "IX_siteinfos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ScenersJobs_ScenerId",
                table: "scenersjobs",
                newName: "IX_scenersjobs_ScenerId");

            migrationBuilder.RenameIndex(
                name: "IX_ScenersGroups_ScenerId",
                table: "scenersgroups",
                newName: "IX_scenersgroups_ScenerId");

            migrationBuilder.RenameIndex(
                name: "IX_ScenersGroups_GroupId",
                table: "scenersgroups",
                newName: "IX_scenersgroups_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_ScenerSceners_ScenerToId",
                table: "scenersceners",
                newName: "IX_scenersceners_ScenerToId");

            migrationBuilder.RenameIndex(
                name: "IX_Sceners_CountryId",
                table: "sceners",
                newName: "IX_sceners_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_ScenerGroupJob_ScenersGroupsId",
                table: "scenergroupjob",
                newName: "IX_scenergroupjob_ScenersGroupsId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_UserId",
                table: "ratings",
                newName: "IX_ratings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_ProductionId",
                table: "ratings",
                newName: "IX_ratings_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionVideos_ProductionId",
                table: "productionvideos",
                newName: "IX_productionvideos_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionsParties_PartyId",
                table: "productionsparties",
                newName: "IX_productionsparties_PartyId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionsParties_PartyCategoryId",
                table: "productionsparties",
                newName: "IX_productionsparties_PartyCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionsGroups_GroupId",
                table: "productionsgroups",
                newName: "IX_productionsgroups_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Productions_UserId",
                table: "productions",
                newName: "IX_productions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Productions_SubmitterUserId",
                table: "productions",
                newName: "IX_productions_SubmitterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionPictures_ProductionId",
                table: "productionpictures",
                newName: "IX_productionpictures_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionFiles_ProductionId",
                table: "productionfiles",
                newName: "IX_productionfiles_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionCredits_ScenerId",
                table: "productioncredits",
                newName: "IX_productioncredits_ScenerId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionCredits_ProductionId",
                table: "productioncredits",
                newName: "IX_productioncredits_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_PartiesSceners_ScenerId",
                table: "partiessceners",
                newName: "IX_partiessceners_ScenerId");

            migrationBuilder.RenameIndex(
                name: "IX_PartiesSceners_GroupId",
                table: "partiessceners",
                newName: "IX_partiessceners_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_PartiesGroups_GroupId",
                table: "partiesgroups",
                newName: "IX_partiesgroups_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Parties_CountryId",
                table: "parties",
                newName: "IX_parties_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Links_LinkCategoryId",
                table: "links",
                newName: "IX_links_LinkCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryRecords_UserId",
                table: "historyrecords",
                newName: "IX_historyrecords_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryRecords_AffectedScenerId",
                table: "historyrecords",
                newName: "IX_historyrecords_AffectedScenerId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryRecords_AffectedProductionId",
                table: "historyrecords",
                newName: "IX_historyrecords_AffectedProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryRecords_AffectedPartyId",
                table: "historyrecords",
                newName: "IX_historyrecords_AffectedPartyId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryRecords_AffectedGroupId",
                table: "historyrecords",
                newName: "IX_historyrecords_AffectedGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_HiddenParts_ProductionId",
                table: "hiddenparts",
                newName: "IX_hiddenparts_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_GuestbookEntries_UserId",
                table: "guestbookentries",
                newName: "IX_guestbookentries_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_CountryId",
                table: "groups",
                newName: "IX_groups_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_AddedById",
                table: "groups",
                newName: "IX_groups_AddedById");

            migrationBuilder.RenameIndex(
                name: "IX_Downloads_UserId",
                table: "downloads",
                newName: "IX_downloads_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Downloads_ProductionFileId_DownloadedOn",
                table: "downloads",
                newName: "IX_downloads_ProductionFileId_DownloadedOn");

            migrationBuilder.RenameIndex(
                name: "IX_Downloads_ProductionFileId",
                table: "downloads",
                newName: "IX_downloads_ProductionFileId");

            migrationBuilder.RenameIndex(
                name: "IX_Downloads_DownloadedOn",
                table: "downloads",
                newName: "IX_downloads_DownloadedOn");

            migrationBuilder.RenameIndex(
                name: "IX_DbFiles_Container_FileName",
                table: "dbfiles",
                newName: "IX_dbfiles_Container_FileName");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "comments",
                newName: "IX_comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ProductionId",
                table: "comments",
                newName: "IX_comments_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_ScenerId",
                table: "aspnetusers",
                newName: "IX_aspnetusers_ScenerId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CountryId",
                table: "aspnetusers",
                newName: "IX_aspnetusers_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "aspnetuserroles",
                newName: "IX_aspnetuserroles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "aspnetuserlogins",
                newName: "IX_aspnetuserlogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "aspnetuserclaims",
                newName: "IX_aspnetuserclaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "aspnetroleclaims",
                newName: "IX_aspnetroleclaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userfavorites",
                table: "userfavorites",
                columns: new[] { "UserId", "ProductionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_tools",
                table: "tools",
                column: "ToolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_siteinfos",
                table: "siteinfos",
                column: "SiteInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_scenersjobs",
                table: "scenersjobs",
                column: "ScenersJobsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_scenersgroups",
                table: "scenersgroups",
                column: "ScenersGroupsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_scenersceners",
                table: "scenersceners",
                columns: new[] { "ScenerId", "ScenerToId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_sceners",
                table: "sceners",
                column: "ScenerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_scenergroupjob",
                table: "scenergroupjob",
                column: "ScenerGroupJobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ratings",
                table: "ratings",
                column: "RatingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productionvideos",
                table: "productionvideos",
                column: "ProductionVideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productionsparties",
                table: "productionsparties",
                columns: new[] { "ProductionId", "PartyId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_productionsgroups",
                table: "productionsgroups",
                columns: new[] { "ProductionId", "GroupId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_productions",
                table: "productions",
                column: "ProductionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productionpictures",
                table: "productionpictures",
                column: "ProductionPictureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productionfiles",
                table: "productionfiles",
                column: "ProductionFileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productioncredits",
                table: "productioncredits",
                column: "ProductionCreditId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_partycategories",
                table: "partycategories",
                column: "PartyCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_partiessceners",
                table: "partiessceners",
                columns: new[] { "PartyId", "ScenerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_partiesgroups",
                table: "partiesgroups",
                columns: new[] { "PartyId", "GroupId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_parties",
                table: "parties",
                column: "PartyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_links",
                table: "links",
                column: "LinkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_linkcategories",
                table: "linkcategories",
                column: "LinkCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_historyrecords",
                table: "historyrecords",
                column: "HistoryRecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hiddenparts",
                table: "hiddenparts",
                column: "HiddenPartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_guestbookentries",
                table: "guestbookentries",
                column: "GuestbookEntryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_groups",
                table: "groups",
                column: "GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_downloads",
                table: "downloads",
                column: "DownloadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbfiles",
                table: "dbfiles",
                column: "DbFileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_countries",
                table: "countries",
                column: "CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_comments",
                table: "comments",
                column: "CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetusertokens",
                table: "aspnetusertokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetusers",
                table: "aspnetusers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetuserroles",
                table: "aspnetuserroles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetuserlogins",
                table: "aspnetuserlogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetuserclaims",
                table: "aspnetuserclaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetroles",
                table: "aspnetroles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetroleclaims",
                table: "aspnetroleclaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetroleclaims_aspnetroles_RoleId",
                table: "aspnetroleclaims",
                column: "RoleId",
                principalTable: "aspnetroles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetuserclaims_aspnetusers_UserId",
                table: "aspnetuserclaims",
                column: "UserId",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetuserlogins_aspnetusers_UserId",
                table: "aspnetuserlogins",
                column: "UserId",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetuserroles_aspnetroles_RoleId",
                table: "aspnetuserroles",
                column: "RoleId",
                principalTable: "aspnetroles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetuserroles_aspnetusers_UserId",
                table: "aspnetuserroles",
                column: "UserId",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetusers_countries_CountryId",
                table: "aspnetusers",
                column: "CountryId",
                principalTable: "countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetusers_sceners_ScenerId",
                table: "aspnetusers",
                column: "ScenerId",
                principalTable: "sceners",
                principalColumn: "ScenerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetusertokens_aspnetusers_UserId",
                table: "aspnetusertokens",
                column: "UserId",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_aspnetusers_UserId",
                table: "comments",
                column: "UserId",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_productions_ProductionId",
                table: "comments",
                column: "ProductionId",
                principalTable: "productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_downloads_aspnetusers_UserId",
                table: "downloads",
                column: "UserId",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_downloads_productionfiles_ProductionFileId",
                table: "downloads",
                column: "ProductionFileId",
                principalTable: "productionfiles",
                principalColumn: "ProductionFileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_groups_aspnetusers_AddedById",
                table: "groups",
                column: "AddedById",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_groups_countries_CountryId",
                table: "groups",
                column: "CountryId",
                principalTable: "countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_guestbookentries_aspnetusers_UserId",
                table: "guestbookentries",
                column: "UserId",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_hiddenparts_productions_ProductionId",
                table: "hiddenparts",
                column: "ProductionId",
                principalTable: "productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_historyrecords_aspnetusers_UserId",
                table: "historyrecords",
                column: "UserId",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_historyrecords_groups_AffectedGroupId",
                table: "historyrecords",
                column: "AffectedGroupId",
                principalTable: "groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_historyrecords_parties_AffectedPartyId",
                table: "historyrecords",
                column: "AffectedPartyId",
                principalTable: "parties",
                principalColumn: "PartyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_historyrecords_productions_AffectedProductionId",
                table: "historyrecords",
                column: "AffectedProductionId",
                principalTable: "productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_historyrecords_sceners_AffectedScenerId",
                table: "historyrecords",
                column: "AffectedScenerId",
                principalTable: "sceners",
                principalColumn: "ScenerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_links_linkcategories_LinkCategoryId",
                table: "links",
                column: "LinkCategoryId",
                principalTable: "linkcategories",
                principalColumn: "LinkCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_parties_countries_CountryId",
                table: "parties",
                column: "CountryId",
                principalTable: "countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_partiesgroups_groups_GroupId",
                table: "partiesgroups",
                column: "GroupId",
                principalTable: "groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_partiesgroups_parties_PartyId",
                table: "partiesgroups",
                column: "PartyId",
                principalTable: "parties",
                principalColumn: "PartyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_partiessceners_groups_GroupId",
                table: "partiessceners",
                column: "GroupId",
                principalTable: "groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_partiessceners_parties_PartyId",
                table: "partiessceners",
                column: "PartyId",
                principalTable: "parties",
                principalColumn: "PartyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_partiessceners_sceners_ScenerId",
                table: "partiessceners",
                column: "ScenerId",
                principalTable: "sceners",
                principalColumn: "ScenerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productioncredits_productions_ProductionId",
                table: "productioncredits",
                column: "ProductionId",
                principalTable: "productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productioncredits_sceners_ScenerId",
                table: "productioncredits",
                column: "ScenerId",
                principalTable: "sceners",
                principalColumn: "ScenerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productionfiles_productions_ProductionId",
                table: "productionfiles",
                column: "ProductionId",
                principalTable: "productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productionpictures_productions_ProductionId",
                table: "productionpictures",
                column: "ProductionId",
                principalTable: "productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productions_aspnetusers_SubmitterUserId",
                table: "productions",
                column: "SubmitterUserId",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_productions_aspnetusers_UserId",
                table: "productions",
                column: "UserId",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_productionsgroups_groups_GroupId",
                table: "productionsgroups",
                column: "GroupId",
                principalTable: "groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productionsgroups_productions_ProductionId",
                table: "productionsgroups",
                column: "ProductionId",
                principalTable: "productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productionsparties_parties_PartyId",
                table: "productionsparties",
                column: "PartyId",
                principalTable: "parties",
                principalColumn: "PartyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productionsparties_partycategories_PartyCategoryId",
                table: "productionsparties",
                column: "PartyCategoryId",
                principalTable: "partycategories",
                principalColumn: "PartyCategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_productionsparties_productions_ProductionId",
                table: "productionsparties",
                column: "ProductionId",
                principalTable: "productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productionvideos_productions_ProductionId",
                table: "productionvideos",
                column: "ProductionId",
                principalTable: "productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_aspnetusers_UserId",
                table: "ratings",
                column: "UserId",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_productions_ProductionId",
                table: "ratings",
                column: "ProductionId",
                principalTable: "productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_scenergroupjob_scenersgroups_ScenersGroupsId",
                table: "scenergroupjob",
                column: "ScenersGroupsId",
                principalTable: "scenersgroups",
                principalColumn: "ScenersGroupsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_sceners_countries_CountryId",
                table: "sceners",
                column: "CountryId",
                principalTable: "countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_scenersceners_sceners_ScenerToId",
                table: "scenersceners",
                column: "ScenerToId",
                principalTable: "sceners",
                principalColumn: "ScenerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_scenersgroups_groups_GroupId",
                table: "scenersgroups",
                column: "GroupId",
                principalTable: "groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_scenersgroups_sceners_ScenerId",
                table: "scenersgroups",
                column: "ScenerId",
                principalTable: "sceners",
                principalColumn: "ScenerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_scenersjobs_sceners_ScenerId",
                table: "scenersjobs",
                column: "ScenerId",
                principalTable: "sceners",
                principalColumn: "ScenerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_siteinfos_aspnetusers_UserId",
                table: "siteinfos",
                column: "UserId",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_userfavorites_aspnetusers_UserId",
                table: "userfavorites",
                column: "UserId",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userfavorites_productions_ProductionId",
                table: "userfavorites",
                column: "ProductionId",
                principalTable: "productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aspnetroleclaims_aspnetroles_RoleId",
                table: "aspnetroleclaims");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserclaims_aspnetusers_UserId",
                table: "aspnetuserclaims");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserlogins_aspnetusers_UserId",
                table: "aspnetuserlogins");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserroles_aspnetroles_RoleId",
                table: "aspnetuserroles");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserroles_aspnetusers_UserId",
                table: "aspnetuserroles");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetusers_countries_CountryId",
                table: "aspnetusers");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetusers_sceners_ScenerId",
                table: "aspnetusers");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetusertokens_aspnetusers_UserId",
                table: "aspnetusertokens");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_aspnetusers_UserId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_productions_ProductionId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_downloads_aspnetusers_UserId",
                table: "downloads");

            migrationBuilder.DropForeignKey(
                name: "FK_downloads_productionfiles_ProductionFileId",
                table: "downloads");

            migrationBuilder.DropForeignKey(
                name: "FK_groups_aspnetusers_AddedById",
                table: "groups");

            migrationBuilder.DropForeignKey(
                name: "FK_groups_countries_CountryId",
                table: "groups");

            migrationBuilder.DropForeignKey(
                name: "FK_guestbookentries_aspnetusers_UserId",
                table: "guestbookentries");

            migrationBuilder.DropForeignKey(
                name: "FK_hiddenparts_productions_ProductionId",
                table: "hiddenparts");

            migrationBuilder.DropForeignKey(
                name: "FK_historyrecords_aspnetusers_UserId",
                table: "historyrecords");

            migrationBuilder.DropForeignKey(
                name: "FK_historyrecords_groups_AffectedGroupId",
                table: "historyrecords");

            migrationBuilder.DropForeignKey(
                name: "FK_historyrecords_parties_AffectedPartyId",
                table: "historyrecords");

            migrationBuilder.DropForeignKey(
                name: "FK_historyrecords_productions_AffectedProductionId",
                table: "historyrecords");

            migrationBuilder.DropForeignKey(
                name: "FK_historyrecords_sceners_AffectedScenerId",
                table: "historyrecords");

            migrationBuilder.DropForeignKey(
                name: "FK_links_linkcategories_LinkCategoryId",
                table: "links");

            migrationBuilder.DropForeignKey(
                name: "FK_parties_countries_CountryId",
                table: "parties");

            migrationBuilder.DropForeignKey(
                name: "FK_partiesgroups_groups_GroupId",
                table: "partiesgroups");

            migrationBuilder.DropForeignKey(
                name: "FK_partiesgroups_parties_PartyId",
                table: "partiesgroups");

            migrationBuilder.DropForeignKey(
                name: "FK_partiessceners_groups_GroupId",
                table: "partiessceners");

            migrationBuilder.DropForeignKey(
                name: "FK_partiessceners_parties_PartyId",
                table: "partiessceners");

            migrationBuilder.DropForeignKey(
                name: "FK_partiessceners_sceners_ScenerId",
                table: "partiessceners");

            migrationBuilder.DropForeignKey(
                name: "FK_productioncredits_productions_ProductionId",
                table: "productioncredits");

            migrationBuilder.DropForeignKey(
                name: "FK_productioncredits_sceners_ScenerId",
                table: "productioncredits");

            migrationBuilder.DropForeignKey(
                name: "FK_productionfiles_productions_ProductionId",
                table: "productionfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_productionpictures_productions_ProductionId",
                table: "productionpictures");

            migrationBuilder.DropForeignKey(
                name: "FK_productions_aspnetusers_SubmitterUserId",
                table: "productions");

            migrationBuilder.DropForeignKey(
                name: "FK_productions_aspnetusers_UserId",
                table: "productions");

            migrationBuilder.DropForeignKey(
                name: "FK_productionsgroups_groups_GroupId",
                table: "productionsgroups");

            migrationBuilder.DropForeignKey(
                name: "FK_productionsgroups_productions_ProductionId",
                table: "productionsgroups");

            migrationBuilder.DropForeignKey(
                name: "FK_productionsparties_parties_PartyId",
                table: "productionsparties");

            migrationBuilder.DropForeignKey(
                name: "FK_productionsparties_partycategories_PartyCategoryId",
                table: "productionsparties");

            migrationBuilder.DropForeignKey(
                name: "FK_productionsparties_productions_ProductionId",
                table: "productionsparties");

            migrationBuilder.DropForeignKey(
                name: "FK_productionvideos_productions_ProductionId",
                table: "productionvideos");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_aspnetusers_UserId",
                table: "ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_productions_ProductionId",
                table: "ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_scenergroupjob_scenersgroups_ScenersGroupsId",
                table: "scenergroupjob");

            migrationBuilder.DropForeignKey(
                name: "FK_sceners_countries_CountryId",
                table: "sceners");

            migrationBuilder.DropForeignKey(
                name: "FK_scenersceners_sceners_ScenerToId",
                table: "scenersceners");

            migrationBuilder.DropForeignKey(
                name: "FK_scenersgroups_groups_GroupId",
                table: "scenersgroups");

            migrationBuilder.DropForeignKey(
                name: "FK_scenersgroups_sceners_ScenerId",
                table: "scenersgroups");

            migrationBuilder.DropForeignKey(
                name: "FK_scenersjobs_sceners_ScenerId",
                table: "scenersjobs");

            migrationBuilder.DropForeignKey(
                name: "FK_siteinfos_aspnetusers_UserId",
                table: "siteinfos");

            migrationBuilder.DropForeignKey(
                name: "FK_userfavorites_aspnetusers_UserId",
                table: "userfavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_userfavorites_productions_ProductionId",
                table: "userfavorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userfavorites",
                table: "userfavorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tools",
                table: "tools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_siteinfos",
                table: "siteinfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_scenersjobs",
                table: "scenersjobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_scenersgroups",
                table: "scenersgroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_scenersceners",
                table: "scenersceners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sceners",
                table: "sceners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_scenergroupjob",
                table: "scenergroupjob");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ratings",
                table: "ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productionvideos",
                table: "productionvideos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productionsparties",
                table: "productionsparties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productionsgroups",
                table: "productionsgroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productions",
                table: "productions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productionpictures",
                table: "productionpictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productionfiles",
                table: "productionfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productioncredits",
                table: "productioncredits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_partycategories",
                table: "partycategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_partiessceners",
                table: "partiessceners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_partiesgroups",
                table: "partiesgroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_parties",
                table: "parties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_links",
                table: "links");

            migrationBuilder.DropPrimaryKey(
                name: "PK_linkcategories",
                table: "linkcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_historyrecords",
                table: "historyrecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hiddenparts",
                table: "hiddenparts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_guestbookentries",
                table: "guestbookentries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_groups",
                table: "groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_downloads",
                table: "downloads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbfiles",
                table: "dbfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_countries",
                table: "countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_comments",
                table: "comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetusertokens",
                table: "aspnetusertokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetusers",
                table: "aspnetusers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetuserroles",
                table: "aspnetuserroles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetuserlogins",
                table: "aspnetuserlogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetuserclaims",
                table: "aspnetuserclaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetroles",
                table: "aspnetroles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetroleclaims",
                table: "aspnetroleclaims");

            migrationBuilder.RenameTable(
                name: "userfavorites",
                newName: "UserFavorites");

            migrationBuilder.RenameTable(
                name: "tools",
                newName: "Tools");

            migrationBuilder.RenameTable(
                name: "siteinfos",
                newName: "SiteInfos");

            migrationBuilder.RenameTable(
                name: "scenersjobs",
                newName: "ScenersJobs");

            migrationBuilder.RenameTable(
                name: "scenersgroups",
                newName: "ScenersGroups");

            migrationBuilder.RenameTable(
                name: "scenersceners",
                newName: "ScenerSceners");

            migrationBuilder.RenameTable(
                name: "sceners",
                newName: "Sceners");

            migrationBuilder.RenameTable(
                name: "scenergroupjob",
                newName: "ScenerGroupJob");

            migrationBuilder.RenameTable(
                name: "ratings",
                newName: "Ratings");

            migrationBuilder.RenameTable(
                name: "productionvideos",
                newName: "ProductionVideos");

            migrationBuilder.RenameTable(
                name: "productionsparties",
                newName: "ProductionsParties");

            migrationBuilder.RenameTable(
                name: "productionsgroups",
                newName: "ProductionsGroups");

            migrationBuilder.RenameTable(
                name: "productions",
                newName: "Productions");

            migrationBuilder.RenameTable(
                name: "productionpictures",
                newName: "ProductionPictures");

            migrationBuilder.RenameTable(
                name: "productionfiles",
                newName: "ProductionFiles");

            migrationBuilder.RenameTable(
                name: "productioncredits",
                newName: "ProductionCredits");

            migrationBuilder.RenameTable(
                name: "partycategories",
                newName: "PartyCategories");

            migrationBuilder.RenameTable(
                name: "partiessceners",
                newName: "PartiesSceners");

            migrationBuilder.RenameTable(
                name: "partiesgroups",
                newName: "PartiesGroups");

            migrationBuilder.RenameTable(
                name: "parties",
                newName: "Parties");

            migrationBuilder.RenameTable(
                name: "links",
                newName: "Links");

            migrationBuilder.RenameTable(
                name: "linkcategories",
                newName: "LinkCategories");

            migrationBuilder.RenameTable(
                name: "historyrecords",
                newName: "HistoryRecords");

            migrationBuilder.RenameTable(
                name: "hiddenparts",
                newName: "HiddenParts");

            migrationBuilder.RenameTable(
                name: "guestbookentries",
                newName: "GuestbookEntries");

            migrationBuilder.RenameTable(
                name: "groups",
                newName: "Groups");

            migrationBuilder.RenameTable(
                name: "downloads",
                newName: "Downloads");

            migrationBuilder.RenameTable(
                name: "dbfiles",
                newName: "DbFiles");

            migrationBuilder.RenameTable(
                name: "countries",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "comments",
                newName: "Comments");

            migrationBuilder.RenameTable(
                name: "aspnetusertokens",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "aspnetusers",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "aspnetuserroles",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "aspnetuserlogins",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "aspnetuserclaims",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "aspnetroles",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "aspnetroleclaims",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameIndex(
                name: "IX_userfavorites_ProductionId",
                table: "UserFavorites",
                newName: "IX_UserFavorites_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_tools_Show",
                table: "Tools",
                newName: "IX_Tools_Show");

            migrationBuilder.RenameIndex(
                name: "IX_siteinfos_UserId",
                table: "SiteInfos",
                newName: "IX_SiteInfos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_scenersjobs_ScenerId",
                table: "ScenersJobs",
                newName: "IX_ScenersJobs_ScenerId");

            migrationBuilder.RenameIndex(
                name: "IX_scenersgroups_ScenerId",
                table: "ScenersGroups",
                newName: "IX_ScenersGroups_ScenerId");

            migrationBuilder.RenameIndex(
                name: "IX_scenersgroups_GroupId",
                table: "ScenersGroups",
                newName: "IX_ScenersGroups_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_scenersceners_ScenerToId",
                table: "ScenerSceners",
                newName: "IX_ScenerSceners_ScenerToId");

            migrationBuilder.RenameIndex(
                name: "IX_sceners_CountryId",
                table: "Sceners",
                newName: "IX_Sceners_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_scenergroupjob_ScenersGroupsId",
                table: "ScenerGroupJob",
                newName: "IX_ScenerGroupJob_ScenersGroupsId");

            migrationBuilder.RenameIndex(
                name: "IX_ratings_UserId",
                table: "Ratings",
                newName: "IX_Ratings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ratings_ProductionId",
                table: "Ratings",
                newName: "IX_Ratings_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_productionvideos_ProductionId",
                table: "ProductionVideos",
                newName: "IX_ProductionVideos_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_productionsparties_PartyId",
                table: "ProductionsParties",
                newName: "IX_ProductionsParties_PartyId");

            migrationBuilder.RenameIndex(
                name: "IX_productionsparties_PartyCategoryId",
                table: "ProductionsParties",
                newName: "IX_ProductionsParties_PartyCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_productionsgroups_GroupId",
                table: "ProductionsGroups",
                newName: "IX_ProductionsGroups_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_productions_UserId",
                table: "Productions",
                newName: "IX_Productions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_productions_SubmitterUserId",
                table: "Productions",
                newName: "IX_Productions_SubmitterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_productionpictures_ProductionId",
                table: "ProductionPictures",
                newName: "IX_ProductionPictures_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_productionfiles_ProductionId",
                table: "ProductionFiles",
                newName: "IX_ProductionFiles_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_productioncredits_ScenerId",
                table: "ProductionCredits",
                newName: "IX_ProductionCredits_ScenerId");

            migrationBuilder.RenameIndex(
                name: "IX_productioncredits_ProductionId",
                table: "ProductionCredits",
                newName: "IX_ProductionCredits_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_partiessceners_ScenerId",
                table: "PartiesSceners",
                newName: "IX_PartiesSceners_ScenerId");

            migrationBuilder.RenameIndex(
                name: "IX_partiessceners_GroupId",
                table: "PartiesSceners",
                newName: "IX_PartiesSceners_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_partiesgroups_GroupId",
                table: "PartiesGroups",
                newName: "IX_PartiesGroups_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_parties_CountryId",
                table: "Parties",
                newName: "IX_Parties_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_links_LinkCategoryId",
                table: "Links",
                newName: "IX_Links_LinkCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_historyrecords_UserId",
                table: "HistoryRecords",
                newName: "IX_HistoryRecords_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_historyrecords_AffectedScenerId",
                table: "HistoryRecords",
                newName: "IX_HistoryRecords_AffectedScenerId");

            migrationBuilder.RenameIndex(
                name: "IX_historyrecords_AffectedProductionId",
                table: "HistoryRecords",
                newName: "IX_HistoryRecords_AffectedProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_historyrecords_AffectedPartyId",
                table: "HistoryRecords",
                newName: "IX_HistoryRecords_AffectedPartyId");

            migrationBuilder.RenameIndex(
                name: "IX_historyrecords_AffectedGroupId",
                table: "HistoryRecords",
                newName: "IX_HistoryRecords_AffectedGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_hiddenparts_ProductionId",
                table: "HiddenParts",
                newName: "IX_HiddenParts_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_guestbookentries_UserId",
                table: "GuestbookEntries",
                newName: "IX_GuestbookEntries_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_groups_CountryId",
                table: "Groups",
                newName: "IX_Groups_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_groups_AddedById",
                table: "Groups",
                newName: "IX_Groups_AddedById");

            migrationBuilder.RenameIndex(
                name: "IX_downloads_UserId",
                table: "Downloads",
                newName: "IX_Downloads_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_downloads_ProductionFileId_DownloadedOn",
                table: "Downloads",
                newName: "IX_Downloads_ProductionFileId_DownloadedOn");

            migrationBuilder.RenameIndex(
                name: "IX_downloads_ProductionFileId",
                table: "Downloads",
                newName: "IX_Downloads_ProductionFileId");

            migrationBuilder.RenameIndex(
                name: "IX_downloads_DownloadedOn",
                table: "Downloads",
                newName: "IX_Downloads_DownloadedOn");

            migrationBuilder.RenameIndex(
                name: "IX_dbfiles_Container_FileName",
                table: "DbFiles",
                newName: "IX_DbFiles_Container_FileName");

            migrationBuilder.RenameIndex(
                name: "IX_comments_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_comments_ProductionId",
                table: "Comments",
                newName: "IX_Comments_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetusers_ScenerId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_ScenerId");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetusers_CountryId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetuserroles_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetuserlogins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetuserclaims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetroleclaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFavorites",
                table: "UserFavorites",
                columns: new[] { "UserId", "ProductionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tools",
                table: "Tools",
                column: "ToolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SiteInfos",
                table: "SiteInfos",
                column: "SiteInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScenersJobs",
                table: "ScenersJobs",
                column: "ScenersJobsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScenersGroups",
                table: "ScenersGroups",
                column: "ScenersGroupsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScenerSceners",
                table: "ScenerSceners",
                columns: new[] { "ScenerId", "ScenerToId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sceners",
                table: "Sceners",
                column: "ScenerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScenerGroupJob",
                table: "ScenerGroupJob",
                column: "ScenerGroupJobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "RatingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionVideos",
                table: "ProductionVideos",
                column: "ProductionVideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionsParties",
                table: "ProductionsParties",
                columns: new[] { "ProductionId", "PartyId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionsGroups",
                table: "ProductionsGroups",
                columns: new[] { "ProductionId", "GroupId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productions",
                table: "Productions",
                column: "ProductionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionPictures",
                table: "ProductionPictures",
                column: "ProductionPictureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionFiles",
                table: "ProductionFiles",
                column: "ProductionFileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionCredits",
                table: "ProductionCredits",
                column: "ProductionCreditId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartyCategories",
                table: "PartyCategories",
                column: "PartyCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartiesSceners",
                table: "PartiesSceners",
                columns: new[] { "PartyId", "ScenerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartiesGroups",
                table: "PartiesGroups",
                columns: new[] { "PartyId", "GroupId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parties",
                table: "Parties",
                column: "PartyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Links",
                table: "Links",
                column: "LinkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LinkCategories",
                table: "LinkCategories",
                column: "LinkCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryRecords",
                table: "HistoryRecords",
                column: "HistoryRecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HiddenParts",
                table: "HiddenParts",
                column: "HiddenPartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuestbookEntries",
                table: "GuestbookEntries",
                column: "GuestbookEntryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Downloads",
                table: "Downloads",
                column: "DownloadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbFiles",
                table: "DbFiles",
                column: "DbFileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Countries_CountryId",
                table: "AspNetUsers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Sceners_ScenerId",
                table: "AspNetUsers",
                column: "ScenerId",
                principalTable: "Sceners",
                principalColumn: "ScenerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Productions_ProductionId",
                table: "Comments",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Downloads_AspNetUsers_UserId",
                table: "Downloads",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Downloads_ProductionFiles_ProductionFileId",
                table: "Downloads",
                column: "ProductionFileId",
                principalTable: "ProductionFiles",
                principalColumn: "ProductionFileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_AddedById",
                table: "Groups",
                column: "AddedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Countries_CountryId",
                table: "Groups",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GuestbookEntries_AspNetUsers_UserId",
                table: "GuestbookEntries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HiddenParts_Productions_ProductionId",
                table: "HiddenParts",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryRecords_AspNetUsers_UserId",
                table: "HistoryRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryRecords_Groups_AffectedGroupId",
                table: "HistoryRecords",
                column: "AffectedGroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryRecords_Parties_AffectedPartyId",
                table: "HistoryRecords",
                column: "AffectedPartyId",
                principalTable: "Parties",
                principalColumn: "PartyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryRecords_Productions_AffectedProductionId",
                table: "HistoryRecords",
                column: "AffectedProductionId",
                principalTable: "Productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryRecords_Sceners_AffectedScenerId",
                table: "HistoryRecords",
                column: "AffectedScenerId",
                principalTable: "Sceners",
                principalColumn: "ScenerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Links_LinkCategories_LinkCategoryId",
                table: "Links",
                column: "LinkCategoryId",
                principalTable: "LinkCategories",
                principalColumn: "LinkCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Countries_CountryId",
                table: "Parties",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartiesGroups_Groups_GroupId",
                table: "PartiesGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartiesGroups_Parties_PartyId",
                table: "PartiesGroups",
                column: "PartyId",
                principalTable: "Parties",
                principalColumn: "PartyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartiesSceners_Groups_GroupId",
                table: "PartiesSceners",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartiesSceners_Parties_PartyId",
                table: "PartiesSceners",
                column: "PartyId",
                principalTable: "Parties",
                principalColumn: "PartyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartiesSceners_Sceners_ScenerId",
                table: "PartiesSceners",
                column: "ScenerId",
                principalTable: "Sceners",
                principalColumn: "ScenerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionCredits_Productions_ProductionId",
                table: "ProductionCredits",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionCredits_Sceners_ScenerId",
                table: "ProductionCredits",
                column: "ScenerId",
                principalTable: "Sceners",
                principalColumn: "ScenerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionFiles_Productions_ProductionId",
                table: "ProductionFiles",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionPictures_Productions_ProductionId",
                table: "ProductionPictures",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_AspNetUsers_SubmitterUserId",
                table: "Productions",
                column: "SubmitterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_AspNetUsers_UserId",
                table: "Productions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionsGroups_Groups_GroupId",
                table: "ProductionsGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionsGroups_Productions_ProductionId",
                table: "ProductionsGroups",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionsParties_Parties_PartyId",
                table: "ProductionsParties",
                column: "PartyId",
                principalTable: "Parties",
                principalColumn: "PartyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionsParties_PartyCategories_PartyCategoryId",
                table: "ProductionsParties",
                column: "PartyCategoryId",
                principalTable: "PartyCategories",
                principalColumn: "PartyCategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionsParties_Productions_ProductionId",
                table: "ProductionsParties",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionVideos_Productions_ProductionId",
                table: "ProductionVideos",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId",
                table: "Ratings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Productions_ProductionId",
                table: "Ratings",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScenerGroupJob_ScenersGroups_ScenersGroupsId",
                table: "ScenerGroupJob",
                column: "ScenersGroupsId",
                principalTable: "ScenersGroups",
                principalColumn: "ScenersGroupsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sceners_Countries_CountryId",
                table: "Sceners",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScenerSceners_Sceners_ScenerToId",
                table: "ScenerSceners",
                column: "ScenerToId",
                principalTable: "Sceners",
                principalColumn: "ScenerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScenersGroups_Groups_GroupId",
                table: "ScenersGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScenersGroups_Sceners_ScenerId",
                table: "ScenersGroups",
                column: "ScenerId",
                principalTable: "Sceners",
                principalColumn: "ScenerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScenersJobs_Sceners_ScenerId",
                table: "ScenersJobs",
                column: "ScenerId",
                principalTable: "Sceners",
                principalColumn: "ScenerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteInfos_AspNetUsers_UserId",
                table: "SiteInfos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorites_AspNetUsers_UserId",
                table: "UserFavorites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorites_Productions_ProductionId",
                table: "UserFavorites",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "ProductionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
