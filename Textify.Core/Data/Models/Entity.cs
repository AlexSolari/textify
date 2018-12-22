using sORM.Core.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textify.Core.Data.Models
{
    public class Entity
    {
        [MapAsType(DataType.Guid)]
        [Key]
        public Guid Id { get; set; }
    }
}
