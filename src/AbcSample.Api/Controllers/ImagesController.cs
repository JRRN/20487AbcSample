using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AbcSample.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ImagesController : ApiController
    {
        [HttpPost, Route("api/images/uploadOriginal")]
        public async Task<IHttpActionResult> UploadOriginal()
        {
            // validar que es multipart
            // conectar con el container images dentro del blob storage
            // crear y leer por medio del proveedor que esta en helper
            // devolver el ok

            return await Task.FromResult(Ok());
        }

        [HttpGet, Route("api/images/thumbnails")]
        public async Task<IHttpActionResult> GetThumbnails()
        {
            var url = "https://pbs.twimg.com/profile_images/3210576643/84355abe2ed11d0e4c32ccb6a3b6841a_400x400.jpeg";
            var robot = "https://image.flaticon.com/teams/slug/freepik.jpg";
            var flechaUbic = "http://www.eniacinformatica.net/wp-content/uploads/2016/06/Map-Marker-Marker-Outside-Azure-icon.png";

            return await Task.FromResult(Ok(new[] { url, robot, flechaUbic }));

            // crear una conexion con el contenedor thumbs en blob storage
            // asignar los permisos publicos al contenedor
            // buscar los registros llamando al metodo ListBlobsSegmentedAsync y volcar las url de cada item en una coleccion
            // repetir la busqueda mientras el continuation token no sea nulo
            // luego, devolver el resultado
        }
    }
}