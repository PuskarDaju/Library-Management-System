using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class categoryUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rent_logs");

            migrationBuilder.RenameColumn(
                name: "Password_Hash",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "Full_Name",
                table: "Users",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Category_Name",
                table: "Category",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "Category_Id",
                table: "Category",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "ISBN",
                table: "Books",
                newName: "Isbn");

            migrationBuilder.RenameColumn(
                name: "publication_Date",
                table: "Books",
                newName: "PublicationDate");

            migrationBuilder.RenameColumn(
                name: "Image_Url",
                table: "Books",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Book_Name",
                table: "Books",
                newName: "BookName");

            migrationBuilder.RenameColumn(
                name: "Book_id",
                table: "Books",
                newName: "BookId");

            migrationBuilder.RenameColumn(
                name: "Request_type",
                table: "BookRequests",
                newName: "RequestType");

            migrationBuilder.RenameColumn(
                name: "Book_request_id",
                table: "BookRequests",
                newName: "BookRequestId");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "BookRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BookRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RentLogs",
                columns: table => new
                {
                    RentLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IncomeId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentLogs", x => x.RentLogId);
                    table.ForeignKey(
                        name: "FK_RentLogs_Incomes_IncomeId",
                        column: x => x.IncomeId,
                        principalTable: "Incomes",
                        principalColumn: "IncomeId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RentLogs_IncomeId",
                table: "RentLogs",
                column: "IncomeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentLogs");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "BookRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BookRequests");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Password_Hash");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Users",
                newName: "Full_Name");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Category",
                newName: "Category_Name");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Category",
                newName: "Category_Id");

            migrationBuilder.RenameColumn(
                name: "Isbn",
                table: "Books",
                newName: "ISBN");

            migrationBuilder.RenameColumn(
                name: "PublicationDate",
                table: "Books",
                newName: "publication_Date");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Books",
                newName: "Image_Url");

            migrationBuilder.RenameColumn(
                name: "BookName",
                table: "Books",
                newName: "Book_Name");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Books",
                newName: "Book_id");

            migrationBuilder.RenameColumn(
                name: "RequestType",
                table: "BookRequests",
                newName: "Request_type");

            migrationBuilder.RenameColumn(
                name: "BookRequestId",
                table: "BookRequests",
                newName: "Book_request_id");

            migrationBuilder.CreateTable(
                name: "Rent_logs",
                columns: table => new
                {
                    RentLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IncomeId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rent_logs", x => x.RentLogId);
                    table.ForeignKey(
                        name: "FK_Rent_logs_Incomes_IncomeId",
                        column: x => x.IncomeId,
                        principalTable: "Incomes",
                        principalColumn: "IncomeId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_logs_IncomeId",
                table: "Rent_logs",
                column: "IncomeId");
        }
    }
}
