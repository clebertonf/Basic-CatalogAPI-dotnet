using System.Linq.Expressions;

namespace CatalogAPI.Repositories.Generic
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? Get(Expression<Func<T, bool>> predicate);
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
