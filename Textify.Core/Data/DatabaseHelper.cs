using sORM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textify.Core.Data
{
    public class DatabaseHelper
    {
        public static void Initialize()
        {
            SimpleORM.Current.Initialize(System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        }
    }

}
