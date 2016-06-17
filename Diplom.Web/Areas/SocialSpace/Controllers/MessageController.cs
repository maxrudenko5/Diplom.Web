using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diplom.Web.Areas.SocialSpace.Controllers
{
    public class MessageController : Controller
    {
        // GET: SocialSpace/Message
        public ActionResult Index()
        {
            return View();
        }
    }
}