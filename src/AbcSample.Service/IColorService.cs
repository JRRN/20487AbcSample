using System.Collections.Generic;
using System.Threading.Tasks;

namespace AbcSample.Service
{
    public interface IColorService
    {
        Task<IEnumerable<string>> GetAllColors();

        Task UpdateRandomColors();
    }
}