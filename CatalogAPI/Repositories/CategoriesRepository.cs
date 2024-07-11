using CatalogAPI.Context;
using CatalogAPI.Models;

namespace CatalogAPI.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoriesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _appDbContext.categories.ToList();
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
            var categorie = _appDbContext.categories.FirstOrDefault(c => c.CategoryId.Equals(category));
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
