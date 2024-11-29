using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALtar_WBS.Migrations
{
    public partial class Dbv17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "enrollments",
                columns: table => new
                {
                    EnrollmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    ClassID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enrollments", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK_enrollments_classes_ClassID",
                        column: x => x.ClassID,
                        principalTable: "classes",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_enrollments_students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "students",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_ClassID",
                table: "enrollments",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_StudentID",
                table: "enrollments",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "enrollments");
        }
    }
}
