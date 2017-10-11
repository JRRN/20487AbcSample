using System.Collections.Generic;
using System.Linq;

namespace AbcSample.XCutting.Mapper
{
    public abstract class MapperBase<TApiModel, TEntityModel> : IMapper<TApiModel, TEntityModel>
    {
        public IEnumerable<TApiModel> From(IEnumerable<TEntityModel> values)
        {
            if (values == null)
            {
                return new List<TApiModel>();
            }

            return values.Where(v => v != null)
                .Select(From)
                .Where(v => v != null)
                .ToArray();
        }

        public abstract TApiModel From(TEntityModel value);

        public IEnumerable<TEntityModel> From(IEnumerable<TApiModel> values)
        {
            if (values == null)
            {
                return new List<TEntityModel>();
            }

            return values.Where(v => v != null)
                .Select(From)
                .Where(v => v != null)
                .ToArray();
        }

        public abstract TEntityModel From(TApiModel value);
    }
}