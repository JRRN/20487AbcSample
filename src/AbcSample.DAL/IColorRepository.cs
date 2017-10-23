using System.Collections.Generic;
using System.Threading.Tasks;
using AbcSample.Entities;

namespace AbcSample.DAL
{
    public interface IColorRepository
    {
        Task Upsert(IList<string> colors);

        Task<IEnumerable<string>> GetAllColors();
    }
}