using CatalogAPI.DTOs;
using CatalogAPI.Models;

namespace CatalogAPI.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        public IEnumerable<CategoryDTO> GetAllCategories(int size);
        public IEnumerable<Category> GeTAllCategoriesWithProducts();
        public CategoryDTO GetCategory(int id);
        public CategoryDTO CreateCategory(CategoryDTO category);
        public CategoryDTO UpdateCategory(CategoryDTO category);
        public CategoryDTO DeleteCategory(int id);
    }
}
