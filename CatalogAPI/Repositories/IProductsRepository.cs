using CatalogAPI.Models;

namespace CatalogAPI.Repositories
{
    public interface IProductsRepository
    {
        public IEnumerable<Product> GetProducts(int size);
        public Product GetProduct(int id);
        public Product CreateProduct(Product product);
        public Product UpdateProduct(Product product);
        public Product DeleteProduct(int id);
    }
}
