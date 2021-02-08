using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessLogicEntityFramework.Migrations
{
    public partial class gameTimers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameTime",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeGain",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameTime",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TimeGain",
                table: "Games");
        }
    }
}
