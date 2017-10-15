using System.Collections.Generic;
using System.Threading.Tasks;
using AbcSample.Entities;

namespace AbcSample.Service
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllCountries();
    }
}