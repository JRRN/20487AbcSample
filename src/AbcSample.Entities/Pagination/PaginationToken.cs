namespace AbcSample.Entities.Pagination
{
    public class PaginationToken : IPaginationToken
    {
        public string Token { get; }
        public int PageSize { get; }

        private const int DefaultPageSize = 50;
        public PaginationToken()
        {
            PageSize = DefaultPageSize;
        }

        public PaginationToken(int pageSize) :this(pageSize, null)
        {
        }

        public PaginationToken(int pageSize, string continuationToken)
        {
            PageSize = pageSize;
            Token = continuationToken;
        }
    }
}
