using System;
using AbcSample.Api.Models;
using AbcSample.Entities;
using AbcSample.XCutting.Mapper;

namespace AbcSample.Api.Mappers
{
    public class CountryMapper : MapperBase<CountryResponse, Country>
    {
        public override CountryResponse From(Country value)
        {
            throw new NotImplementedException();
        }

        public override Country From(CountryResponse value)
        {
            throw new NotImplementedException();
        }
    }
}