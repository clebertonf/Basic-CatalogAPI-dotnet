namespace CatalogAPI.Pagination.Filter
{
    public class CategoryFilterName : QueryStringPagination
    {
        public string? Name { get; set; }
    }
}
