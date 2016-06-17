using AutoMapper;
using Diplom.BusinessLogic;
using Diplom.Domain;
using Diplom.Interfaces;
using Diplom.Services;
using Diplom.ViewModels.Main.AccountViewModels;
using Diplom.Web.Models;
using Diplom.Web.Utilities.ApiVK;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace Diplom.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationService Service;
        public AccountController(IAuthenticationService service)
        {
            Service = service;
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult LoginIndirect(LoginType type)
        {
            if (type == LoginType.VK)
            {
                string redirect_url = Url.Action("RecieveCode", "Account", null, "http");
                return Redirect(String.Format("http://api.vkontakte.ru/oauth/authorize?response_type=code&redirect_uri={0}&client_id={1}&scope=email,friends,offline,photos&display=page",
                    redirect_url, WebConfigurationManager.AppSettings["app_id"]));

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel view)
        {
            if (!ModelState.IsValid)
            {
                return View(view);
            }
            Service.SignOut();
            var cookie = Service.SignIn(view);
            if (cookie == null)
            {
                ModelState.AddModelError("LoginSummary", "Неверный логин или пароль.");
                return View(view);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public ActionResult RecieveCode(string code)
        {
            var token = this.Url.GetToken(code);
            if (token == null)
            {
                return Redirect("/Home/Index");
            }
            var userInfoJson = VkHelpers.GetRequest(String.Format(@"https://api.vk.com/method/users.get?user_id={0}&v=5.44&access_token={1}&fields={2}", token.user_id.ToString(), token.access_token, "photo_id,has_photo,photo_400_orig,contacts,photo_200_orig,status,nickname,relation,sex,bdate,photo_max"));
            if (String.IsNullOrEmpty(token.email))
            {
                return Redirect("/Home/Index");
            }
            userInfoJson = userInfoJson.NormalizeResponse();
            var userInfo = JsonConvert.DeserializeObject<UserInfo>(userInfoJson);
            var profile = new Domain.Profile()
            {
                FullName = userInfo.first_name + " " + userInfo.last_name,
                Photo_200 = String.IsNullOrEmpty(userInfo.photo_400_orig) == true ? WebConfigurationManager.AppSettings["NoAvatar"] : userInfo.photo_400_orig,
                Photo_max = String.IsNullOrEmpty(userInfo.photo_max) == true ? WebConfigurationManager.AppSettings["NoAvatar"] : userInfo.photo_max,
                Sex = (Sex)Convert.ToInt32(userInfo.sex),
                BirthDate = String.IsNullOrEmpty(userInfo.bdate) == true ? DateTime.Now : DateTime.Parse(userInfo.bdate, CultureInfo.CreateSpecificCulture("de-DE"))
            };
            var user = new User()
            {
                Email = token.email,
                UserName = "vk" + userInfo.id,
                PasswordHash = userInfo.id,
                Profile = profile,
                LoginType = LoginType.VK,
                isActive = true
            };
            Service.SignOut();
            var cookie = Service.SignUpIndirect(user,profile,LoginType.VK);
            if (cookie != null)
            {
                Response.Cookies.Add(cookie);
            }
            return Redirect("/Home/Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            Service.SignOut();
            return Redirect("/Home/Index");
        }



        [HttpPost]
        public ActionResult Register(RegisterView view)
        {
            if (!ModelState.IsValid)
            {
                return View(view);
            }
            Service.SignOut();
            var cookie = Service.SignUp(view);
            if (cookie == null)
            {
                ModelState.AddModelError("UserName", Resources.Resources.RegisterFormError);
                return View(view);
            }
            Response.Cookies.Add(cookie);
            return Redirect("/Home/Index");
        }


    }
}