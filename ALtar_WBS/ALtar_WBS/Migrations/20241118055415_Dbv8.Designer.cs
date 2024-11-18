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
    [Migration("20241118055415_Dbv8")]
    partial class Dbv8
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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

            modelBuilder.Entity("ALtar_WBS.Model.Student", b =>
                {
                    b.HasOne("ALtar_WBS.Model.User", "User")
                        .WithOne("Students")
                        .HasForeignKey("ALtar_WBS.Model.Student", "UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
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

            modelBuilder.Entity("ALtar_WBS.Model.Notifications", b =>
                {
                    b.Navigation("UserNotifications");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Role", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("ALtar_WBS.Model.Teacher", b =>
                {
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
