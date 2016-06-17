using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using System.Web.Routing;
using Diplom.Web.App_Start;
using System.Web;
using Diplom.Web.Models;
using System.Web.Optimization;

[assembly: OwinStartupAttribute(typeof(Diplom.Web.Startup))]
namespace Diplom.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

        }
    }
}

