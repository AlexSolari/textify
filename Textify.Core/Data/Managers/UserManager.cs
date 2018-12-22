using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textify.Core.Data.Managers
{
    public class UserManager
    {
        public string GetCurrentUserEmail()
        {
            return System.Web.HttpContext.Current.User.Identity.Name;
        }
    }
}
