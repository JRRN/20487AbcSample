using System.Collections.Generic;
using System.Threading.Tasks;
using AbcSample.Entities.Pagination;
using Microsoft.WindowsAzure.Storage.Table;

namespace AbcSample.DAL.Storages.Table
{
    public interface ITableStorageManager<TEntity> where TEntity:TableEntity
    {
        Task<IEnumerable<TEntity>> GetAllByPartitionKey(string partitionKey);

        Task<IPageResult<TEntity>> GetPagedByPartitionKey(string partitionKey, IPaginationToken paginationToken);

        Task<TEntity> GetByPartitionRowKey(string partitionKey, string rowKey);

        Task BatchUpsert(IEnumerable<TEntity> countries);
    }
}