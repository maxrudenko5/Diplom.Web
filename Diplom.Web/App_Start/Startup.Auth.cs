
using Diplom.DataAccess;
using Diplom.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diplom.Web
{
  public partial class Startup
  {
    //private IContextCreator contextCreator = new ContextCreator();
    public void ConfigureAuth(IAppBuilder app)
    {
      //app.CreatePerOwinContext(ApplicationContext.Create);
      //app.CreatePerOwinContext<IUserService>(UserService.Create);
      app.UseCookieAuthentication(new CookieAuthenticationOptions
      {
        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        LoginPath = new PathString("/Account/Login"),
      });
    }
  }
}