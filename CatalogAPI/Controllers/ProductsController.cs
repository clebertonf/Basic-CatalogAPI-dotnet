using CatalogAPI.Context;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _appDbContext.products.ToList();
            if (products is null) return NotFound();

            return products;
        }

        [HttpGet("{id:int}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _appDbContext.products.FirstOrDefault(p => p.ProductId.Equals(id));
            if (product is null) return NotFound("Product not found!");

            return product;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            if (product is null) return BadRequest();

            _appDbContext.products.Add(product);
            _appDbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = product.ProductId}, product);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Product product)
        {
            if (!id.Equals(product.ProductId)) return BadRequest();

            /*
             * Pode ser feito assim, com algumas desvantagens:
             * _appDbContext.Entry(product).State = EntityState.Modified;
             _appDbContext.SaveChanges();
            */

            var productData = _appDbContext.products.FirstOrDefault(p => p.ProductId.Equals(id));
            if (productData is null) return NotFound();

            productData.Name = product.Name;
            productData.Description = product.Description;

            _appDbContext.SaveChanges();

            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
           var product = _appDbContext.products.FirstOrDefault(p => p.ProductId.Equals(id));
           if (product is null) return NotFound();

           _appDbContext.products.Remove(product);
           _appDbContext.SaveChanges();

            return NoContent();
        }
    }
}
