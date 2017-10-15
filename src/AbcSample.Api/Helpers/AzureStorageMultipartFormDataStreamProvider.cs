using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AbcSample.Api.Controllers
{
    public class AzureStorageMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        private readonly CloudBlobContainer _blobContainer;
        private readonly string[] _supportedMimeTypes = { "image/png", "image/jpeg", "image/jpg" };

        public AzureStorageMultipartFormDataStreamProvider(CloudBlobContainer blobContainer) : base("azure")
        {
            _blobContainer = blobContainer;
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (headers == null) throw new ArgumentNullException(nameof(headers));

            if (!_supportedMimeTypes.Contains(headers.ContentType.ToString().ToLower()))
            {
                throw new NotSupportedException("Only jpeg and png are supported");
            }
            var originalFileName = headers.ContentDisposition.FileName.Replace("\"", " ").Trim();
            var fileName = $"{Guid.NewGuid():N}-{originalFileName}";
            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(fileName);

            if (headers.ContentType != null)
            {
                // Set appropriate content type for your uploaded file
                blob.Properties.ContentType = headers.ContentType.MediaType;
            }

            FileData.Add(new MultipartFileData(headers, blob.Name));

            return blob.OpenWrite();
        }
    }
}