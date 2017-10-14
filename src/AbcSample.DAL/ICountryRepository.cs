using System.Collections.Generic;
using System.Threading.Tasks;
using AbcSample.Entities;
using AbcSample.Entities.Pagination;

namespace AbcSample.DAL
{
    public interface ICountryRepository
    {
        Task Upsert(IList<Country> country);

        Task<IEnumerable<Country>> GetAllCountries();
    }
}