using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using StudentAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPI.DAL
{
    public class StudentDbContext:DbContext
    {
        public string ConnectionString => _connectionString;
        private readonly string _connectionString;

        public StudentDbContext(DbContextOptions<StudentDbContext> options):base(options)
        {
            if (options != null)
            {
                //extract connnection string
                var extension = options.FindExtension<SqlServerOptionsExtension>();
                _connectionString = extension.ConnectionString;
            }
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<CourseUnit> CourseUnits { get; set; }
        public DbSet<ApplicationMaster> ApplicationMaster { get; set; }
        public DbSet<ApplicationDetail> ApplicationDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Call the base class first:
            base.OnModelCreating(builder);

            if (builder != null)
            {
                //settings
                builder.Entity<Student>().HasData(
                new Student
                {
                    Id = -1,
                    StudentName = "Jone Daniel",
                    Address = "Kampala",
                    DateOfBirth = new DateTime(1990, 12, 15),
                }, new Student
                {
                    Id = -2,
                    StudentName = "Jules Willis",
                    Address = "Kampala",
                    DateOfBirth = new DateTime(1998, 02, 15),
                }, new Student
                {
                    Id = -3,
                    StudentName = "Victoria Elisabeth",
                    Address = "Kampala",
                    DateOfBirth = new DateTime(2000, 11, 18),
                });


                builder.Entity<CourseUnit>().HasData(
                new CourseUnit
                {
                    Id =-1,
                    CourseCode = "ICT001",
                    CourseName = "Foundamental Of Computing",
                    Price = 30000,
                }, new CourseUnit
                {
                    Id = -2,
                    CourseCode = "ICT002",
                    CourseName = "Programming",
                    Price = 2000,
                }, new CourseUnit
                {
                    Id = -3,
                    CourseCode = "ENG223",
                    CourseName = "English Language",
                    Price = 1500,
                });
            }
        }
    }
}
