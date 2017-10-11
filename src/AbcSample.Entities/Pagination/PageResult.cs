using System.Collections.Generic;

namespace AbcSample.Entities.Pagination
{
    public class PageResult<T> : IPageResult<T>
    {
        public PageResult(IEnumerable<T> records) : this(records, null)
        {
        }

        public PageResult(IEnumerable<T> records, string continuationToken)
        {
            ContinuationToken = continuationToken;
            Result = records;
        }

        public string ContinuationToken { get; }

        public IEnumerable<T> Result { get; }
        
    }
}
