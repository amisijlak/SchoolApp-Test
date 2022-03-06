using StudentAPI.BLL.BaseMethods;
using StudentAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPI.BLL.Semester
{
    public interface IApplicationDetailsService : IRepository<ApplicationDetail>
    {
    }
}
