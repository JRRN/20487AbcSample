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
            return new CountryResponse
            {
                Id = value.Id,
                Description = value.Description
            };
        }

        public override Country From(CountryResponse value)
        {
            return new Country
            {
                Id = value.Id,
                Description = value.Description
            };
        }
    }
}