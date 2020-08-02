using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace C64.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<string>(maxLength: 2, nullable: false),
                    Name = table.Column<string>(maxLength: 63, nullable: true),
                    Selectable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    OldId = table.Column<int>(nullable: false),
                    Newsletter = table.Column<bool>(nullable: false),
                    Digest = table.Column<bool>(nullable: false),
                    Registered = table.Column<DateTime>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: false),
                    Realname = table.Column<string>(maxLength: 255, nullable: true),
                    PublicEmail = table.Column<string>(nullable: true),
                    Homepage = table.Column<string>(maxLength: 1023, nullable: true),
                    Icq = table.Column<string>(maxLength: 63, nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(maxLength: 255, nullable: true),
                    CountryId = table.Column<string>(maxLength: 2, nullable: true),
                    Occupation = table.Column<string>(maxLength: 255, nullable: true),
                    Groups = table.Column<string>(maxLength: 1023, nullable: true),
                    FavDemos = table.Column<string>(maxLength: 255, nullable: true),
                    FavGroups = table.Column<string>(maxLength: 255, nullable: true),
                    Watching = table.Column<string>(maxLength: 255, nullable: true),
                    Blabla = table.Column<string>(maxLength: 65535, nullable: true),
                    PublicRatings = table.Column<bool>(nullable: false),
                    PublicFavorites = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "Name", "Selectable" },
                values: new object[,]
                {
                    { "AD", "Andorra", true },
                    { "MZ", "Mozambique", true },
                    { "NA", "Namibia", true },
                    { "NC", "New Caledonia", true },
                    { "NE", "Niger", true },
                    { "NF", "Norfolk Island", true },
                    { "NG", "Nigeria", true },
                    { "NI", "Nicaragua", true },
                    { "NL", "Netherlands", true },
                    { "NO", "Norway", true },
                    { "NP", "Nepal", true },
                    { "NR", "Nauru", true },
                    { "NU", "Niue", true },
                    { "MY", "Malaysia", true },
                    { "NZ", "New Zealand", true },
                    { "PA", "Panama", true },
                    { "PE", "Peru", true },
                    { "PF", "French Polynesia", true },
                    { "PG", "Papua New Guinea", true },
                    { "PH", "Philippines", true },
                    { "PK", "Pakistan", true },
                    { "PL", "Poland", true },
                    { "PM", "Saint Pierre And Miquelon", true },
                    { "PN", "Pitcairn", true },
                    { "PR", "Puerto Rico", true },
                    { "PS", "Palestinian Territory, Occupied", true },
                    { "PT", "Portugal", true },
                    { "OM", "Oman", true },
                    { "PW", "Palau", true },
                    { "MX", "Mexico", true },
                    { "MV", "Maldives", true },
                    { "LC", "Saint Lucia", true },
                    { "LI", "Liechtenstein", true },
                    { "LK", "Sri Lanka", true },
                    { "LR", "Liberia", true },
                    { "LS", "Lesotho", true },
                    { "LT", "Lithuania", true },
                    { "LU", "Luxembourg", true },
                    { "LV", "Latvia", true },
                    { "LY", "Libyan Arab Jamahiriya", true },
                    { "MA", "Morocco", true },
                    { "MC", "Monaco", true },
                    { "MD", "Moldova, Republic Of", true },
                    { "MW", "Malawi", true },
                    { "ME", "Montenegro", true },
                    { "MH", "Marshall Islands", true },
                    { "MK", "Macedonia, The Former Yugoslav Republic Of", true },
                    { "ML", "Mali", true },
                    { "MM", "Myanmar", true },
                    { "MN", "Mongolia", true },
                    { "MO", "Macau", true },
                    { "MP", "Northern Mariana Islands", true },
                    { "MQ", "Martinique", true },
                    { "MR", "Mauritania", true },
                    { "MS", "Montserrat", true },
                    { "MT", "Malta", true },
                    { "MU", "Mauritius", true },
                    { "MG", "Madagascar", true },
                    { "PY", "Paraguay", true },
                    { "QA", "Qatar", true },
                    { "RE", "Reunion", true },
                    { "TN", "Tunisia", true },
                    { "TO", "Tonga", true },
                    { "TP", "East Timor", true },
                    { "TR", "Turkey", true },
                    { "TT", "Trinidad And Tobago", true },
                    { "TV", "Tuvalu", true },
                    { "TW", "Taiwan, Province Of China", true },
                    { "TZ", "Tanzania, United Republic Of", true },
                    { "UA", "Ukraine", true },
                    { "UG", "Uganda", true },
                    { "UM", "United States Minor Outlying Islands", true },
                    { "US", "United States", true },
                    { "TM", "Turkmenistan", true },
                    { "UY", "Uruguay", true },
                    { "VA", "Holy See (vatican City State)", true },
                    { "VC", "Saint Vincent And The Grenadines", true },
                    { "VE", "Venezuela", true },
                    { "VG", "Virgin Islands, British", true },
                    { "VI", "Virgin Islands, U.s.", true },
                    { "VN", "Viet Nam", true },
                    { "VU", "Vanuatu", true },
                    { "WF", "Wallis And Futuna", true },
                    { "WS", "Samoa", true },
                    { "YE", "Yemen", true },
                    { "YT", "Mayotte", true },
                    { "ZA", "South Africa", true },
                    { "UZ", "Uzbekistan", true },
                    { "TK", "Tokelau", true },
                    { "TJ", "Tajikistan", true },
                    { "TH", "Thailand", true },
                    { "RO", "Romania", true },
                    { "RS", "Serbia", true },
                    { "RU", "Russian Federation", true },
                    { "RW", "Rwanda", true },
                    { "SA", "Saudi Arabia", true },
                    { "SB", "Solomon Islands", true },
                    { "SC", "Seychelles", true },
                    { "SD", "Sudan", true },
                    { "SE", "Sweden", true },
                    { "SG", "Singapore", true },
                    { "SH", "Saint Helena", true },
                    { "SI", "Slovenia", true },
                    { "SJ", "Svalbard And Jan Mayen", true },
                    { "SK", "Slovakia", true },
                    { "SL", "Sierra Leone", true },
                    { "SM", "San Marino", true },
                    { "SN", "Senegal", true },
                    { "SO", "Somalia", true },
                    { "SR", "Suriname", true },
                    { "ST", "Sao Tome And Principe", true },
                    { "SV", "El Salvador", true },
                    { "SY", "Syrian Arab Republic", true },
                    { "SZ", "Swaziland", true },
                    { "TC", "Turks And Caicos Islands", true },
                    { "TD", "Chad", true },
                    { "TF", "French Southern Territories", true },
                    { "TG", "Togo", true },
                    { "LB", "Lebanon", true },
                    { "ZM", "Zambia", true },
                    { "LA", "Lao Peoples Democratic Republic", true },
                    { "KY", "Cayman Islands", true },
                    { "BV", "Bouvet Island", true },
                    { "BW", "Botswana", true },
                    { "BY", "Belarus", true },
                    { "BZ", "Belize", true },
                    { "CA", "Canada", true },
                    { "CC", "Cocos (keeling) Islands", true },
                    { "CD", "Congo, The Democratic Republic Of The", true },
                    { "CF", "Central African Republic", true },
                    { "CG", "Congo", true },
                    { "CH", "Switzerland", true },
                    { "CI", "Cote Divoire", true },
                    { "CK", "Cook Islands", true },
                    { "BT", "Bhutan", true },
                    { "CL", "Chile", true },
                    { "CN", "China", true },
                    { "CO", "Colombia", true },
                    { "CR", "Costa Rica", true },
                    { "CU", "Cuba", true },
                    { "CV", "Cape Verde", true },
                    { "CX", "Christmas Island", true },
                    { "CY", "Cyprus", true },
                    { "CZ", "Czech Republic", true },
                    { "DE", "Germany", true },
                    { "DJ", "Djibouti", true },
                    { "DK", "Denmark", true },
                    { "DM", "Dominica", true },
                    { "CM", "Cameroon", true },
                    { "DO", "Dominican Republic", true },
                    { "BS", "Bahamas", true },
                    { "BO", "Bolivia", true },
                    { "AE", "United Arab Emirates", true },
                    { "AF", "Afghanistan", true },
                    { "AG", "Antigua And Barbuda", true },
                    { "AI", "Anguilla", true },
                    { "AL", "Albania", true },
                    { "AM", "Armenia", true },
                    { "AN", "Netherlands Antilles", true },
                    { "AO", "Angola", true },
                    { "AQ", "Antarctica", true },
                    { "AR", "Argentina", true },
                    { "AS", "American Samoa", true },
                    { "AT", "Austria", true },
                    { "BR", "Brazil", true },
                    { "AU", "Australia", true },
                    { "AZ", "Azerbaijan", true },
                    { "BA", "Bosnia And Herzegovina", true },
                    { "BB", "Barbados", true },
                    { "BD", "Bangladesh", true },
                    { "BE", "Belgium", true },
                    { "BF", "Burkina Faso", true },
                    { "BG", "Bulgaria", true },
                    { "BH", "Bahrain", true },
                    { "BI", "Burundi", true },
                    { "BJ", "Benin", true },
                    { "BM", "Bermuda", true },
                    { "BN", "Brunei Darussalam", true },
                    { "AW", "Aruba", true },
                    { "DZ", "Algeria", true },
                    { "EC", "Ecuador", true },
                    { "EE", "Estonia", true },
                    { "HN", "Honduras", true },
                    { "HR", "Croatia", true },
                    { "HT", "Haiti", true },
                    { "HU", "Hungary", true },
                    { "ID", "Indonesia", true },
                    { "IE", "Ireland", true },
                    { "IL", "Israel", true },
                    { "IN", "India", true },
                    { "IO", "British Indian Ocean Territory", true },
                    { "IQ", "Iraq", true },
                    { "IR", "Iran, Islamic Republic Of", true },
                    { "IS", "Iceland", true },
                    { "HM", "Heard Island And Mcdonald Islands", true },
                    { "IT", "Italy", true },
                    { "JO", "Jordan", true },
                    { "JP", "Japan", true },
                    { "KE", "Kenya", true },
                    { "KG", "Kyrgyzstan", true },
                    { "KH", "Cambodia", true },
                    { "KI", "Kiribati", true },
                    { "KM", "Comoros", true },
                    { "KN", "Saint Kitts And Nevis", true },
                    { "KP", "Korea, Democratic Peoples Republic Of", true },
                    { "KR", "Korea, Republic Of", true },
                    { "KV", "Kosovo", true },
                    { "KW", "Kuwait", true },
                    { "JM", "Jamaica", true },
                    { "HK", "Hong Kong", true },
                    { "GY", "Guyana", true },
                    { "GW", "Guinea-bissau", true },
                    { "EG", "Egypt", true },
                    { "EH", "Western Sahara", true },
                    { "ER", "Eritrea", true },
                    { "ES", "Spain", true },
                    { "ET", "Ethiopia", true },
                    { "FI", "Finland", true },
                    { "FJ", "Fiji", true },
                    { "FK", "Falkland Islands (malvinas)", true },
                    { "FM", "Micronesia, Federated States Of", true },
                    { "FO", "Faroe Islands", true },
                    { "FR", "France", true },
                    { "GA", "Gabon", true },
                    { "GB", "United Kingdom", true },
                    { "GD", "Grenada", true },
                    { "GE", "Georgia", true },
                    { "GF", "French Guiana", true },
                    { "GH", "Ghana", true },
                    { "GI", "Gibraltar", true },
                    { "GL", "Greenland", true },
                    { "GM", "Gambia", true },
                    { "GN", "Guinea", true },
                    { "GP", "Guadeloupe", true },
                    { "GQ", "Equatorial Guinea", true },
                    { "GR", "Greece", true },
                    { "GS", "South Georgia And The South Sandwich Islands", true },
                    { "GT", "Guatemala", true },
                    { "GU", "Guam", true },
                    { "KZ", "Kazakstan", true },
                    { "ZW", "Zimbabwe", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CountryId",
                table: "AspNetUsers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
