using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text.RegularExpressions;
using Diplom.Interfaces;
using Diplom.Domain;

namespace Diplom.Web.Areas.SystemAdministration.Controllers
{
    public class NewsController : Controller
    {
        private IPostService Service;
        public NewsController(IPostService service)
        {
            Service = service;
        }
        // GET: SystemAdministration/News
        public ActionResult Index()
        {
            return View(Service.GetPosts());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(string title,string description, string htmlnews)
        {
            var encodeNews = HttpUtility.HtmlEncode(htmlnews);
            var pattern = "src=\"[^ ]{1,}\"";
            var newReg = new Regex(pattern);
            var matches = newReg.Matches(htmlnews);
            string news_img = "";
            if (matches.Count != 0)
            {
                news_img = matches[0].Value.Substring(5, matches[0].Value.Length - 6);
            }
            var post = new Post()
            {
                Description = description,
                Title = title,
                ImgLink = news_img,
                Text = htmlnews
            };
            Service.CreatePost(post);
            var result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}