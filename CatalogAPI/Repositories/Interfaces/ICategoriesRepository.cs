using CatalogAPI.DTOs;
using CatalogAPI.Models;
using CatalogAPI.Pagination;
using CatalogAPI.Pagination.Filter;

namespace CatalogAPI.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        public IEnumerable<CategoryDTO> GetAllCategories(int size);
        public IEnumerable<Category> GeTAllCategoriesWithProducts();
        public PagedList<Category> GetCategoryPagination(CateroryParameters categoryParameter);
        public PagedList<Category> GetCategoryByName(CategoryFilterName categoryFilterName);
        public CategoryDTO GetCategory(int id);
        public CategoryDTO CreateCategory(CategoryDTO category);
        public CategoryDTO UpdateCategory(CategoryDTO category);
        public CategoryDTO DeleteCategory(int id);
    }
}
