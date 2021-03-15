using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppTesting.Web.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forecasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TemperatureC = table.Column<int>(type: "INTEGER", nullable: false),
                    Summary = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecasts", x => x.Id);
                    table.UniqueConstraint("AK_Forecasts_Date", x => x.Date);
                });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "Date", "Summary", "TemperatureC" },
                values: new object[] { 1, new DateTime(2021, 3, 13, 5, 0, 0, 0, DateTimeKind.Local), "Hot", 47 });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "Date", "Summary", "TemperatureC" },
                values: new object[] { 2, new DateTime(2021, 3, 14, 4, 0, 0, 0, DateTimeKind.Local), "Balmy", -15 });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "Date", "Summary", "TemperatureC" },
                values: new object[] { 3, new DateTime(2021, 3, 15, 4, 0, 0, 0, DateTimeKind.Local), "Cool", 39 });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "Date", "Summary", "TemperatureC" },
                values: new object[] { 4, new DateTime(2021, 3, 16, 5, 0, 0, 0, DateTimeKind.Local), "Hot", 18 });

            migrationBuilder.InsertData(
                table: "Forecasts",
                columns: new[] { "Id", "Date", "Summary", "TemperatureC" },
                values: new object[] { 5, new DateTime(2021, 3, 17, 5, 0, 0, 0, DateTimeKind.Local), "Mild", 50 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Forecasts");
        }
    }
}
