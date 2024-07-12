using CatalogAPI.Models;
using CatalogAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductsRepository productsRepository, ILogger<ProductsController> logger)
        {
            _productsRepository = productsRepository;
            _logger = logger;
        }

        [HttpGet]
        [HttpGet("test")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogInformation("========== Searching for products ========== ");
                var products = _productsRepository.GetProducts(10);
                if (products is null) return NotFound();

                _logger.LogInformation("========== Products searched successfully! ========== ");
                return Ok(products);


            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"This is a error! {ex.Message}"); // example
            }
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<Product>> GetByIdAsync(int id) // [BindRequired] string name = ""
        {
            var product = _productsRepository.GetProduct(id);
            if (product is null) return NotFound("Product not found!");

            return product;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Product product) // [BindNever] string name = ""
        {
            if (product is null) return BadRequest();

             _productsRepository.CreateProduct(product);

            return Created();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] Product product)
        {
            if (!id.Equals(product.ProductId)) return BadRequest();

            _productsRepository.UpdateProduct(product);

            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        { 
           _productsRepository.DeleteProduct(id);
           return NoContent();
        }
    }
}
