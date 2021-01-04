using Microsoft.EntityFrameworkCore.Migrations;

namespace XUnitDbAccessEFTest.Migrations
{
    public partial class initalTestingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerWhiteID = table.Column<int>(type: "int", nullable: true),
                    PlayerBlackID = table.Column<int>(type: "int", nullable: true),
                    WinnerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Games_Users_PlayerBlackID",
                        column: x => x.PlayerBlackID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Users_PlayerWhiteID",
                        column: x => x.PlayerWhiteID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Users_WinnerID",
                        column: x => x.WinnerID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerBlackID",
                table: "Games",
                column: "PlayerBlackID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerWhiteID",
                table: "Games",
                column: "PlayerWhiteID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_WinnerID",
                table: "Games",
                column: "WinnerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
