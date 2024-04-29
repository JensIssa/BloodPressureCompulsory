using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MeasurementInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Systolic = table.Column<int>(type: "int", nullable: false),
                    Diastolic = table.Column<int>(type: "int", nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PatientSSN = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Measurements",
                columns: new[] { "Id", "Date", "Diastolic", "PatientSSN", "Systolic" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 29, 17, 19, 55, 178, DateTimeKind.Local).AddTicks(7049), 60, "123", 60 },
                    { 2, new DateTime(2024, 4, 29, 17, 19, 55, 178, DateTimeKind.Local).AddTicks(7104), 30, "1234", 20 },
                    { 3, new DateTime(2024, 4, 29, 17, 19, 55, 178, DateTimeKind.Local).AddTicks(7107), 40, "12345", 30 },
                    { 4, new DateTime(2024, 4, 29, 17, 19, 55, 178, DateTimeKind.Local).AddTicks(7110), 55, "123456", 70 },
                    { 5, new DateTime(2024, 4, 29, 17, 19, 55, 178, DateTimeKind.Local).AddTicks(7112), 60, "1234567", 80 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");
        }
    }
}
