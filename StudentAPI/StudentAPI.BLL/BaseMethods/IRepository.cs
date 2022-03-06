using StudentAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPI.BLL.BaseMethods
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetQueryable();
        T Get(int id);
        IQueryable<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}