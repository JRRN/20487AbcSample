using System.Collections.Generic;
using System.Threading.Tasks;
using AbcSample.Entities;

namespace AbcSample.Service
{
    public interface ICountryService
    {
        void AddAll(IEnumerable<Country> countries);

        Task<IEnumerable<Country>> GetAllCountries();
    }
}