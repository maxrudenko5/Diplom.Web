using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
namespace Diplom.Web.Controllers
{
  public class LocalizationController : Controller
  {
    public ActionResult Change(string lang)
    {
      string returnUrl = Request.UrlReferrer.AbsolutePath;
      List<string> culture = new List<string>() { "en", "ru" };
      if (!culture.Contains(lang))
      {
        lang = "en";
      }
      HttpCookie cookie = Request.Cookies["lang"];
      if (cookie != null)
      {
        cookie.Value = lang;
      }
      else
      {
        cookie = new HttpCookie("lang");
        cookie.HttpOnly = false;
        cookie.Value = lang;
        cookie.Expires = DateTime.Now.AddYears(1);
      }

      Response.Cookies.Add(cookie);
      return Redirect(returnUrl);
    }

    [OutputCache(Location = OutputCacheLocation.None)]
    public JsonResult LocalizedDataTableLanguage()
    {
      var jsonObject = new
      {
        
        sEmptyTable = Resources.Resources.DataTablesEmptyTable,
        sInfo = Resources.Resources.DataTablesInfo,
        sInfoEmpty = Resources.Resources.DataTablesInfoEmpty,
        sInfoFiltered = Resources.Resources.DataTablesInfoFiltered,
        sInfoPostFix = Resources.Resources.DataTablesInfoPostFix,
        sInfoThousands = Resources.Resources.DataTablesInfoThousands,
        sLengthMenu = Resources.Resources.DataTablesLengthMenu,
        sLoadingRecords = Resources.Resources.DataTablesLoadingRecords,
        sProcessing = Resources.Resources.DataTablesProcessing,
        sSearch = Resources.Resources.DataTablesSearch,
        sZeroRecords = Resources.Resources.DataTablesZeroRecords,
        oPaginate = new
        {
          sFirst = Resources.Resources.DataTablesFirst,
          sLast = Resources.Resources.DataTablesLast,
          sNext = Resources.Resources.DataTablesNext,
          sPrevious = Resources.Resources.DataTablesPrevious,
        },
        oAria = new
        {
          sSortAscending = Resources.Resources.DataTablesSortAscending,
          sSortDescending = Resources.Resources.DataTablesSortDescending,
        },
      };
      return Json(jsonObject, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public JsonResult LocalizedJsFileText(string[] parameters)
    {
      var dictionary = new Dictionary<string, string>();

      ResourceSet resourceSet = Resources.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

      var resourceDictionary = resourceSet.Cast<DictionaryEntry>()
                              .ToDictionary(r => r.Key.ToString(),
                                            r => r.Value.ToString());
      foreach (var parameter in parameters)
      {
        if (resourceDictionary.Keys.Contains(parameter))
        {
          dictionary.Add(parameter, resourceDictionary[parameter]);
        }
        else
        {
          dictionary.Add(parameter, String.Empty);
        }
      }
      return Json(dictionary, JsonRequestBehavior.AllowGet);
    }
  }
}