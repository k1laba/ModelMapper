using Modelmapping.Tests.ViewModels;
using ModelMapping;
using ModelMapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelmapping.Tests.Models
{
    class RoleVM
    {
        public int Id { get; set; }
        [MapIgnoreAttribute]
        public string Name { get; set; }
        [MapTo("Permissions")]
        public List<PermissionVM> Perms { get; set; }
    }
}
