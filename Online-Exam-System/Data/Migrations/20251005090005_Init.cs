using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Exam_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Doplomas_DiplomaId",
                table: "Exams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doplomas",
                table: "Doplomas");

            migrationBuilder.RenameTable(
                name: "Doplomas",
                newName: "Diplomas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diplomas",
                table: "Diplomas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Diplomas_DiplomaId",
                table: "Exams",
                column: "DiplomaId",
                principalTable: "Diplomas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Diplomas_DiplomaId",
                table: "Exams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diplomas",
                table: "Diplomas");

            migrationBuilder.RenameTable(
                name: "Diplomas",
                newName: "Doplomas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doplomas",
                table: "Doplomas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Doplomas_DiplomaId",
                table: "Exams",
                column: "DiplomaId",
                principalTable: "Doplomas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
