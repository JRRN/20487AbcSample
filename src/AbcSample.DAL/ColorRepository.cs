using System.Collections.Generic;
using System.Threading.Tasks;
using AbcSample.DAL.Storages.Blob;
using AbcSample.Entities;

namespace AbcSample.DAL
{
    public class ColorRepository : IColorRepository
    {
        private readonly IBlobStorageManager<ColorListWrapper> _storage;
        private const string ColorContainerName = "colors";
        private const string BlobName = "unique-colors-entry";

        public ColorRepository()
        {
            _storage = new BlobStorageManager<ColorListWrapper>(ColorContainerName);
        }

        public async Task Upsert(IList<string> colors)
        {
            ColorListWrapper objColor = new ColorListWrapper {Colors = colors};
            await _storage.UploadAndSerializeAsJsonAsync(objColor, BlobName);
        }

        public async Task<IEnumerable<string>> GetAllColors()
        {
            var container = await _storage.DownloadAndDeserializeJsonAsync(BlobName);
            return container.Colors;

        }
    }
}