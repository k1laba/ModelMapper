using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelmapping.Tests
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object input)
        {
            if (input == null) return String.Empty;
            return Newtonsoft.Json.JsonConvert.SerializeObject(input);
        }
    }
}
