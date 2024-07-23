using CatalogAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CatalogAPI.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _appDbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public T Create(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
            // _appDbContext.SaveChanges(); It is now the responsibility of the unit of work standard
            return entity;
        }

        public T Update(T entity)
        {
            // _appDbContext.Entry(entity).State = EntityState.Modified;
            _appDbContext.Set<T>().Update(entity);
            // _appDbContext.SaveChanges(); It is now the responsibility of the unit of work standard
            return entity;
        }

        public T Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
            // _appDbContext.SaveChanges(); It is now the responsibility of the unit of work standard
            return entity;
        }
    }
}
