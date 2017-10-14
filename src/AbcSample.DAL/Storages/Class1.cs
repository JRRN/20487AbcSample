using System;

namespace AbcSample.DAL.Storages
{
    public enum StorageType
    {
        None,
        AzureTable,
        Blob
    }

    public interface IStoragePersistance
    {
        StorageType StorageType { get; }
    }

    public class BlobStorageAttribute : Attribute, IStoragePersistance
    {
        public StorageType StorageType { get; }

        public BlobStorageAttribute()
        {
            StorageType = StorageType.Blob;
        }
    }

    public class TableStorageAttribute : Attribute, IStoragePersistance
    {
        public StorageType StorageType { get; }

        public string EntityName { get; }

        public TableStorageAttribute(string entityName = null)
        {
            StorageType = StorageType.AzureTable;

            EntityName = entityName;
        }
    }
}
