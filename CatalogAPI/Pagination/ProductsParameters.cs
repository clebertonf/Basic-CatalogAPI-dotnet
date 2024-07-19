namespace CatalogAPI.Pagination
{
    public class ProductsParameters
    {
        public int maxPageSize = 50;
        public int pageNumber { get; set; } = 1;
        public int _pageSize;
        public int pageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
