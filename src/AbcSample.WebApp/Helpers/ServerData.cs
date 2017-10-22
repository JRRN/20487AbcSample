using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AbcSample.WebApp.Helpers
{
    public static class ServerData
    {
        public static readonly string ApiUrlBase = ConfigurationManager.AppSettings["ApiServer"];
    }
}