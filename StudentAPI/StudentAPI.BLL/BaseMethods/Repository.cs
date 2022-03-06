using Microsoft.EntityFrameworkCore;
using StudentAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPI.BLL.BaseMethods
{
    public class Repository<T> : IDisposable, IRepository<T> where T : BaseEntity
    {
        protected readonly StudentDbContext context;
        protected readonly DbSet<T> entities;

        public Repository(StudentDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IQueryable<T> GetQueryable()
        {
            return entities.AsQueryable();
        }

        public T Get(int id)
        {
            return entities.Find(id);

        }

        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }

        public void Insert(T entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            SaveChange();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Entry(entity).State = EntityState.Modified;
            SaveChange();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            SaveChange();
        }

        private void SaveChange()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
    }
}


