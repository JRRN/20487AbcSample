using AbcSample.DAL.Storages.Table;
using Autofac;

namespace AbcSample.DAL
{
    public class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(TableStorageManager<>)).As(typeof(ITableStorageManager<>));
            builder.RegisterModule<XCutting.IoCModule>();
            base.Load(builder);
        }
    }
}