namespace CatalogAPI.Pagination
{
    public abstract class QueryStringPagination
    {
        const int maxPageSize = 50;
        public int pageNumber { get; set; } = 1;
        public int _pageSize = maxPageSize;
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
