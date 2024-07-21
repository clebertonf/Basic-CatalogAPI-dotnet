using CatalogAPI.Models;
using CatalogAPI.Pagination;
using CatalogAPI.Pagination.Filter;
using CatalogAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet("pagination")]
        public IActionResult GetPagination([FromQuery] ProductsParameters productsParameters)
        {
            // return Ok(_productsRepository.GetProductsPagination(productsParameters));
            var productsPagination = _productsRepository.GetProductsPagination(productsParameters);

            var metaData = new
            {
                productsPagination.TotalCount,
                productsPagination.PageSize,
                productsPagination.CurrentPage,
                productsPagination.TotalPages,
                productsPagination.HasNext,
                productsPagination.HasPrevious
            };

            Response.Headers.Append("X-Pagination-Info", JsonConvert.SerializeObject(metaData));

            return Ok(productsPagination);
        }

        [HttpGet("pagination/filter")]
        public IActionResult GetPagination([FromQuery] ProductsFilterPrice productsFilterPrice)
        {
            // return Ok(_productsRepository.GetProductsPagination(productsParameters));
            var productsPagination = _productsRepository.GetProductsByFilter(productsFilterPrice);

            var metaData = new
            {
                productsPagination.TotalCount,
                productsPagination.PageSize,
                productsPagination.CurrentPage,
                productsPagination.TotalPages,
                productsPagination.HasNext,
                productsPagination.HasPrevious
            };

            Response.Headers.Append("X-Pagination-Info", JsonConvert.SerializeObject(metaData));

            return Ok(productsPagination);
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
