﻿using CatalogAPI.DTOs;
using CatalogAPI.Filters;
using CatalogAPI.Models;
using CatalogAPI.Pagination;
using CatalogAPI.Pagination.Filter;
using CatalogAPI.Repositories.Interfaces;
using CatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public async Task<IActionResult> Get()
        {
            return Ok(await _categoriesRepository.GetAllCategoriesAsync(10));
        }

        [HttpGet("pagination")]
        public IActionResult GetPagination([FromQuery] CateroryParameters cateroryParameters)
        {
            var response = _categoriesRepository.GetCategoryPagination(cateroryParameters);

            var metaData = new
            {
                response.TotalCount,
                response.PageSize,
                response.CurrentPage,
                response.TotalPages,
                response.HasNext,
                response.HasPrevious
            };

            Response.Headers.Append("X-Pagination-Info", JsonConvert.SerializeObject(metaData));

            return Ok(response);
        }

        [HttpGet("pagination/filter")]
        public IActionResult GetPaginationFilter([FromQuery] CategoryFilterName categoryFilterName)
        {
            var response = _categoriesRepository.GetCategoryByName(categoryFilterName);

            var metaData = new
            {
                response.TotalCount,
                response.PageSize,
                response.CurrentPage,
                response.TotalPages,
                response.HasNext,
                response.HasPrevious
            };

            Response.Headers.Append("X-Pagination-Info", JsonConvert.SerializeObject(metaData));

            return Ok(response);
        }

        [HttpGet("/categories/products")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<IActionResult> GetProducts() 
        { 
            return Ok(await _categoriesRepository.GeTAllCategoriesWithProductsAsync());
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
