using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALtar_WBS.Migrations
{
    public partial class Dbv16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "classTeachers",
                columns: table => new
                {
                    ctID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassID = table.Column<int>(type: "int", nullable: false),
                    TeacherID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classTeachers", x => x.ctID);
                    table.ForeignKey(
                        name: "FK_classTeachers_classes_ClassID",
                        column: x => x.ClassID,
                        principalTable: "classes",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_classTeachers_teachers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "teachers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_classTeachers_ClassID",
                table: "classTeachers",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_classTeachers_TeacherID",
                table: "classTeachers",
                column: "TeacherID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "classTeachers");
        }
    }
}
