using CatalogAPI.Models;
using CatalogAPI.Pagination;
using CatalogAPI.Pagination.Filter;

namespace CatalogAPI.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        public IEnumerable<Product> GetProducts(int size);
        // public IEnumerable<Product> GetProductsPagination(ProductsParameters productsParameters);
        public PagedList<Product> GetProductsPagination(ProductsParameters productsParameters);
        public PagedList<Product> GetProductsByFilter(ProductsFilterPrice productsFilterPrice);
        public Product GetProduct(int id);
        public Product CreateProduct(Product product);
        public Product UpdateProduct(Product product);
        public Product DeleteProduct(int id);
    }
}
