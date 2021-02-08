using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessLogicEntityFramework.Migrations
{
    public partial class friendsbasic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    User1 = table.Column<int>(type: "int", nullable: false),
                    User2 = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    UserID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => new { x.User1, x.User2 });
                    table.ForeignKey(
                        name: "FK_Friendships_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendships_Users_UserID1",
                        column: x => x.UserID1,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_UserID",
                table: "Friendships",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_UserID1",
                table: "Friendships",
                column: "UserID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    FriendOneID = table.Column<int>(type: "int", nullable: false),
                    FriendTwoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => new { x.FriendOneID, x.FriendTwoID })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Friends_Users_FriendOneID",
                        column: x => x.FriendOneID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Friends_Users_FriendTwoID",
                        column: x => x.FriendTwoID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_FriendTwoID",
                table: "Friends",
                column: "FriendTwoID");
        }
    }
}
