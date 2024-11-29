using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALtar_WBS.Migrations
{
    public partial class Dbv20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Room",
                table: "classes");

            migrationBuilder.CreateTable(
                name: "schedules",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedules", x => x.ScheduleID);
                    table.ForeignKey(
                        name: "FK_schedules_classes_ClassID",
                        column: x => x.ClassID,
                        principalTable: "classes",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_schedules_ClassID",
                table: "schedules",
                column: "ClassID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "schedules");

            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "classes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
