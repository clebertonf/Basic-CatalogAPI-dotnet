﻿using CatalogAPI.Context;
using CatalogAPI.Filters;
using CatalogAPI.Models;
using CatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CategoriesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("config")]
        public IActionResult GetConfigAppSettings([FromServices] IConfiguration configuration)
        {
            // throw new Exception("Error getting settings!");
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
            return Ok(_appDbContext.categories.Take(10).AsNoTracking().ToList());
        }

        [HttpGet("/categories/products")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public IActionResult GetProducts() 
        { 
            return Ok(_appDbContext.categories.Include(p => p.Products).ToList());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var category = _appDbContext.categories.FirstOrDefault(c => c.CategoryId.Equals(id));
            if (category is null) return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public IActionResult Post(Category category)
        {
            if (category is null) return BadRequest();

            _appDbContext.categories.Add(category);
            _appDbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, category);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, Category category)
        {
            if (!category.CategoryId.Equals(id)) return BadRequest();

            /*
            * Pode ser feito assim, com algumas desvantagens:
            * _appDbContext.Entry(category).State = EntityState.Modified;
            _appDbContext.SaveChanges();
           */

            var categoryResult = _appDbContext.categories.FirstOrDefault(c => c.CategoryId.Equals(id));
            if (categoryResult is null) return NotFound();

            categoryResult.Name = category.Name;
            categoryResult.UrlImage = category.UrlImage;

            _appDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var categorie = _appDbContext.categories.FirstOrDefault(c => c.CategoryId.Equals(id));
            if (categorie is null) return NotFound();

            _appDbContext.Remove(categorie);
            _appDbContext.SaveChanges();

            return NoContent();
        }
    }
}
