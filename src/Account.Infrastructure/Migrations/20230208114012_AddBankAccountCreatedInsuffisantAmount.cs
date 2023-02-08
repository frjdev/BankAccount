using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBankAccountCreatedInsuffisantAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccountSet",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 2, 8, 12, 40, 11, 997, DateTimeKind.Local).AddTicks(6037));

            migrationBuilder.UpdateData(
                table: "AccountSet",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Balance", "Date" },
                values: new object[] { 0m, new DateTime(2023, 2, 8, 12, 40, 11, 997, DateTimeKind.Local).AddTicks(6089) });

            migrationBuilder.UpdateData(
                table: "AccountSet",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2023, 2, 8, 12, 40, 11, 997, DateTimeKind.Local).AddTicks(6091));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccountSet",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 2, 8, 11, 54, 46, 630, DateTimeKind.Local).AddTicks(543));

            migrationBuilder.UpdateData(
                table: "AccountSet",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Balance", "Date" },
                values: new object[] { 1000m, new DateTime(2023, 2, 8, 11, 54, 46, 630, DateTimeKind.Local).AddTicks(602) });

            migrationBuilder.UpdateData(
                table: "AccountSet",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2023, 2, 8, 11, 54, 46, 630, DateTimeKind.Local).AddTicks(604));
        }
    }
}
