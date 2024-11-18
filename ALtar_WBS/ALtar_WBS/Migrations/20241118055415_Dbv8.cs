using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALtar_WBS.Migrations
{
    public partial class Dbv8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_teacherSalaries_teachers_TeacherID",
                table: "teacherSalaries");

            migrationBuilder.AddForeignKey(
                name: "FK_teacherSalaries_teachers_TeacherID",
                table: "teacherSalaries",
                column: "TeacherID",
                principalTable: "teachers",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_teacherSalaries_teachers_TeacherID",
                table: "teacherSalaries");

            migrationBuilder.AddForeignKey(
                name: "FK_teacherSalaries_teachers_TeacherID",
                table: "teacherSalaries",
                column: "TeacherID",
                principalTable: "teachers",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
