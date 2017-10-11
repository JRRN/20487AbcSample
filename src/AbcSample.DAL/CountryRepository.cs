using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbcSample.Entities;
using AbcSample.Entities.Pagination;

namespace AbcSample.DAL
{
    public class CountryRepository:ICountryRepository
    {
        public CountryRepository()
        {
            //Create storage
        }
        

        public Task Upsert(IEnumerable<Country> country)
        {
            throw new NotImplementedException();
        }

        public Task<IPageResult<Country>> GetAllCountries()
        {
            throw new NotImplementedException();
        }
    }
}
