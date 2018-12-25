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
        public ActionResult Info()
        {
            var mail = SiteApi.Users.GetCurrentUserEmail();
            var existingKey = SiteApi.UserKeys.GetByField("UserEmail", mail);
            UserApiKey apiKey;

            if (existingKey == null)
            {
                apiKey = Core.Dependencies.Resolver.GetInstance<UserApiKey>();
                apiKey.UserEmail = mail;
                apiKey.ApiKey = Guid.NewGuid().ToString().Replace("-", "");

                SiteApi.UserKeys.CreateOrUpdate(apiKey);
            }
            else
            {
                apiKey = existingKey;
            }

            return View(apiKey);
        }

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

        #region Public API
        [HttpPost]
        public ActionResult GetAllTexts(string key)
        {
            var userMail = GetUserByKey(key);

            if (userMail == null)
                return Json(null);

            var userTexts = SiteApi.Texts.GetListByField("UserEmail", userMail);
            var formattedTexts = userTexts
                .Select(text => 
                new
                {
                    Key = text.TextKey,
                    Value = text.TextValue,
                    Id = text.Id
                }).ToArray();

            return Json(formattedTexts);
        }

        [HttpPost]
        public ActionResult GetTextValue(string key, string textKey)
        {
            var userMail = GetUserByKey(key);

            if (userMail == null)
                return Json(null);

            var userTexts = SiteApi.Texts.GetListByField("UserEmail", userMail);
            var requestedText = userTexts.Where(text => text.TextKey.Equals(textKey)).FirstOrDefault();

            return Json(requestedText?.TextValue
);
        }

        [HttpPost]
        public ActionResult CreateNewText(string key, string textKey, string textValue)
        {
            var userMail = GetUserByKey(key);

            if (userMail == null)
                return Json(null);

            var newText = Core.Dependencies.Resolver.GetInstance<SiteText>();
            newText.TextKey = textKey;
            newText.TextValue = textValue;
            newText.UserEmail = userMail;

            SiteApi.Texts.CreateOrUpdate(newText);

            return Json(new { Key = newText.TextKey, Value = newText.TextValue, Id = newText.Id });
        }


        [HttpPost]
        public ActionResult DeleteExistingText(string key, string id)
        {
            var userMail = GetUserByKey(key);

            if (userMail == null)
                return Json(false);

            var text = SiteApi.Texts.GetById(id);

            if (text == null)
                return Json(false);

            SiteApi.Texts.Delete(text);

            return Json(true);
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
        #endregion

        protected string GetUserByKey(string key)
        {
            var userApiKey = SiteApi.UserKeys.GetByField("ApiKey", key);

            return userApiKey?.UserEmail;
        }
    }
}