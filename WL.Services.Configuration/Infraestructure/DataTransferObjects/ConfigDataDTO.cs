using Infraestructure.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.DataTransferObjects
{
    public class ConfigDataDTO
    {
        [DataFieldMapping("key")]
        public string Key { get; set; }
        [DataFieldMapping("value")]
        public string Value { get; set; }
        [DataFieldMapping("SystemCode")]
        public string SystemCode { get; set; }
    }
}
