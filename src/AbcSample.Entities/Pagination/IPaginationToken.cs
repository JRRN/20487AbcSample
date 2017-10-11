namespace AbcSample.Entities.Pagination
{
    public interface IPaginationToken
    {
        int PageSize { get; }
        string Token { get; }
    }
}