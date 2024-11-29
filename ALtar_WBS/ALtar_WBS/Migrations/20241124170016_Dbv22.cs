using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALtar_WBS.Migrations
{
    public partial class Dbv22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "users");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "users",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "grades",
                columns: table => new
                {
                    GradeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    GradeValue = table.Column<float>(type: "real", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grades", x => x.GradeID);
                    table.ForeignKey(
                        name: "FK_grades_courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_grades_students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "students",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_grades_CourseID",
                table: "grades",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_grades_StudentID",
                table: "grades",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grades");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
