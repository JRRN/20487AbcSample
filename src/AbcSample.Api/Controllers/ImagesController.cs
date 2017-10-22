using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using AbcSample.Api.Helpers;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AbcSample.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ImagesController : ApiController
    {
        [HttpPost, Route("api/images/uploadOriginal")]
        public async Task<IHttpActionResult> UploadOriginal()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer imagesContainer = blobClient.GetContainerReference("images");
            var provider = new AzureStorageMultipartFormDataStreamProvider(imagesContainer);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error has occured. Details: {ex.Message}");
            }

            var filename = provider.FileData.FirstOrDefault()?.LocalFileName;
            if (string.IsNullOrEmpty(filename))
            {
                return BadRequest("An error has occured while uploading your file. Please try again.");
            }

            return Ok();
        }

        [HttpGet, Route("api/images/thumbnails")]
        public async Task<IHttpActionResult> GetThumbnails()
        {
            List<string> thumbnailUrls = new List<string>();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer thumbContainer = blobClient.GetContainerReference("thumbs");

            await thumbContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            BlobContinuationToken continuationToken = null;

            do
            {
                var resultSegment = await thumbContainer.ListBlobsSegmentedAsync("", true, BlobListingDetails.All, 10, continuationToken, null, null);

                foreach (var blobItem in resultSegment.Results)
                {
                    thumbnailUrls.Add(blobItem.StorageUri.PrimaryUri.ToString());
                }

                continuationToken = resultSegment.ContinuationToken;
            }
            while (continuationToken != null);

            return await Task.FromResult(Ok(thumbnailUrls));
        }
    }
}