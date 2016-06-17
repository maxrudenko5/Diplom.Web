
using Diplom.DataAccess.Repositories;
using Diplom.Interfaces;
using Diplom.ViewModels.Main.HomeViewModels;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diplom.Web.Controllers
{
    public class HomeController : Controller
    {
        private IHomeService Service;
        public HomeController(IHomeService service)
        {
            Service = service;
        }
        // GET: Home
        public ActionResult Index()
        {
            var view = new HomeIndexViewModel();
            Service.FillIndex(view);
            return View(view);
        }
        [Authorize(Roles ="ModeratorAccess")]
        public JsonResult DeletePost(int? id)
        {
            var result = false;
            result = Service.DeletePost(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult News()
        {
            return View(Service.GetPosts());
        }
        public ActionResult Post(int? id)
        {
            var model = Service.GetPost(id);
            if (model == null)
            {
                return RedirectToAction("News");
            }
            return View(model);
        }
    }
}