using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace AbcSample.DAL.Storages.Blob
{
    public class BlobStorageManager<TEntity> : IBlobStorageManager<TEntity>
    {
        private readonly CloudBlobClient _client;
        private readonly string _containerName;
        public BlobStorageManager(string containerName)
        {
            _containerName = containerName ?? throw new ArgumentNullException(nameof(containerName));

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            _client = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = _client.GetContainerReference(containerName);
            bool containerCreated = container.CreateIfNotExists();
            if (containerCreated)
            {
                container.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }
        }
        public async Task UploadAndSerializeAsJsonAsync(TEntity obj, string blobName)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };

            using (Stream jsonStream = new MemoryStream())
            {
                string json = JsonConvert.SerializeObject(obj, settings);
                StreamWriter writer = new StreamWriter(jsonStream, Encoding.UTF8);
                await writer.WriteAsync(json);
                await writer.FlushAsync();
                jsonStream.Position = 0;

                await UploadBlobAsync(jsonStream, blobName, MdpBlobContentType.Json);
            }
        }

        public async Task<TEntity> DownloadAndDeserializeJsonAsync(string blobName)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };

            using (Stream jsonStream = new MemoryStream())
            {
                await DownloadAsync(jsonStream, blobName);
                jsonStream.Position = 0;
                using (var sr = new StreamReader(jsonStream, Encoding.UTF8))
                {
                    var jsonStr = sr.ReadToEnd();
                    return JsonConvert.DeserializeObject<TEntity>(jsonStr, settings);
                }
            }
        }

        private async Task DownloadAsync(Stream stream, string blobName)
        {
            if (string.IsNullOrWhiteSpace(blobName)) throw new ArgumentNullException(nameof(blobName));
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            CloudBlockBlob blockBlob = GetBlockBlob(blobName);
            await blockBlob.DownloadToStreamAsync(stream);
        }

        private async Task UploadBlobAsync(Stream blobContent, string blobName, MdpBlobContentType? contentType = null)
        {
            if (blobContent == null || blobContent.Length <= 0)
                throw new ArgumentNullException(nameof(blobContent), "blobContent cannot be null or empty");
            if (string.IsNullOrEmpty(blobName))
                throw new ArgumentNullException(nameof(blobName), "the blob name cannot be null or empty");

            CloudBlockBlob blockBlob = GetBlockBlob(blobName);

            if (await blockBlob.ExistsAsync())
            {
                await DeleteAsync(blobName);
            }

            // version simplificada, no controlo el tamaño
            await blockBlob.UploadFromStreamAsync(blobContent);

            bool setProperties = false;

            string blobContentType = contentType?.ToDescription();
            if (!string.IsNullOrEmpty(blobContentType))
            {
                blockBlob.Properties.ContentType = blobContentType;
                setProperties = true;
            }

            if (setProperties)
            {
                await blockBlob.SetPropertiesAsync();
            }
        }

        private CloudBlockBlob GetBlockBlob(string blobName)
        {
            CloudBlobContainer container = _client.GetContainerReference(_containerName);
            return container.GetBlockBlobReference(blobName);
        }

        public async Task DeleteAsync(string blobName)
        {
            if (string.IsNullOrWhiteSpace(blobName)) throw new ArgumentNullException(nameof(blobName));

            CloudBlockBlob blockBlob = GetBlockBlob(blobName);
            try
            {
                await blockBlob.DeleteAsync();
            }
            catch (StorageException e)
            {
                if (e.RequestInformation.HttpStatusCode == 404)
                {
                    return;
                }
                throw;
            }
        }
    }
}