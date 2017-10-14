using System.Reflection;
using System.Web.Http;
using AbcSample.Api.Mappers;
using AbcSample.Api.Models;
using AbcSample.Entities;
using AbcSample.XCutting.Mapper;
using Autofac;
using Autofac.Integration.WebApi;

namespace AbcSample.Api
{
    public static class InversionOfControlRegister
    {
        public static void Register(HttpConfiguration config)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<CountryMapper>().As<IMapper<CountryResponse, Country>>();

            builder.RegisterModule<Service.IoCModule>();
            builder.RegisterModule<XCutting.IoCModule>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}

