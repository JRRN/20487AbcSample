using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RestSharp;

namespace AbcSample.WebApp.Controllers
{
    public class ImagesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // POST /api/images/upload
        //[HttpPost]
        //public async Task<ActionResult> Upload()
        //{
        //    bool isUploaded = false;

        //    try
        //    {
        //        for(int index = 0; index < Request.Files.Count; index++)
        //        {
        //            var file = Request.Files[index];
        //            if (file != null && file.ContentLength > 0 && IsImage(file))
        //            {
        //                byte[] buffer;
        //                using (var binaryReader = new BinaryReader(file.InputStream))
        //                {
        //                    buffer = binaryReader.ReadBytes(file.ContentLength);
        //                }
        //                //var upload = new { FileName = Path.GetFileName(file.FileName), Buffer = buffer };
        //                var client = new RestClient("http://localhost:57282/api/images/uploadOriginal");
        //                var request = new RestRequest(Method.POST) {RequestFormat = DataFormat.Json};
        //                //request.AddBody(upload);
        //                request.AddHeader("filename", file.FileName);
        //                var fp = new FileParameter();
        //                fp.
        //                request.Files.Add(new FileParameter());
        //                IRestResponse response = client.Execute(request);

        //                // send
        //                isUploaded = response.StatusCode == HttpStatusCode.Accepted;
        //            }
        //        }


        //        if (isUploaded)
        //        {
        //            return RedirectToAction("GetThumbNails");
        //        }
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //}

        private bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = { ".jpg", ".png", ".gif", ".jpeg" };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public ActionResult GetThumbNails()
        {
            throw new NotImplementedException();
        }
        //// GET /api/images/thumbnails
        //[HttpGet, Route("thumbnails")]
        //public async Task<IActionResult> GetThumbNails()
        //{

        //    try
        //    {
        //        if (storageConfig.AccountKey == string.Empty || storageConfig.AccountName == string.Empty)

        //            return BadRequest("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

        //        if (storageConfig.ImageContainer == string.Empty)

        //            return BadRequest("Please provide a name for your image container in the azure blob storage");

        //        List<string> thumbnailUrls = await StorageHelper.GetThumbNailUrls(storageConfig);

        //        return new ObjectResult(thumbnailUrls);

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

    }
}