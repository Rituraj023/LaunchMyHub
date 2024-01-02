using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaunchMyHub.Migrations
{
    /// <inheritdoc />
    public partial class InislizeLaunchMyhub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HubTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferralSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HubMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrganizationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FeeType = table.Column<byte>(type: "tinyint", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NonProfit = table.Column<bool>(type: "bit", nullable: false),
                    PreferredSubdomain = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BriefDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    HubTypeId = table.Column<int>(type: "int", nullable: true),
                    HubTypeOther = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReferralSourceId = table.Column<int>(type: "int", nullable: true),
                    ReferralSourceOther = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HubMasters_HubTypes_HubTypeId",
                        column: x => x.HubTypeId,
                        principalTable: "HubTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HubMasters_ReferralSources_ReferralSourceId",
                        column: x => x.ReferralSourceId,
                        principalTable: "ReferralSources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HubMasters_HubTypeId",
                table: "HubMasters",
                column: "HubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HubMasters_ReferralSourceId",
                table: "HubMasters",
                column: "ReferralSourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HubMasters");

            migrationBuilder.DropTable(
                name: "HubTypes");

            migrationBuilder.DropTable(
                name: "ReferralSources");
        }
    }
}
