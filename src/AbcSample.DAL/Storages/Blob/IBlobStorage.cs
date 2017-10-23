using System.Threading.Tasks;

namespace AbcSample.DAL.Storages.Blob
{
    public interface IBlobStorageManager<TEntity>
    {
        Task UploadAndSerializeAsJsonAsync(TEntity obj, string blobName);

        Task<TEntity> DownloadAndDeserializeJsonAsync(string blobName);
    }
}