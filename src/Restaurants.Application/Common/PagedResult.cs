namespace Restaurants.Application.Common
{
    public class PagedResult<T>
    {
        public PagedResult(IEnumerable<T> items, int totalCount, int pageSize, int pageNumber)
        {
            // pagesize = 5
            // totalCount = 100
            // pageNumber(curr page) = 3 => 11, 12, 13, 14, 15

            Items = items;
            TotalItemsCount = totalCount;

            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;

        }
        public IEnumerable<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int TotalItemsCount { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
    }
}
