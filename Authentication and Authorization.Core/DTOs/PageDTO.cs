namespace Authentication_and_Authorization.Core.DTOs
{
    public class PageDTO<T>
    {
        public int Page { get; private set; }
        public int Total { get; private set; }
        public IEnumerable<T> ItemList { get; private set; }

        public PageDTO(int page, int total, IEnumerable<T> itemList)
        {
            Page = page;
            Total = total;
            ItemList = itemList;
        }
    }
}
