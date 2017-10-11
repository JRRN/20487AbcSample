using System.Collections.Generic;
using System.Threading.Tasks;
using AbcSample.Entities;
using AbcSample.Entities.Pagination;

namespace AbcSample.DAL
{
    public interface ICountryRepository
    {
        Task Upsert(IEnumerable<Country> country);

        Task<IPageResult<Country>> GetAllCountries();
    }
}