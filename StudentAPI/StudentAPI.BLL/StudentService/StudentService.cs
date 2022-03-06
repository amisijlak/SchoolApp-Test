using StudentAPI.BLL.BaseMethods;
using StudentAPI.DAL;
using StudentAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPI.BLL.StudentService
{
    public class StudentService : Repository<Student>, IStudentService
    {
        public StudentService(StudentDbContext dbContext) : base(dbContext) { }
    }
}
