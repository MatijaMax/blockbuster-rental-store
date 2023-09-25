using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MovieStoreInfrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly MovieStoreContext context;

        public GenericRepository(MovieStoreContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetByID(Guid id)
        {
            return context.Set<T>().Find(id);
        }

        public void Insert(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            var entry = context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                context.Set<T>().Attach(entity);
            }
            context.Set<T>().Remove(entity);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate).FirstOrDefault();
        }
        public void Save()
        {
            context.SaveChanges();
        }

    }
}
