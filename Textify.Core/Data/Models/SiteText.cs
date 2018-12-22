using sORM.Core.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textify.Core.Data.Models
{
    [DataModel]
    public class SiteText : Entity
    {
        [MapAuto]
        public string UserEmail { get; set; }

        [MapAuto]
        public string TextKey { get; set; }

        [MapAuto]
        public string TextValue { get; set; }
    }
}
