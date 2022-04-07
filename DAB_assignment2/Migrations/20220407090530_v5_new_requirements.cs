using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAB_assignment2.Migrations
{
    public partial class v5_new_requirements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Chairmen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Accesses",
                columns: table => new
                {
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Codes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accesses", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Accesses_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Address",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyResponsibles",
                columns: table => new
                {
                    SocietyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyResponsibles", x => x.SocietyId);
                    table.ForeignKey(
                        name: "FK_KeyResponsibles_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeyResponsibles_Societies_SocietyId",
                        column: x => x.SocietyId,
                        principalTable: "Societies",
                        principalColumn: "CVR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chairmen_MemberId",
                table: "Chairmen",
                column: "MemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KeyResponsibles_MemberId",
                table: "KeyResponsibles",
                column: "MemberId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Chairmen_Members_MemberId",
                table: "Chairmen",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chairmen_Members_MemberId",
                table: "Chairmen");

            migrationBuilder.DropTable(
                name: "Accesses");

            migrationBuilder.DropTable(
                name: "KeyResponsibles");

            migrationBuilder.DropIndex(
                name: "IX_Chairmen_MemberId",
                table: "Chairmen");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Chairmen");
        }
    }
}
