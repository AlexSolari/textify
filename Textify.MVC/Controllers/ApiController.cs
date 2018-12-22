using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textify.Core;
using Textify.Core.Api;
using Textify.Core.Data.Models;
using Textify.MVC.Models;

namespace Textify.MVC.Controllers
{
    public class ApiController : Controller
    {
        [HttpPost]
        public ActionResult TextCreate(string TextKey, string TextValue)
        {
            var newText = Core.Dependencies.Resolver.GetInstance<SiteText>();
            newText.TextKey = TextKey;
            newText.TextValue = TextValue;
            newText.UserEmail = SiteApi.Users.GetCurrentUserEmail();

            SiteApi.Texts.CreateOrUpdate(newText);

            return PartialView("_TextCard", new SiteTextViewModel() { Text = newText, Collapsed = false });
        }

        [HttpPost]
        public ActionResult TextUpdate(SiteText model)
        {
            SiteApi.Texts.CreateOrUpdate(model);

            return PartialView("_TextCard", new SiteTextViewModel() { Text = model, Collapsed = false });
        }

        [HttpPost]
        public ActionResult TextDelete(string textId)
        {
            var textToDelete = SiteApi.Texts.GetById(textId);

            if (textToDelete != null)
            {
                SiteApi.Texts.Delete(textToDelete);

                return Json(true);
            }

            return Json(false);
        }

        [HttpGet]
        public ActionResult UserScript(string key)
        {
            var userApiKey = SiteApi.UserKeys.GetByField("ApiKey", key);

            if (userApiKey == null)
                return new EmptyResult();

            var texts = SiteApi.Texts.GetListByField("UserEmail", userApiKey.UserEmail);

            return PartialView("_UserScript", texts);
        }
    }
}