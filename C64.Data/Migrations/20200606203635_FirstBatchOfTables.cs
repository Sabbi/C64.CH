using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class FirstBatchOfTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScenerId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DbFiles",
                columns: table => new
                {
                    DbFileId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Container = table.Column<string>(maxLength: 127, nullable: false),
                    FileName = table.Column<string>(maxLength: 254, nullable: false),
                    Size = table.Column<long>(nullable: false),
                    Data = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbFiles", x => x.DbFileId);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Aka = table.Column<string>(maxLength: 255, nullable: true),
                    LogoFile = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 65535, nullable: true),
                    Url = table.Column<string>(maxLength: 255, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    FoundedDate = table.Column<DateTime>(nullable: false),
                    FoundedDateType = table.Column<int>(nullable: false),
                    ClosedDate = table.Column<DateTime>(nullable: false),
                    ClosedDateType = table.Column<int>(nullable: false),
                    Added = table.Column<DateTime>(nullable: false),
                    AddedById = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Groups_AspNetUsers_AddedById",
                        column: x => x.AddedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GuestbookEntries",
                columns: table => new
                {
                    GuestbookEntryId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    Homepage = table.Column<string>(maxLength: 255, nullable: true),
                    Comment = table.Column<string>(maxLength: 65535, nullable: true),
                    WrittenAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(maxLength: 36, nullable: true),
                    Ip = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestbookEntries", x => x.GuestbookEntryId);
                    table.ForeignKey(
                        name: "FK_GuestbookEntries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LinkCategories",
                columns: table => new
                {
                    LinkCategoryId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 63, nullable: true),
                    Selectable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkCategories", x => x.LinkCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    PartyId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 65535, nullable: true),
                    Url = table.Column<string>(maxLength: 255, nullable: true),
                    Email = table.Column<string>(maxLength: 63, nullable: true),
                    Organizers = table.Column<string>(maxLength: 1023, nullable: true),
                    Location = table.Column<string>(maxLength: 255, nullable: true),
                    CountryId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.PartyId);
                    table.ForeignKey(
                        name: "FK_Parties_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Productions",
                columns: table => new
                {
                    ProductionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Aka = table.Column<string>(maxLength: 255, nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    ReleaseDateType = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 65535, nullable: true),
                    Added = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(maxLength: 36, nullable: true),
                    Uploader = table.Column<string>(maxLength: 255, nullable: true),
                    Submitter = table.Column<string>(maxLength: 255, nullable: true),
                    SubmitterUserId = table.Column<string>(nullable: true),
                    TopCategory = table.Column<int>(nullable: false),
                    SubCategory = table.Column<int>(nullable: false),
                    VideoType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productions", x => x.ProductionId);
                    table.ForeignKey(
                        name: "FK_Productions_AspNetUsers_SubmitterUserId",
                        column: x => x.SubmitterUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sceners",
                columns: table => new
                {
                    ScenerId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Handle = table.Column<string>(nullable: true),
                    RealName = table.Column<string>(maxLength: 255, nullable: true),
                    ShowRealName = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sceners", x => x.ScenerId);
                });

            migrationBuilder.CreateTable(
                name: "SiteInfos",
                columns: table => new
                {
                    SiteInfoId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PublishedAt = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(maxLength: 65535, nullable: true),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    Writer = table.Column<string>(maxLength: 255, nullable: true),
                    Show = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteInfos", x => x.SiteInfoId);
                    table.ForeignKey(
                        name: "FK_SiteInfos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    LinkId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Url = table.Column<string>(maxLength: 255, nullable: true),
                    LinkCategoryId = table.Column<int>(nullable: false),
                    Added = table.Column<DateTime>(nullable: false),
                    Hits = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.LinkId);
                    table.ForeignKey(
                        name: "FK_Links_LinkCategories_LinkCategoryId",
                        column: x => x.LinkCategoryId,
                        principalTable: "LinkCategories",
                        principalColumn: "LinkCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartiesGroups",
                columns: table => new
                {
                    PartyId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartiesGroups", x => new { x.PartyId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_PartiesGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartiesGroups_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductionId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(maxLength: 36, nullable: true),
                    Text = table.Column<string>(maxLength: 65535, nullable: true),
                    Ip = table.Column<string>(maxLength: 40, nullable: true),
                    CommentedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "ProductionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HiddenParts",
                columns: table => new
                {
                    HiddenPartId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductionId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 65535, nullable: true),
                    SubmittedById = table.Column<string>(maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiddenParts", x => x.HiddenPartId);
                    table.ForeignKey(
                        name: "FK_HiddenParts_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "ProductionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HiddenParts_AspNetUsers_SubmittedById",
                        column: x => x.SubmittedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionFiles",
                columns: table => new
                {
                    ProductionFileId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductionId = table.Column<int>(nullable: false),
                    Filename = table.Column<string>(maxLength: 1023, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Downloads = table.Column<int>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    Show = table.Column<bool>(nullable: false),
                    Size = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionFiles", x => x.ProductionFileId);
                    table.ForeignKey(
                        name: "FK_ProductionFiles_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "ProductionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionPictures",
                columns: table => new
                {
                    ProductionPictureId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductionId = table.Column<int>(nullable: false),
                    Filename = table.Column<string>(maxLength: 511, nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    Show = table.Column<bool>(nullable: false),
                    Size = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionPictures", x => x.ProductionPictureId);
                    table.ForeignKey(
                        name: "FK_ProductionPictures_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "ProductionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionsGroups",
                columns: table => new
                {
                    ProductionId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionsGroups", x => new { x.ProductionId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_ProductionsGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionsGroups_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "ProductionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionsParties",
                columns: table => new
                {
                    ProductionId = table.Column<int>(nullable: false),
                    PartyId = table.Column<int>(nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    Category = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionsParties", x => new { x.ProductionId, x.PartyId });
                    table.ForeignKey(
                        name: "FK_ProductionsParties_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionsParties_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "ProductionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionStat",
                columns: table => new
                {
                    ProductionStatId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Downloads = table.Column<int>(nullable: false),
                    NumberOfRatings = table.Column<int>(nullable: false),
                    SumOfRatings = table.Column<int>(nullable: false),
                    AverageRating = table.Column<decimal>(nullable: false),
                    Views = table.Column<int>(nullable: false),
                    ProductionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionStat", x => x.ProductionStatId);
                    table.ForeignKey(
                        name: "FK_ProductionStat_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "ProductionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionVideos",
                columns: table => new
                {
                    ProductionVideoId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VideoProvider = table.Column<int>(nullable: false),
                    VideoId = table.Column<string>(nullable: true),
                    ProductionId = table.Column<int>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    Show = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionVideos", x => x.ProductionVideoId);
                    table.ForeignKey(
                        name: "FK_ProductionVideos_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "ProductionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductionId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(maxLength: 36, nullable: true),
                    Value = table.Column<int>(nullable: false),
                    Ip = table.Column<string>(maxLength: 40, nullable: true),
                    RatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Ratings_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "ProductionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserFavorites",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ProductionId = table.Column<int>(nullable: false),
                    Added = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavorites", x => new { x.UserId, x.ProductionId });
                    table.ForeignKey(
                        name: "FK_UserFavorites_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "ProductionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartiesSceners",
                columns: table => new
                {
                    PartyId = table.Column<int>(nullable: false),
                    ScenerId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartiesSceners", x => new { x.PartyId, x.ScenerId });
                    table.ForeignKey(
                        name: "FK_PartiesSceners_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartiesSceners_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartiesSceners_Sceners_ScenerId",
                        column: x => x.ScenerId,
                        principalTable: "Sceners",
                        principalColumn: "ScenerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionCredits",
                columns: table => new
                {
                    ProductionCreditId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductionId = table.Column<int>(nullable: false),
                    ScenerId = table.Column<int>(nullable: false),
                    Credit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionCredits", x => x.ProductionCreditId);
                    table.ForeignKey(
                        name: "FK_ProductionCredits_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "ProductionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionCredits_Sceners_ScenerId",
                        column: x => x.ScenerId,
                        principalTable: "Sceners",
                        principalColumn: "ScenerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScenerSceners",
                columns: table => new
                {
                    ScenerId = table.Column<int>(nullable: false),
                    ScenerToId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScenerSceners", x => new { x.ScenerId, x.ScenerToId });
                    table.ForeignKey(
                        name: "FK_ScenerSceners_Sceners_ScenerToId",
                        column: x => x.ScenerToId,
                        principalTable: "Sceners",
                        principalColumn: "ScenerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScenersGroups",
                columns: table => new
                {
                    ScenersGroupsId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ScenerId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    ValidFrom = table.Column<DateTime>(nullable: false),
                    ValidFromType = table.Column<int>(nullable: false),
                    ValidTo = table.Column<DateTime>(nullable: false),
                    ValidToType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScenersGroups", x => x.ScenersGroupsId);
                    table.ForeignKey(
                        name: "FK_ScenersGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScenersGroups_Sceners_ScenerId",
                        column: x => x.ScenerId,
                        principalTable: "Sceners",
                        principalColumn: "ScenerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScenersJobs",
                columns: table => new
                {
                    ScenersJobsId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ScenerId = table.Column<int>(nullable: false),
                    Job = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScenersJobs", x => x.ScenersJobsId);
                    table.ForeignKey(
                        name: "FK_ScenersJobs_Sceners_ScenerId",
                        column: x => x.ScenerId,
                        principalTable: "Sceners",
                        principalColumn: "ScenerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Downloads",
                columns: table => new
                {
                    DownloadId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductionFileId = table.Column<int>(nullable: false),
                    DownloadedOn = table.Column<DateTime>(nullable: false),
                    Ip = table.Column<string>(maxLength: 40, nullable: true),
                    UserId = table.Column<string>(maxLength: 36, nullable: true),
                    Referer = table.Column<string>(maxLength: 2047, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Downloads", x => x.DownloadId);
                    table.ForeignKey(
                        name: "FK_Downloads_ProductionFiles_ProductionFileId",
                        column: x => x.ProductionFileId,
                        principalTable: "ProductionFiles",
                        principalColumn: "ProductionFileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Downloads_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScenerGroupJob",
                columns: table => new
                {
                    ScenerGroupJobId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ScenerGroupId = table.Column<int>(nullable: false),
                    Job = table.Column<int>(nullable: false),
                    ScenersGroupsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScenerGroupJob", x => x.ScenerGroupJobId);
                    table.ForeignKey(
                        name: "FK_ScenerGroupJob_ScenersGroups_ScenersGroupsId",
                        column: x => x.ScenersGroupsId,
                        principalTable: "ScenersGroups",
                        principalColumn: "ScenersGroupsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "LinkCategories",
                columns: new[] { "LinkCategoryId", "Name", "Selectable" },
                values: new object[,]
                {
                    { 1, "General", true },
                    { 2, "Games", true },
                    { 3, "SID", true },
                    { 4, "Group", true },
                    { 5, "Hardware", true },
                    { 6, "Zines", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ScenerId",
                table: "AspNetUsers",
                column: "ScenerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductionId",
                table: "Comments",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DbFiles_Container_FileName",
                table: "DbFiles",
                columns: new[] { "Container", "FileName" });

            migrationBuilder.CreateIndex(
                name: "IX_Downloads_DownloadedOn",
                table: "Downloads",
                column: "DownloadedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Downloads_ProductionFileId",
                table: "Downloads",
                column: "ProductionFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Downloads_UserId",
                table: "Downloads",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_AddedById",
                table: "Groups",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_GuestbookEntries_UserId",
                table: "GuestbookEntries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HiddenParts_ProductionId",
                table: "HiddenParts",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_HiddenParts_SubmittedById",
                table: "HiddenParts",
                column: "SubmittedById");

            migrationBuilder.CreateIndex(
                name: "IX_Links_LinkCategoryId",
                table: "Links",
                column: "LinkCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_CountryId",
                table: "Parties",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PartiesGroups_GroupId",
                table: "PartiesGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PartiesSceners_GroupId",
                table: "PartiesSceners",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PartiesSceners_ScenerId",
                table: "PartiesSceners",
                column: "ScenerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionCredits_ProductionId",
                table: "ProductionCredits",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionCredits_ScenerId",
                table: "ProductionCredits",
                column: "ScenerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionFiles_ProductionId",
                table: "ProductionFiles",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPictures_ProductionId",
                table: "ProductionPictures",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_SubmitterUserId",
                table: "Productions",
                column: "SubmitterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_UserId",
                table: "Productions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionsGroups_GroupId",
                table: "ProductionsGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionsParties_PartyId",
                table: "ProductionsParties",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionStat_ProductionId",
                table: "ProductionStat",
                column: "ProductionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductionVideos_ProductionId",
                table: "ProductionVideos",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ProductionId",
                table: "Ratings",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ScenerGroupJob_ScenersGroupsId",
                table: "ScenerGroupJob",
                column: "ScenersGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_ScenerSceners_ScenerToId",
                table: "ScenerSceners",
                column: "ScenerToId");

            migrationBuilder.CreateIndex(
                name: "IX_ScenersGroups_GroupId",
                table: "ScenersGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ScenersGroups_ScenerId",
                table: "ScenersGroups",
                column: "ScenerId");

            migrationBuilder.CreateIndex(
                name: "IX_ScenersJobs_ScenerId",
                table: "ScenersJobs",
                column: "ScenerId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteInfos_UserId",
                table: "SiteInfos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_ProductionId",
                table: "UserFavorites",
                column: "ProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Sceners_ScenerId",
                table: "AspNetUsers",
                column: "ScenerId",
                principalTable: "Sceners",
                principalColumn: "ScenerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Sceners_ScenerId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "DbFiles");

            migrationBuilder.DropTable(
                name: "Downloads");

            migrationBuilder.DropTable(
                name: "GuestbookEntries");

            migrationBuilder.DropTable(
                name: "HiddenParts");

            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "PartiesGroups");

            migrationBuilder.DropTable(
                name: "PartiesSceners");

            migrationBuilder.DropTable(
                name: "ProductionCredits");

            migrationBuilder.DropTable(
                name: "ProductionPictures");

            migrationBuilder.DropTable(
                name: "ProductionsGroups");

            migrationBuilder.DropTable(
                name: "ProductionsParties");

            migrationBuilder.DropTable(
                name: "ProductionStat");

            migrationBuilder.DropTable(
                name: "ProductionVideos");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "ScenerGroupJob");

            migrationBuilder.DropTable(
                name: "ScenerSceners");

            migrationBuilder.DropTable(
                name: "ScenersJobs");

            migrationBuilder.DropTable(
                name: "SiteInfos");

            migrationBuilder.DropTable(
                name: "UserFavorites");

            migrationBuilder.DropTable(
                name: "ProductionFiles");

            migrationBuilder.DropTable(
                name: "LinkCategories");

            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropTable(
                name: "ScenersGroups");

            migrationBuilder.DropTable(
                name: "Productions");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Sceners");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ScenerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ScenerId",
                table: "AspNetUsers");
        }
    }
}
