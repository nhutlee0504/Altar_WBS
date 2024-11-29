﻿// <auto-generated />
using System;
using ALtar_WBS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ALtar_WBS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241124125632_Dbv20")]
    partial class Dbv20
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ALtar_WBS.Model.Classes", b =>
                {
                    b.Property<int>("ClassID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClassID"), 1L, 1);

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.HasKey("ClassID");

                    b.HasIndex("CourseID");

                    b.ToTable("classes");
                });

            modelBuilder.Entity("ALtar_WBS.Model.ClassTeachers", b =>
                {
                    b.Property<int>("ctID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ctID"), 1L, 1);

                    b.Property<int>("ClassID")
                        .HasColumnType("int");

                    b.Property<int>("TeacherID")
                        .HasColumnType("int");

                    b.HasKey("ctID");

                    b.HasIndex("ClassID");

                    b.HasIndex("TeacherID");

                    b.ToTable("classTeachers");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseID"), 1L, 1);

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Fee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CourseID");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("ALtar_WBS.Model.CourseSubject", b =>
                {
                    b.Property<int>("csID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("csID"), 1L, 1);

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<int>("SubjectID")
                        .HasColumnType("int");

                    b.HasKey("csID");

                    b.HasIndex("CourseID");

                    b.HasIndex("SubjectID");

                    b.ToTable("courseSubjects");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EnrollmentID"), 1L, 1);

                    b.Property<int>("ClassID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ErollmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("ClassID");

                    b.HasIndex("StudentID");

                    b.ToTable("enrollments");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Notifications", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificationId"), 1L, 1);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Senđate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NotificationId");

                    b.ToTable("notifications");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Payment", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentID"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.HasKey("PaymentID");

                    b.HasIndex("StudentID");

                    b.ToTable("payments");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Report", b =>
                {
                    b.Property<int>("ReportID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportID"), 1L, 1);

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReportID");

                    b.ToTable("reports");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"), 1L, 1);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Schedule", b =>
                {
                    b.Property<int>("ScheduleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScheduleID"), 1L, 1);

                    b.Property<int>("ClassID")
                        .HasColumnType("int");

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<string>("Room")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("ScheduleID");

                    b.HasIndex("ClassID");

                    b.ToTable("schedules");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Student", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("ParentPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("students");
                });

            modelBuilder.Entity("ALtar_WBS.Model.SubjectCategories", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("subjectCategories");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Subjects", b =>
                {
                    b.Property<int>("SubjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectID"), 1L, 1);

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubjectID");

                    b.HasIndex("CategoryID");

                    b.ToTable("subjects");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Teacher", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdCard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserID");

                    b.ToTable("teachers");
                });

            modelBuilder.Entity("ALtar_WBS.Model.TeacherSalary", b =>
                {
                    b.Property<int>("SalaryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalaryID"), 1L, 1);

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeacherID")
                        .HasColumnType("int");

                    b.Property<decimal>("amount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("SalaryID");

                    b.HasIndex("TeacherID");

                    b.ToTable("teacherSalaries");
                });

            modelBuilder.Entity("ALtar_WBS.Model.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.ToTable("users");
                });

            modelBuilder.Entity("ALtar_WBS.Model.UserNotifications", b =>
                {
                    b.Property<int>("upID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("upID"), 1L, 1);

                    b.Property<int>("NotificationID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("upID");

                    b.HasIndex("NotificationID");

                    b.HasIndex("UserID");

                    b.ToTable("userNotifications");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Classes", b =>
                {
                    b.HasOne("ALtar_WBS.Model.Course", "Course")
                        .WithMany("Classes")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("ALtar_WBS.Model.ClassTeachers", b =>
                {
                    b.HasOne("ALtar_WBS.Model.Classes", "Classes")
                        .WithMany("ClassTeachers")
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ALtar_WBS.Model.Teacher", "Teacher")
                        .WithMany("ClassTeachers")
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Classes");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ALtar_WBS.Model.CourseSubject", b =>
                {
                    b.HasOne("ALtar_WBS.Model.Course", "Course")
                        .WithMany("CourseSubjects")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ALtar_WBS.Model.Subjects", "Subjects")
                        .WithMany("CourseSubjects")
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Enrollment", b =>
                {
                    b.HasOne("ALtar_WBS.Model.Classes", "Classes")
                        .WithMany("Enrollments")
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ALtar_WBS.Model.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Classes");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Payment", b =>
                {
                    b.HasOne("ALtar_WBS.Model.Student", "Student")
                        .WithMany("payments")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Schedule", b =>
                {
                    b.HasOne("ALtar_WBS.Model.Classes", "Classes")
                        .WithMany("schedules")
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Classes");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Student", b =>
                {
                    b.HasOne("ALtar_WBS.Model.User", "User")
                        .WithOne("Students")
                        .HasForeignKey("ALtar_WBS.Model.Student", "UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Subjects", b =>
                {
                    b.HasOne("ALtar_WBS.Model.SubjectCategories", "SubjectCategories")
                        .WithMany("Subjects")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SubjectCategories");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Teacher", b =>
                {
                    b.HasOne("ALtar_WBS.Model.User", "User")
                        .WithOne("Teachers")
                        .HasForeignKey("ALtar_WBS.Model.Teacher", "UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ALtar_WBS.Model.TeacherSalary", b =>
                {
                    b.HasOne("ALtar_WBS.Model.Teacher", "Teacher")
                        .WithMany("TeacherSalaries")
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ALtar_WBS.Model.User", b =>
                {
                    b.HasOne("ALtar_WBS.Model.Role", "Role")
                        .WithMany("users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ALtar_WBS.Model.UserNotifications", b =>
                {
                    b.HasOne("ALtar_WBS.Model.Notifications", "Notifications")
                        .WithMany("UserNotifications")
                        .HasForeignKey("NotificationID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ALtar_WBS.Model.User", "User")
                        .WithMany("UserNotifications")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Notifications");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Classes", b =>
                {
                    b.Navigation("ClassTeachers");

                    b.Navigation("Enrollments");

                    b.Navigation("schedules");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Course", b =>
                {
                    b.Navigation("Classes");

                    b.Navigation("CourseSubjects");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Notifications", b =>
                {
                    b.Navigation("UserNotifications");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Role", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Student", b =>
                {
                    b.Navigation("Enrollments");

                    b.Navigation("payments");
                });

            modelBuilder.Entity("ALtar_WBS.Model.SubjectCategories", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Subjects", b =>
                {
                    b.Navigation("CourseSubjects");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Teacher", b =>
                {
                    b.Navigation("ClassTeachers");

                    b.Navigation("TeacherSalaries");
                });

            modelBuilder.Entity("ALtar_WBS.Model.User", b =>
                {
                    b.Navigation("Students")
                        .IsRequired();

                    b.Navigation("Teachers")
                        .IsRequired();

                    b.Navigation("UserNotifications");
                });
#pragma warning restore 612, 618
        }
    }
}
