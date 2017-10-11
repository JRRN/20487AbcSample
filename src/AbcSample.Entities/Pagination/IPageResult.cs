using System.Collections.Generic;

namespace AbcSample.Entities.Pagination
{
    public interface IPageResult<out TEntity>
    {
        string ContinuationToken { get; }
        IEnumerable<TEntity> Result { get; }
    }
}