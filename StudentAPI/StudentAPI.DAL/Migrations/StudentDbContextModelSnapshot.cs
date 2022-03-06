﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentAPI.DAL;

namespace StudentAPI.DAL.Migrations
{
    [DbContext(typeof(StudentDbContext))]
    partial class StudentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("StudentAPI.DAL.Models.ApplicationDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ApplicationMasterId")
                        .HasColumnType("int");

                    b.Property<int>("CourseDetailsId")
                        .HasColumnType("int");

                    b.Property<decimal>("CoursePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationMasterId");

                    b.HasIndex("CourseDetailsId");

                    b.ToTable("ApplicationDetails");
                });

            modelBuilder.Entity("StudentAPI.DAL.Models.ApplicationMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ApplicationNumber")
                        .HasColumnType("nvarchar(75)");

                    b.Property<decimal>("GrandTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("TeachingMethod")
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("ApplicationMaster");
                });

            modelBuilder.Entity("StudentAPI.DAL.Models.CourseUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CourseCode")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("CourseUnits");
                });

            modelBuilder.Entity("StudentAPI.DAL.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("StudentName")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StudentAPI.DAL.Models.ApplicationDetail", b =>
                {
                    b.HasOne("StudentAPI.DAL.Models.ApplicationMaster", null)
                        .WithMany("ApplicationDetails")
                        .HasForeignKey("ApplicationMasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentAPI.DAL.Models.CourseUnit", "CourseDetails")
                        .WithMany()
                        .HasForeignKey("CourseDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseDetails");
                });

            modelBuilder.Entity("StudentAPI.DAL.Models.ApplicationMaster", b =>
                {
                    b.HasOne("StudentAPI.DAL.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentAPI.DAL.Models.ApplicationMaster", b =>
                {
                    b.Navigation("ApplicationDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
