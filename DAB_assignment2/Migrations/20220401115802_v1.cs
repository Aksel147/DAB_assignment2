using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAB_assignment2.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chairmen",
                columns: table => new
                {
                    CPR = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chairmen", x => x.CPR);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Address = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeopleLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Address);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Timespans",
                columns: table => new
                {
                    Span = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timespans", x => x.Span);
                });

            migrationBuilder.CreateTable(
                name: "Societies",
                columns: table => new
                {
                    CVR = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChairmanId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Societies", x => x.CVR);
                    table.ForeignKey(
                        name: "FK_Societies_Chairmen_ChairmanId",
                        column: x => x.ChairmanId,
                        principalTable: "Chairmen",
                        principalColumn: "CPR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeopleLimit = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Address");
                });

            migrationBuilder.CreateTable(
                name: "LocationTimespan",
                columns: table => new
                {
                    AvailabilitySpan = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationsAddress = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationTimespan", x => new { x.AvailabilitySpan, x.LocationsAddress });
                    table.ForeignKey(
                        name: "FK_LocationTimespan_Locations_LocationsAddress",
                        column: x => x.LocationsAddress,
                        principalTable: "Locations",
                        principalColumn: "Address",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationTimespan_Timespans_AvailabilitySpan",
                        column: x => x.AvailabilitySpan,
                        principalTable: "Timespans",
                        principalColumn: "Span",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberSociety",
                columns: table => new
                {
                    MembersId = table.Column<int>(type: "int", nullable: false),
                    SocietiesCVR = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberSociety", x => new { x.MembersId, x.SocietiesCVR });
                    table.ForeignKey(
                        name: "FK_MemberSociety_Members_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberSociety_Societies_SocietiesCVR",
                        column: x => x.SocietiesCVR,
                        principalTable: "Societies",
                        principalColumn: "CVR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocietyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    TimespanId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Address",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_Societies_SocietyId",
                        column: x => x.SocietyId,
                        principalTable: "Societies",
                        principalColumn: "CVR",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Timespans_TimespanId",
                        column: x => x.TimespanId,
                        principalTable: "Timespans",
                        principalColumn: "Span");
                });

            migrationBuilder.CreateTable(
                name: "RoomTimespan",
                columns: table => new
                {
                    AvailabilitySpan = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTimespan", x => new { x.AvailabilitySpan, x.RoomsId });
                    table.ForeignKey(
                        name: "FK_RoomTimespan_Rooms_RoomsId",
                        column: x => x.RoomsId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomTimespan_Timespans_AvailabilitySpan",
                        column: x => x.AvailabilitySpan,
                        principalTable: "Timespans",
                        principalColumn: "Span",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_LocationId",
                table: "Bookings",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SocietyId",
                table: "Bookings",
                column: "SocietyId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TimespanId",
                table: "Bookings",
                column: "TimespanId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationTimespan_LocationsAddress",
                table: "LocationTimespan",
                column: "LocationsAddress");

            migrationBuilder.CreateIndex(
                name: "IX_MemberSociety_SocietiesCVR",
                table: "MemberSociety",
                column: "SocietiesCVR");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_LocationId",
                table: "Rooms",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTimespan_RoomsId",
                table: "RoomTimespan",
                column: "RoomsId");

            migrationBuilder.CreateIndex(
                name: "IX_Societies_ChairmanId",
                table: "Societies",
                column: "ChairmanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "LocationTimespan");

            migrationBuilder.DropTable(
                name: "MemberSociety");

            migrationBuilder.DropTable(
                name: "RoomTimespan");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Societies");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Timespans");

            migrationBuilder.DropTable(
                name: "Chairmen");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
