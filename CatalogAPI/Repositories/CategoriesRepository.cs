using CatalogAPI.Context;
using CatalogAPI.DTOs;
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

        public IEnumerable<CategoryDTO> GetAllCategories(int size)
        {
            var categories = _appDbContext.categories.Take(size).AsNoTracking().ToList();
            var categoriesDTO = new List<CategoryDTO>();

            foreach (var categorie in categories)
            {
                var categoryDTO = new CategoryDTO()
                {
                    CategoryId = categorie.CategoryId,
                    Name = categorie.Name,
                    UrlImage = categorie.UrlImage
                };
                categoriesDTO.Add(categoryDTO);
            };

            return categoriesDTO;
        }

        public IEnumerable<Category> GeTAllCategoriesWithProducts()
        {
           return _appDbContext.categories.Include(c => c.Products).ToList();
        }

        public CategoryDTO GetCategory(int id)
        {
            var categorie = _appDbContext.categories.FirstOrDefault(c => c.CategoryId.Equals(id));
            if (categorie is null) throw new ArgumentNullException(nameof(categorie));

            return new CategoryDTO()
            {
                CategoryId = categorie.CategoryId,
                Name = categorie.Name,
                UrlImage = categorie.UrlImage
            };
        }

        public CategoryDTO CreateCategory(CategoryDTO categorieDTO)
        {
            if (categorieDTO is null) throw new ArgumentNullException(nameof(categorieDTO));

            var categorie = new Category()
            {
                Name = categorieDTO.Name,
                UrlImage = categorieDTO.UrlImage
            };

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

            var categorie = new Category()
            {
                CategoryId = categorieDTO.CategoryId,
                Name = categorieDTO.Name,
                UrlImage = categorieDTO.UrlImage
            };

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

            return new CategoryDTO()
            {
                Name = categorie.Name,
                UrlImage = categorie.UrlImage
            };
        }
    }
}
