using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Diplom.Web.Filters
{
  public class LocalizationAttribute : ActionFilterAttribute
  {
    private string _DefaultLanguage = "ru";
    public LocalizationAttribute(string defaultLanguage)
    {
      _DefaultLanguage = defaultLanguage;
    }
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      string cultureName = null;
      HttpCookie cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
      if (cultureCookie != null)
      {
        cultureName = cultureCookie.Value;
      }
      else
      {
        cultureName = "en";
      }
      List<string> culture = new List<string>() { "en", "ru" };
      if (!culture.Contains(cultureName))
      {
        cultureName = "en";
      }
      Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
      Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
    }
  }
}