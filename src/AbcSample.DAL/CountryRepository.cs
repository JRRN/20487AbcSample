using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbcSample.DAL.Storages.Table;
using AbcSample.Entities;
using AbcSample.Entities.Pagination;
using Microsoft.WindowsAzure.Storage.Table;

namespace AbcSample.DAL
{
    public class CountryRepository:ICountryRepository
    {
        const string PartitionKeyCountry = "countries";
        private readonly ITableStorageManager<Country> _tableStorage;

        public CountryRepository()
        {
            _tableStorage = new TableStorageManager<Country>("masterData", Map2Entity, Map2Table);
        }

        Country Map2Entity(DynamicTableEntity tableEntity)
        {
            return new Country
            {
                Id = tableEntity.RowKey,
                Description = tableEntity.Properties["description"].StringValue
            };
        }

        DynamicTableEntity Map2Table(Country input)
        {
            var table = new DynamicTableEntity{
                PartitionKey = PartitionKeyCountry,
                RowKey = input.Id,
                Properties = new Dictionary<string, EntityProperty>
                {
                    {"description", new EntityProperty(input.Description)}
                }
            };

            return table;
        }
        public async Task Upsert(IList<Country> country)
        {
            await _tableStorage.BatchUpsert(country);
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await _tableStorage.GetAllByPartitionKey(PartitionKeyCountry);
        }
    }
}
