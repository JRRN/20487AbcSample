using Autofac;

namespace AbcSample.Service
{
    public class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            builder.RegisterModule<DAL.IoCModule>();
            builder.RegisterModule<XCutting.IoCModule>();
            base.Load(builder);
        }
    }
}