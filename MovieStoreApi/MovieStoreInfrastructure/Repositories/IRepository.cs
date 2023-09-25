using System.Linq.Expressions;

namespace MovieStoreInfrastructure.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetByID(Guid id);
        void Insert(T entity);
        void Delete(T entity);
        T Find(Expression<Func<T, bool>> predicate);
        void Save();
    }
}
