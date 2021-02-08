using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessLogicEntityFramework.Migrations
{
    public partial class isFriendshipAccepted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isAccepted",
                table: "Friendships",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAccepted",
                table: "Friendships");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Friendships",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID1",
                table: "Friendships",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_UserID",
                table: "Friendships",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_UserID1",
                table: "Friendships",
                column: "UserID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_UserID",
                table: "Friendships",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_UserID1",
                table: "Friendships",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
