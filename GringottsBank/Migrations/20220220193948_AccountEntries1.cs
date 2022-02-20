using Microsoft.EntityFrameworkCore.Migrations;

namespace GringottsBank.Migrations
{
    public partial class AccountEntries1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Accounts",
                newName: "CreationDateTime");

            migrationBuilder.AlterColumn<int>(
                name: "AccountBalance",
                table: "Accounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationDateTime",
                table: "Accounts",
                newName: "CreationDate");

            migrationBuilder.AlterColumn<float>(
                name: "AccountBalance",
                table: "Accounts",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
