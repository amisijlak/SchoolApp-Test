using Microsoft.EntityFrameworkCore;
using StudentAPI.BLL.BaseMethods;
using StudentAPI.DAL;
using StudentAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPI.BLL.Register
{
    public class ApplicationService : Repository<ApplicationMaster>, IApplicationService
    {
        public ApplicationService(StudentDbContext dbContext) : base(dbContext) { }

        public (string, bool) DeleteRecord(int Id)
        {
            try
            {
                var record = context.Set<ApplicationMaster>().Find(Id);
                foreach(var item in context.Set<ApplicationDetail>().Where(r => r.ApplicationMasterId == Id))
                {
                    context.Set<ApplicationDetail>().Remove(item);
                }
                context.Set<ApplicationMaster>().Remove(record);
                context.SaveChanges();
                return (string.Empty, true);
            }
            catch (Exception e)
            {
                return (e.InnerException.Message, false);
            }
        }
    }
}
