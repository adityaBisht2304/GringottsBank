using Microsoft.EntityFrameworkCore.Migrations;

namespace GringottsBank.Migrations
{
    public partial class AccountEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Customers_CustomerID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CustomerID",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "EmailID",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AccountNumber",
                table: "Accounts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AccountType",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "EmailID",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AccountNumber",
                table: "Accounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountID",
                table: "Transactions",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerID",
                table: "Accounts",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Customers_CustomerID",
                table: "Accounts",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountID",
                table: "Transactions",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
