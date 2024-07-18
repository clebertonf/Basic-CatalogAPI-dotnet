using CatalogAPI.Models;

namespace CatalogAPI.DTOs.Extensions
{
    public static class CategoriaDTOMappingExtensions
    {
        public static CategoryDTO? ToCategoryDTO(this Category category)
        {
            if(category is null) return new CategoryDTO();

            return new CategoryDTO()
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                UrlImage = category.UrlImage,
            };
        }

        public static Category? ToCategory(this CategoryDTO categoryDTO) 
        {
            if (categoryDTO is null) return new Category();

            return new Category()
            {
                CategoryId = categoryDTO.CategoryId,
                Name = categoryDTO.Name,
                UrlImage = categoryDTO.UrlImage,
            };
        }

        public static IEnumerable<CategoryDTO> TocategoryDTOList(IEnumerable<Category> categoryDTO)
        {
            if (categoryDTO is null | !categoryDTO.Any())
                return new List<CategoryDTO>();

            return categoryDTO.Select(c => new CategoryDTO()
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                UrlImage = c.UrlImage,
            }).ToList();
        }
    }
}
