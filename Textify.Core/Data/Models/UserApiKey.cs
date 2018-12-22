using sORM.Core.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textify.Core.Data.Models
{
    [DataModel]
    public class UserApiKey : Entity
    {
        [MapAuto]
        public string UserEmail { get; set; }

        [MapAuto]
        public string ApiKey { get; set; }
    }
}
