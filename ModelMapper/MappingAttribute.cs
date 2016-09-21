using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapper
{
    public class MappingAttribute : Attribute
    {
        private string _toPropertyName;
        public MappingAttribute(string toPropertyName)
        {
            _toPropertyName = toPropertyName;
        }
        public string GetPropertyName()
        {
            return _toPropertyName;
        }
    }
}
