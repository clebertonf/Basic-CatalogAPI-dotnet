using CatalogAPI.DTOs;
using CatalogAPI.Models;
using CatalogAPI.Pagination;
using CatalogAPI.Pagination.Filter;

namespace CatalogAPI.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        public Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync(int size);
        public Task<IEnumerable<Category>> GeTAllCategoriesWithProductsAsync();
        public PagedList<Category> GetCategoryPagination(CateroryParameters categoryParameter);
        public PagedList<Category> GetCategoryByName(CategoryFilterName categoryFilterName);
        public CategoryDTO GetCategory(int id);
        public CategoryDTO CreateCategory(CategoryDTO category);
        public CategoryDTO UpdateCategory(CategoryDTO category);
        public CategoryDTO DeleteCategory(int id);
    }
}
