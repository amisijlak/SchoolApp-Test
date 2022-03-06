using StudentAPI.BLL.BaseMethods;
using StudentAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPI.BLL.Register
{
    public interface IApplicationService : IRepository<ApplicationMaster>
    {
        /// <summary>
        /// Deletes selected record from ApplicationDetail table based on Id
        /// </summary>
        /// <param name="Id"></param>
        (string, bool) DeleteRecord(int Id);
    }
}
