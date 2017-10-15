using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AbcSample.Api.Models;
using AbcSample.Entities;
using AbcSample.Service;
using AbcSample.XCutting.Mapper;

namespace AbcSample.Api.Controllers
{
    public class CountryController : ApiController
    {
        private readonly ICountryService _countryService;
        private readonly IMapper<CountryResponse, Country> _mapper;

        public CountryController(ICountryService countryService, IMapper<CountryResponse, Country> mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }
        [HttpGet]
        [ResponseType(typeof(IEnumerable<CountryResponse>))]
        public async Task<IHttpActionResult> GetAll()
        {
            //var list = new List<CountryResponse>
            //{
            //    new CountryResponse {Id = "ESP", Description = "Spain"},
            //    new CountryResponse {Id = "ARG", Description = "Argentine"},
            //    new CountryResponse {Id = "FRA", Description = "France"}
            //};
            IEnumerable<Country> countryList = await _countryService.GetAllCountries();
            var result = _mapper.From(countryList);
            return Ok(result);
        }
    }
}
