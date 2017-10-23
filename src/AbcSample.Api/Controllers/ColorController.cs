using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using AbcSample.Service;

namespace AbcSample.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ColorController : ApiController
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<string>))]
        public async Task<IHttpActionResult> GetAll()
        {
            var result = await _colorService.GetAllColors();
            if (result.Any())
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}