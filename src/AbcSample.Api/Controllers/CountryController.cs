using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using AbcSample.Api.Models;

namespace AbcSample.Api.Controllers
{
    public class CountryController : ApiController
    {
        [HttpGet]
        [ResponseType(typeof(IEnumerable<CountryResponse>))]
        public IHttpActionResult GetAll()
        {
            var list = new List<CountryResponse>
            {
                new CountryResponse {Id = "ESP", Description = "Spain"},
                new CountryResponse {Id = "ARG", Description = "Argentine"},
                new CountryResponse {Id = "FRA", Description = "France"}
            };

            return Ok(list);
        }
    }
}
