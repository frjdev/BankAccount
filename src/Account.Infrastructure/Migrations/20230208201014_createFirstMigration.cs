using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Account.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationSet_AccountSet_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AccountSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountSet",
                columns: new[] { "Id", "Amount", "Balance", "Date" },
                values: new object[,]
                {
                    { 1, 10m, 1000m, new DateTime(2023, 2, 8, 21, 10, 14, 376, DateTimeKind.Local).AddTicks(2619) },
                    { 2, 10m, 0m, new DateTime(2023, 2, 8, 21, 10, 14, 376, DateTimeKind.Local).AddTicks(2674) },
                    { 3, 10m, 1000m, new DateTime(2023, 2, 8, 21, 10, 14, 376, DateTimeKind.Local).AddTicks(2677) }
                });

            migrationBuilder.InsertData(
                table: "OperationSet",
                columns: new[] { "Id", "AccountId", "Type" },
                values: new object[,]
                {
                    { 1, 1, "Deposit" },
                    { 2, 1, "Deposit" },
                    { 3, 1, "Withdrawal" },
                    { 4, 1, "Withdrawal" },
                    { 5, 1, "Deposit" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationSet_AccountId",
                table: "OperationSet",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationSet");

            migrationBuilder.DropTable(
                name: "AccountSet");
        }
    }
}
