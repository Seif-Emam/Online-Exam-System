using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Exam_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelationBetweenDiplomaandExam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "duration",
                table: "Exams",
                newName: "Duration");

            migrationBuilder.AddColumn<Guid>(
                name: "DiplomaId",
                table: "Exams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Exams_DiplomaId",
                table: "Exams",
                column: "DiplomaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Doplomas_DiplomaId",
                table: "Exams",
                column: "DiplomaId",
                principalTable: "Doplomas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Doplomas_DiplomaId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_DiplomaId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "DiplomaId",
                table: "Exams");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Exams",
                newName: "duration");
        }
    }
}
