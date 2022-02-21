using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GringottsBank.Migrations
{
    public partial class TransactionEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionTyp",
                table: "Transactions",
                newName: "TransactionType");

            migrationBuilder.RenameColumn(
                name: "TransactionStat",
                table: "Transactions",
                newName: "TransactionStatus");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionAmount",
                table: "Transactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDateTime",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionDateTime",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "TransactionType",
                table: "Transactions",
                newName: "TransactionTyp");

            migrationBuilder.RenameColumn(
                name: "TransactionStatus",
                table: "Transactions",
                newName: "TransactionStat");

            migrationBuilder.AlterColumn<float>(
                name: "TransactionAmount",
                table: "Transactions",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
