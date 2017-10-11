using System.Collections.Generic;

namespace AbcSample.XCutting.Mapper
{
    public interface IMapper<TApiModel, TEntityModel>
    {
        IEnumerable<TApiModel> From(IEnumerable<TEntityModel> values);

        TApiModel From(TEntityModel value);

        IEnumerable<TEntityModel> From(IEnumerable<TApiModel> values);

        TEntityModel From(TApiModel value);
    }
}
