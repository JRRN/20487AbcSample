using System.Collections.Generic;
using AbcSample.Entities;

namespace AbcSample.Service
{
    public interface ICountryService
    {
        void AddAll(IEnumerable<Country> countries);
    }
}