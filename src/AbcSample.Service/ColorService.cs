using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using AbcSample.DAL;

namespace AbcSample.Service
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly KnownColor[] _names = (KnownColor[])Enum.GetValues(typeof(KnownColor));

        public ColorService(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }
        public async Task<IEnumerable<string>> GetAllColors()
        {
            return await _colorRepository.GetAllColors();
        }


        public async Task UpdateRandomColors()
        {
            var random = new Random();

            var newsColor = new List<string>
            {
                GetRandomColor(random.Next(_names.Length)),
                GetRandomColor(random.Next(_names.Length)),
                GetRandomColor(random.Next(_names.Length))
            };

            await _colorRepository.Upsert(newsColor);
        }

        private string GetRandomColor(int indexOfColor)
        {
            var color = _names[indexOfColor];
            return color.ToString();
        }
    }
}