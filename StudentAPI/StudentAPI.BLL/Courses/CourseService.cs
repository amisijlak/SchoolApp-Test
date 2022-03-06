using StudentAPI.BLL.BaseMethods;
using StudentAPI.DAL;
using StudentAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPI.BLL.Courses
{
    public class CourseService : Repository<CourseUnit>, ICourseService
    {
        public CourseService(StudentDbContext dbContext) : base(dbContext) { }
    }
}
