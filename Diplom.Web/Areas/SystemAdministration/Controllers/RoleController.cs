using Diplom.Interfaces;
using Diplom.ViewModels.SystemAdministration.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diplom.Web.Areas.SystemAdministration.Controllers
{
    [Authorize(Roles = "SystemAdministrationAccess")]
    public class RoleController : Controller
    {
        private IRoleService Service;
        public RoleController(IRoleService service)
        {
            Service = service;
        }
        // GET: SystemAdministration/Role
        public ActionResult Index()
        {
            return View(Service.GetAllRoles());
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var view = new EditRoleViewModel();
            view.Id = id;
            Service.FillEdit(view);
            return View(view);
        }

        [HttpPost]
        public ActionResult Edit(EditRoleViewModel view)
        {
            if (!ModelState.IsValid || String.IsNullOrEmpty(view.Id))
            {
                return View(view);
            }
            Service.Edit(view);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Add()
        {
            var view = new AddRoleViewModel();
            return View(view);
        }
        [HttpPost]
        public ActionResult Add(AddRoleViewModel view)
        {
            if (!ModelState.IsValid)
            {
                return View(view);
            }
            Service.Add(view);
            return RedirectToAction("Index");
        }
        public JsonResult ChangeActive(string id)
        {
            var result = Service.ChangeActive(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}