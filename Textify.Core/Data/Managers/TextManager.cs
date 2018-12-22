using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textify.Core.Data.Models;
using System.Web.Mvc;
using System.Web;
using Textify.Core.Dependencies;
using Textify.Core.Api;

namespace Textify.Core.Data.Managers
{
    public class TextManager : BaseManager<SiteText>
    {
        public string GenerateUserScript()
        {
            var helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var mail = SiteApi.Users.GetCurrentUserEmail();
            var existingKey = SiteApi.UserKeys.GetByField("UserEmail", mail);
            UserApiKey apiKey;

            if (existingKey == null)
            {
                apiKey = Resolver.GetInstance<UserApiKey>();
                apiKey.UserEmail = mail;
                apiKey.ApiKey = Guid.NewGuid().ToString().Replace("-", "");

                SiteApi.UserKeys.CreateOrUpdate(apiKey);
            }
            else
            {
                apiKey = existingKey;
            }

            return "<script src=\"" + helper.Action("UserScript", "Api", new { key = apiKey.ApiKey }, HttpContext.Current.Request.Url.Scheme) + "\" ></script>";
        }
    }
}
