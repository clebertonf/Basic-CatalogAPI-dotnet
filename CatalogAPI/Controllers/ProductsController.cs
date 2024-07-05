using CatalogAPI.Context;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ProductsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [HttpGet("test")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var products = await _appDbContext.products.Take(10).AsNoTracking().ToListAsync();
                if (products is null) return NotFound();

                return Ok(products);


            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"This is a error! {ex.Message}"); // example
            }
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<Product>> GetByIdAsync(int id)
        {
            var product = await _appDbContext.products.FirstOrDefaultAsync(p => p.ProductId.Equals(id));
            if (product is null) return NotFound("Product not found!");

            return product;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Product product)
        {
            if (product is null) return BadRequest();

            await _appDbContext.products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByIdAsync), new { id = product.ProductId}, product);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] Product product)
        {
            if (!id.Equals(product.ProductId)) return BadRequest();

            /*
             * Pode ser feito assim, com algumas desvantagens:
             * _appDbContext.Entry(product).State = EntityState.Modified;
             _appDbContext.SaveChanges();
            */

            var productData = await _appDbContext.products.FirstOrDefaultAsync(p => p.ProductId.Equals(id));
            if (productData is null) return NotFound();

            productData.Name = product.Name;
            productData.Description = product.Description;

            await _appDbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
           var product = await _appDbContext.products.FirstOrDefaultAsync(p => p.ProductId.Equals(id));
           if (product is null) return NotFound();

           _appDbContext.products.Remove(product);
           await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
