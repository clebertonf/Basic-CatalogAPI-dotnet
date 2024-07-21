namespace CatalogAPI.Pagination.Filter
{
    public class ProductsFilterPrice : QueryStringPagination
    {
        public decimal? Price { get; set; }
        public string? Criterion { get; set; }
    }
}
