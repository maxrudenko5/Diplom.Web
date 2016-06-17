using Diplom.Interfaces;
using Diplom.ViewModels.SocialSpace;
using Diplom.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diplom.Web.Areas.SocialSpace.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private IProfileService Service;
        public ProfileController(IProfileService service)
        {
            Service = service;
        }
        public JsonResult GetUser()
        {
            var user = Service.GetUser(User.Identity.Name);
            return Json(new { name=user.Profile.FullName,avatar=user.Profile.Photo_200}, JsonRequestBehavior.AllowGet);
        }
        [ValidateAntiForgeryToken]
        public ActionResult Search(string searchQuery)
        {
            var view = new SearchProfileViewModel();
            Service.FillSearch(searchQuery,view);
            return View(view);
        }
        // GET: SocialSpace/Profile
        public ActionResult Index(string username)
        {
            var model = new IndexProfileViewModel();
            model.Type = PageType.Other;
            if (String.IsNullOrEmpty(username) || username==User.Identity.Name)
            {
                username = User.Identity.Name;
                model.Type = PageType.My;
            }
            model.User = Service.GetUser(username);
            if (model.User == null) return Redirect("/Home/Index");
            model.Friends = Service.GetFriends(username);
            model.Supscriptions = Service.GetSubscriptions(User.Identity.Name);
            model.Subscribers= Service.GetSubscribers(User.Identity.Name);
            if (model.Type == PageType.Other)
            {
                model.Type= model.Friends.Any(u => u.UserName == User.Identity.Name)==true ? PageType.Friend : PageType.Other;
                model.Type = model.Type == PageType.Other ? Service.CheckSubscription(User.Identity.Name, model.User.Id) == true ? PageType.Signed : PageType.Other : PageType.Friend;
            }
            model.Sex = model.User.Profile.Sex;
            model.Skype = model.User.Profile.Skype;
            model.Phone = model.User.Profile.Phone;
            model.BirthDate = model.User.Profile.BirthDate.ToString("dd/MM/yyyy");
            return View(model);
        }
        public JsonResult AddToFriends(string id)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(id))
            {
                result = Service.AddToFriends(User.Identity.Name, id);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteFromFriends(string id)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(id))
            {
                result = Service.DeleteFromFriends(User.Identity.Name, id);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult LoadPhoto(HttpPostedFileBase files)
        {
            Load(files);
            return RedirectToAction("Index");
        }
        private bool Load(HttpPostedFileBase files)
        {
            if (files == null)
            {
                return false;
            }
            var imageMax = FileConvertor.StreamToImage(files);
            if (imageMax.Width < 259 || imageMax.Height < 50)
            {
                return false;
            }
            var user = Service.GetUser(User.Identity.Name);
            var image200 = FileConvertor.ResizeOrigImg(imageMax, 259);
            if (ConfigurationManager.AppSettings["NoAvatar"] != user.Profile.Photo_max)
            {
                FileConvertor.DeleteFile(Server.MapPath("~" + user.Profile.Photo_max));
                FileConvertor.DeleteFile(Server.MapPath("~" + user.Profile.Photo_200));
            }
            user.Profile.Photo_max = "/Files/" + Guid.NewGuid().ToString() + "max.jpg";
            user.Profile.Photo_200 = "/Files/" + Guid.NewGuid().ToString() + "200.jpg";
            // сохраняем файл в папку Files в проекте
            imageMax.Save(Server.MapPath("~" + user.Profile.Photo_max));
            image200.Save(Server.MapPath("~" + user.Profile.Photo_200));
            Service.UpdatePhoto(user.Profile);
            return true;
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditProfile(IndexProfileViewModel view)
        {
            if (ModelState.IsValid)
            {
                Service.EditProfile(view, User.Identity.Name);
            }
            return RedirectToAction("Index");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditTeacherProfile(IndexProfileViewModel view)
        {
            if (ModelState.IsValid)
            {
                Service.EditTeacherProfile(view, User.Identity.Name);
            }
            return RedirectToAction("Index");
        }
    }
}