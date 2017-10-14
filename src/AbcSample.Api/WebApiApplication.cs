using System.Web.Http;

namespace AbcSample.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(InversionOfControlRegister.Register);

            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
