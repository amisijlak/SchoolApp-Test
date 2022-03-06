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
    }
}
