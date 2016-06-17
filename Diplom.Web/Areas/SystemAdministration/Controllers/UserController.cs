using Diplom.Interfaces;
using Diplom.ViewModels.SystemAdministration.User;
using Diplom.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.IO;
using System.Configuration;

namespace Diplom.Web.Areas.SystemAdministration.Controllers
{
    [Authorize(Roles = "SystemAdministrationAccess")]
    public class UserController : Controller
    {
        private IUserService Service;
        public UserController(IUserService service)
        {
            Service = service;
        }
        // GET: SystemAdministration/User
        public ActionResult Index()
        {
            return View(Service.GetAllUsers());
        }
        [HttpGet]
        public ActionResult Add()
        {
            var view = new AddUserViewModel();
            view.AllExitstRoles = Service.GetAllRoles();
            return View(view);
        }
        [HttpPost]
        public ActionResult Add(AddUserViewModel view)
        {
            if (!ModelState.IsValid)
            {
                view.AllExitstRoles = Service.GetAllRoles();
                return View(view);
            }
            Service.Add(view);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var view = new EditUserViewModel();
            view.Id = id;
            Service.FillEdit(view);
            return View(view);
        }

        [HttpPost]
        public ActionResult Edit(EditUserViewModel view)
        {
            if (!ModelState.IsValid || String.IsNullOrEmpty(view.SelectedRoleId))
            {
                view.AllExitstRoles = Service.GetAllRoles();
                return View(view);
            }
            Service.Edit(view);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditProfile(string id)
        {
            var view = new EditUserProfileViewModel();
            view.UserId = id;
            Service.FillProfileEdit(view);
            return View(view);
        }
        [HttpPost]
        public async Task<ActionResult> EditProfile(EditUserProfileViewModel view, HttpPostedFileBase files)
        {
            if (!ModelState.IsValid)
            {
                return View(view);
            }
            if (files != null)
            {
                var imageMax = FileConvertor.StreamToImage(files);
                if (imageMax.Width < 259 || imageMax.Height < 50)
                {
                    ModelState.AddModelError("", "Invalid image!");
                    return View(view);
                }
                await Task.Run(() =>
                {
                    var image200 = FileConvertor.ResizeOrigImg(imageMax, 259);
                    if (ConfigurationManager.AppSettings["NoAvatar"] != view.Photo_max)
                    {
                        FileConvertor.DeleteFile(Server.MapPath("~" + view.Photo_max));
                        FileConvertor.DeleteFile(Server.MapPath("~" + view.Photo_200));
                    }
                    view.Photo_max = "/Files/" + Guid.NewGuid().ToString() + "max.jpg";
                    view.Photo_200 = "/Files/" + Guid.NewGuid().ToString() + "200.jpg";
                    // сохраняем файл в папку Files в проекте
                    imageMax.Save(Server.MapPath("~" + view.Photo_max));
                    image200.Save(Server.MapPath("~" + view.Photo_200));
                    Service.EditUserProfile(view);
                });
            }

            return RedirectToAction("Index");
        }

        public JsonResult ChangeActive(string id)
        {
            var result = Service.ChangeActive(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}