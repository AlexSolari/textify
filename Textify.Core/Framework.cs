using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textify.Core.Cache;
using Textify.Core.Data;
using Textify.Core.Data.Models;
using Textify.Core.Dependencies;

namespace Textify.Core
{
    public static class Framework
    {
        public static void Initialize()
        {
            Resolver.RegisterType<SiteText, SiteText>();
            Resolver.RegisterType<UserApiKey, UserApiKey>();
            Resolver.RegisterInstance<SiteCache>(new SiteCache());

            DatabaseHelper.Initialize();
        }
    }
}
