namespace DineHub.Application.Common;

public class PageResult<T>
{
    public PageResult(List<T> items, int totalItemsCount, int pageSize, int pageNumber)
    {
        Items = items;
        TotalPages = (int)Math.Ceiling(totalItemsCount / (double)pageSize);
        TotalItemsCount = totalItemsCount;
        ItemsFrom = pageSize * (pageNumber-1) + 1;
        ItemsTo = ItemsFrom + pageSize - 1;
    }
    public List<T> Items { get; }
    public int TotalPages { get; }
    public int TotalItemsCount { get; }
    public int ItemsFrom { get; }
    public int ItemsTo { get; }

}