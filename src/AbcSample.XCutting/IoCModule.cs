using AbcSample.XCutting.Mapper;
using Autofac;

namespace AbcSample.XCutting
{
    public class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(MapperBase<,>)).As(typeof(IMapper<,>));
            

            base.Load(builder);
        }
    }
}
