using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookRequests_Books_Book_id",
                table: "BookRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_BookRequests_Users_User_id",
                table: "BookRequests");

            migrationBuilder.DropIndex(
                name: "IX_BookRequests_Book_id",
                table: "BookRequests");

            migrationBuilder.DropIndex(
                name: "IX_BookRequests_User_id",
                table: "BookRequests");

            migrationBuilder.DropColumn(
                name: "Book_id",
                table: "BookRequests");

            migrationBuilder.DropColumn(
                name: "User_id",
                table: "BookRequests");

            migrationBuilder.CreateIndex(
                name: "IX_BookRequests_BookId",
                table: "BookRequests",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookRequests_UserId",
                table: "BookRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookRequests_Books_BookId",
                table: "BookRequests",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRequests_Users_UserId",
                table: "BookRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookRequests_Books_BookId",
                table: "BookRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_BookRequests_Users_UserId",
                table: "BookRequests");

            migrationBuilder.DropIndex(
                name: "IX_BookRequests_BookId",
                table: "BookRequests");

            migrationBuilder.DropIndex(
                name: "IX_BookRequests_UserId",
                table: "BookRequests");

            migrationBuilder.AddColumn<int>(
                name: "Book_id",
                table: "BookRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User_id",
                table: "BookRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookRequests_Book_id",
                table: "BookRequests",
                column: "Book_id");

            migrationBuilder.CreateIndex(
                name: "IX_BookRequests_User_id",
                table: "BookRequests",
                column: "User_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookRequests_Books_Book_id",
                table: "BookRequests",
                column: "Book_id",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRequests_Users_User_id",
                table: "BookRequests",
                column: "User_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
