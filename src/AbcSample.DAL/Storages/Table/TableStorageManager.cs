using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbcSample.Entities.Pagination;
using Microsoft.WindowsAzure.Storage.Table;

namespace AbcSample.DAL.Storages.Table
{
    public class TableStorageManager<TEntity> : ITableStorageManager<TEntity> where TEntity : TableEntity
    {
        public Task<IEnumerable<TEntity>> GetAllByPartitionKey(string partitionKey)
        {
            throw new NotImplementedException();
        }

        public Task<IPageResult<TEntity>> GetPagedByPartitionKey(string partitionKey, IPaginationToken paginationToken)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByPartitionRowKey(string partitionKey, string rowKey)
        {
            throw new NotImplementedException();
        }

        public Task BatchUpsert(IEnumerable<TEntity> countries)
        {
            throw new NotImplementedException();
        }
    }
}