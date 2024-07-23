using CatalogAPI.Context;
using CatalogAPI.DTOs;
using CatalogAPI.DTOs.Extensions;
using CatalogAPI.Models;
using CatalogAPI.Pagination;
using CatalogAPI.Pagination.Filter;
using CatalogAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace CatalogAPI.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoriesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync(int size)
        {
            var categories = await _appDbContext.categories.Take(size).AsNoTracking().ToListAsync();
            var response = CategoriaDTOMappingExtensions.TocategoryDTOList(categories);

            return response;
        }

        public async Task<IEnumerable<Category>> GeTAllCategoriesWithProductsAsync()
        {
           return await _appDbContext.categories.Include(c => c.Products).ToListAsync();
        }

        public CategoryDTO GetCategory(int id)
        {
            var categorie = _appDbContext.categories.FirstOrDefault(c => c.CategoryId.Equals(id));
            if (categorie is null) throw new ArgumentNullException(nameof(categorie));

            return CategoriaDTOMappingExtensions.ToCategoryDTO(categorie);
        }

        public CategoryDTO CreateCategory(CategoryDTO categorieDTO)
        {
            if (categorieDTO is null) throw new ArgumentNullException(nameof(categorieDTO));

            var categorie = CategoriaDTOMappingExtensions.ToCategory(categorieDTO);

            _appDbContext.categories.Add(categorie);
            _appDbContext.SaveChanges();

            return categorieDTO;
        }

        public CategoryDTO UpdateCategory(CategoryDTO categorieDTO)
        {
            /*
            * Pode ser feito assim, com algumas desvantagens:
            * _appDbContext.Entry(category).State = EntityState.Modified;
            _appDbContext.SaveChanges();
           */

            var categorie = CategoriaDTOMappingExtensions.ToCategory(categorieDTO);

            var categorieResponse = _appDbContext.categories.FirstOrDefault(c => c.CategoryId.Equals(categorie.CategoryId));
            if (categorie is null) throw new ArgumentNullException(nameof(categorie));

            categorieResponse.Name = categorie.Name;
            categorieResponse.UrlImage = categorie.UrlImage;

            _appDbContext.SaveChanges();

            return categorieDTO;
        }

        public CategoryDTO DeleteCategory(int id)
        {
            var categorie = _appDbContext.categories.Find(id);
            if (categorie is null) throw new ArgumentNullException(nameof(categorie));

            _appDbContext.categories.Remove(categorie);
            _appDbContext.SaveChanges();

           return CategoriaDTOMappingExtensions.ToCategoryDTO(categorie);
        }

        public PagedList<Category> GetCategoryPagination(CateroryParameters categoryParameter)
        {
            var categories = _appDbContext.categories.OrderBy(p => p.CategoryId).AsQueryable();

            var orderCategory = PagedList<Category>.ToPagedList(categories, categoryParameter.pageNumber, categoryParameter.pageSize);

            return orderCategory;
        }

        public PagedList<Category> GetCategoryByName(CategoryFilterName categoryFilterName)
        {
            var categories = _appDbContext.categories.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(categoryFilterName.Name))
            {
                categories = categories.Where(c => c.Name.Contains(categoryFilterName.Name));
            }

            return PagedList<Category>.ToPagedList(categories, categoryFilterName.pageNumber, categoryFilterName.pageSize);
        }
    }
}
