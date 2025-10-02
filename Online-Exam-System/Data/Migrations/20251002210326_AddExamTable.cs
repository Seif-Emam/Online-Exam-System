using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Exam_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddExamTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: new Guid("156d577a-dba5-46d6-b5c5-1b893b1c1cbe"));

            migrationBuilder.DeleteData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: new Guid("f4b02ec3-b504-490c-8064-60b42b359b47"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "Id", "CreatedAt", "EndDate", "IsDeleted", "PictureUrl", "StartDate", "Title", "UpdatedAt", "duration" },
                values: new object[,]
                {
                    { new Guid("156d577a-dba5-46d6-b5c5-1b893b1c1cbe"), new DateTime(2025, 9, 30, 0, 7, 58, 627, DateTimeKind.Local).AddTicks(4556), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://example.com/exams/csharp.png", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# Basics Exam", new DateTime(2025, 9, 30, 0, 7, 58, 627, DateTimeKind.Local).AddTicks(4558), new TimeSpan(0, 1, 30, 0, 0) },
                    { new Guid("f4b02ec3-b504-490c-8064-60b42b359b47"), new DateTime(2025, 9, 30, 0, 7, 58, 627, DateTimeKind.Local).AddTicks(4569), new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "https://example.com/exams/aspnet.png", new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASP.NET Core Web API Exam", new DateTime(2025, 9, 30, 0, 7, 58, 627, DateTimeKind.Local).AddTicks(4571), new TimeSpan(0, 2, 0, 0, 0) }
                });
        }
    }
}
