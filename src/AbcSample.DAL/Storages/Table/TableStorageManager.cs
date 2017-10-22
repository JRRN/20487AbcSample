using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbcSample.Entities.Pagination;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AbcSample.DAL.Storages.Table
{
    public class TableStorageManager<TEntity> : ITableStorageManager<TEntity>
    {
        private readonly Func<DynamicTableEntity, TEntity> _map2Entity;
        private readonly Func<TEntity, DynamicTableEntity> _map2Table;
        private readonly CloudTable _table;

        public TableStorageManager(string tableName, Func<DynamicTableEntity, TEntity> map2Entity, Func<TEntity, DynamicTableEntity> map2Table)
        {
            _map2Entity = map2Entity;
            _map2Table = map2Table;
            _table = CreateTable(tableName);
        }

        private CloudTable CreateTable(string tableName)
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference(tableName);

            // Create the table if it doesn't exist.
            table.CreateIfNotExists();

            return table;
        }

        public async Task<IEnumerable<TEntity>> GetAllByPartitionKey(string partitionKey)
        {
            List<TEntity> result = new List<TEntity>();
            TableQuery<DynamicTableEntity> query = new TableQuery<DynamicTableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<DynamicTableEntity> seg = await _table.ExecuteQuerySegmentedAsync(query, token);

                token = seg.ContinuationToken;

                result.AddRange(seg.Select(_map2Entity).ToList());

            } while (token != null);


            return result;
        }

        public async Task<IPageResult<TEntity>> GetPagedByPartitionKey(string partitionKey, IPaginationToken paginationToken)
        {
            List<TEntity> result = new List<TEntity>();
            TableContinuationToken token = string.IsNullOrWhiteSpace(paginationToken.Token)
                ? null
                : TableStorageTokenSerializer.DeserializeToken(paginationToken.Token);

            TableQuery<DynamicTableEntity> query = new TableQuery<DynamicTableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));

            TableQuerySegment<DynamicTableEntity> seg = await _table.ExecuteQuerySegmentedAsync(query, token);

            string serializedToken = seg.ContinuationToken != null ? TableStorageTokenSerializer.SerializeToken(seg.ContinuationToken) : null;

            result.AddRange(seg.Select(_map2Entity).ToList());

            return new PageResult<TEntity>(result, serializedToken);
        }

        public Task<TEntity> GetByPartitionRowKey(string partitionKey, string rowKey)
        {
            throw new NotImplementedException();
        }

        public async Task BatchUpsert(IList<TEntity> listToInsert)
        {
            int initialRecord = 0;
            int totalRecords = listToInsert.Count;
            const int pageSize = 100;
            
            do
            {
                var batchGroup = listToInsert.Skip(initialRecord).Take(pageSize).ToList();
                TableBatchOperation batchOperation = new TableBatchOperation();
                foreach (TEntity item in batchGroup)
                {
                    var row = _map2Table(item);
                    batchOperation.Insert(row);
                }
                await _table.ExecuteBatchAsync(batchOperation);

                initialRecord = initialRecord + pageSize;

            } while (initialRecord<totalRecords);
        }
    }
}