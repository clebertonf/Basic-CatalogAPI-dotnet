using CatalogAPI.Models;

namespace CatalogAPI.Repositories
{
    public interface ICategoriesRepository
    {
        public IEnumerable<Category> GetAllCategories(int size);
        public IEnumerable<Category> GeTAllCategoriesWithProducts();
        public Category GetCategory(int id);
        public Category CreateCategory(Category category); 
        public Category UpdateCategory(Category category);
        public Category DeleteCategory(int id);
    }
}
