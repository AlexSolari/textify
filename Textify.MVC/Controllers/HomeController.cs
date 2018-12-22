using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textify.Core.Api;

namespace Textify.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var mail = SiteApi.Users.GetCurrentUserEmail();
            var texts = SiteApi.Texts.GetListByField("UserEmail", mail);

            return View(texts);
        }
    }
}