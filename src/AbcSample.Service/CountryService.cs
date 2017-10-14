using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbcSample.DAL;
using AbcSample.Entities;

namespace AbcSample.Service
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public void AddAll(IEnumerable<Country> countries)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Country>> GetAllCountries()
        {
            return _countryRepository.GetAllCountries();
        }
    }
}
