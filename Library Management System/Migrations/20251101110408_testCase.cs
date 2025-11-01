using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class testCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Fines_FineId",
                table: "Incomes");

            migrationBuilder.AlterColumn<int>(
                name: "FineId",
                table: "Incomes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Fines_FineId",
                table: "Incomes",
                column: "FineId",
                principalTable: "Fines",
                principalColumn: "FineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Fines_FineId",
                table: "Incomes");

            migrationBuilder.AlterColumn<int>(
                name: "FineId",
                table: "Incomes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Fines_FineId",
                table: "Incomes",
                column: "FineId",
                principalTable: "Fines",
                principalColumn: "FineId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
