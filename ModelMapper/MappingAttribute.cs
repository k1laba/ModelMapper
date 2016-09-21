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
        private bool _ignore;
        public MappingAttribute(bool ignore)
        {
            _ignore = ignore;
        }
        public MappingAttribute(string toPropertyName)
        {
            _toPropertyName = toPropertyName;
        }
        public MappingAttribute(string toPropertyName, bool ignore)
        {
            _toPropertyName = toPropertyName;
            _ignore = ignore;
        }
        public string GetPropertyName()
        {
            return _toPropertyName;
        }
        public bool ShouldIgnore()
        {
            return _ignore;
        }
    }
}
