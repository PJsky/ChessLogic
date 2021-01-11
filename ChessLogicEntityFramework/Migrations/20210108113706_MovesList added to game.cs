using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessLogicEntityFramework.Migrations
{
    public partial class MovesListaddedtogame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovesList",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovesList",
                table: "Games");
        }
    }
}
