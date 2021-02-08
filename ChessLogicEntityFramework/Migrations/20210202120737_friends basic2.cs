using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessLogicEntityFramework.Migrations
{
    public partial class friendsbasic2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User2",
                table: "Friendships",
                newName: "User2ID");

            migrationBuilder.RenameColumn(
                name: "User1",
                table: "Friendships",
                newName: "User1ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User2ID",
                table: "Friendships",
                newName: "User2");

            migrationBuilder.RenameColumn(
                name: "User1ID",
                table: "Friendships",
                newName: "User1");
        }
    }
}
