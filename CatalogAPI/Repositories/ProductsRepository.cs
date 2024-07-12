using CatalogAPI.Context;
using CatalogAPI.Models;

namespace CatalogAPI.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _appDbContext.products.ToList();
        }

        public Product GetProduct(int id)
        {
            var product = _appDbContext.products.FirstOrDefault(p => p.ProductId.Equals(id));
            if (product is null)
                  throw new ArgumentNullException(nameof(GetProduct));

            return product;
        }

        public Product CreateProduct(Product product)
        {
           if(product is null)
                throw new ArgumentNullException(nameof(CreateProduct));

           _appDbContext.products.Add(product);
           _appDbContext.SaveChanges();

            return product;
        }

        public Product UpdateProduct(Product product)
        {
            var productResponse = _appDbContext.products.FirstOrDefault(p => p.ProductId.Equals(product.ProductId));

            if (productResponse is null)
                throw new ArgumentNullException(nameof(UpdateProduct));

            productResponse.Name = product.Name;
            productResponse.Price = product.Price;
            productResponse.Description = product.Description;

            _appDbContext.SaveChanges();
            return productResponse;
        }
        public Product DeleteProduct(int id)
        {
            var productResponse = _appDbContext.products.FirstOrDefault(p => p.ProductId.Equals(id));
            if (productResponse is null)
                throw new ArgumentNullException(nameof(DeleteProduct));

            _appDbContext.products.Remove(productResponse);
            _appDbContext.SaveChanges();

            return productResponse;
        }
    }
}
