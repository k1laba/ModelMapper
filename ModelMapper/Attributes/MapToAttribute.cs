using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapping.Attributes
{
    public class MapToAttribute : Attribute
    {
        private string _propertyName;
        public MapToAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }
        public string GetPropertyName()
        {
            return _propertyName;
        }
    }
}
