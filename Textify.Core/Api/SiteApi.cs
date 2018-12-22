using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textify.Core.Cache;
using Textify.Core.Data.Managers;
using Textify.Core.Dependencies;

namespace Textify.Core.Api
{
    public class SiteApi
    {
        public static SiteCache Cache
        {
            get
            {
                return Resolver.GetInstance<SiteCache>();
            }
        }

        public static TextManager Texts
        {
            get
            {
                return Resolver.GetInstance<TextManager>();
            }
        }

        public static UserKeyManager UserKeys
        {
            get
            {
                return Resolver.GetInstance<UserKeyManager>();
            }
        }

        public static UserManager Users
        {
            get
            {
                return Resolver.GetInstance<UserManager>();
            }
        }
    }
}
