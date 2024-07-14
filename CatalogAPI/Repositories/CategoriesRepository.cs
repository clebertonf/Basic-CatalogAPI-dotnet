using CatalogAPI.Context;
using CatalogAPI.Models;
using CatalogAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoriesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Category> GetAllCategories(int size)
        {
            return _appDbContext.categories.Take(size).AsNoTracking().ToList();
        }

        public IEnumerable<Category> GeTAllCategoriesWithProducts()
        {
           return _appDbContext.categories.Include(c => c.Products).ToList();
        }

        public Category GetCategory(int id)
        {
            var categorie = _appDbContext.categories.FirstOrDefault(c => c.CategoryId.Equals(id));
            if (categorie is null) throw new ArgumentNullException(nameof(categorie));

            return categorie;
        }

        public Category CreateCategory(Category category)
        {
            if (category is null) throw new ArgumentNullException(nameof(category));

            _appDbContext.categories.Add(category);
            _appDbContext.SaveChanges();

            return category;
        }

        public Category UpdateCategory(Category category)
        {
            /*
            * Pode ser feito assim, com algumas desvantagens:
            * _appDbContext.Entry(category).State = EntityState.Modified;
            _appDbContext.SaveChanges();
           */

            var categorie = _appDbContext.categories.FirstOrDefault(c => c.CategoryId.Equals(category.CategoryId));
            if (categorie is null) throw new ArgumentNullException(nameof(categorie));

            categorie.Name = category.Name;
            categorie.UrlImage = category.UrlImage;

            _appDbContext.SaveChanges();

            return categorie;
        }

        public Category DeleteCategory(int id)
        {
            var categorie = _appDbContext.categories.Find(id);
            if (categorie is null) throw new ArgumentNullException(nameof(categorie));

            _appDbContext.categories.Remove(categorie);
            _appDbContext.SaveChanges();

            return categorie;
        }
    }
}
