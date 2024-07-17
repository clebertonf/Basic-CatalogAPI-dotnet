using CatalogAPI.DTOs;
using CatalogAPI.Filters;
using CatalogAPI.Models;
using CatalogAPI.Repositories.Interfaces;
using CatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        [HttpGet("config")]
        public IActionResult GetConfigAppSettings([FromServices] IConfiguration configuration)
        {
            // throw new Exception();
            // var config = configuration["KeyName"];
            var config = configuration.Get<AppSettings>();

            return Ok($"ProjectName {config?.ProjectName} and DefaultTimeOut {config?.DefaultTimeOut}");
        }

        [HttpGet("service-teste")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public IActionResult GetBasicService([FromServices] IBasicService basicService)
        {
            return Ok(basicService.GetMessage());
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_categoriesRepository.GetAllCategories(10));
        }

        [HttpGet("/categories/products")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public IActionResult GetProducts() 
        { 
            return Ok(_categoriesRepository.GeTAllCategoriesWithProducts());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var category = _categoriesRepository.GetCategory(id);
            if (category is null) return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public IActionResult Post(CategoryDTO category)
        {
            if (category is null) return BadRequest();

            _categoriesRepository.CreateCategory(category);

            return Created();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, CategoryDTO category)
        {
            return Ok(_categoriesRepository.UpdateCategory(category));
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var categorie = _categoriesRepository.DeleteCategory(id);

            if (categorie is null) return BadRequest();

            return NoContent();
        }
    }
}
