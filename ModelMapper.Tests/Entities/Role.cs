using Modelmapping.Tests.Entities;
using ModelMapping;
using ModelMapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelmapping.Tests.Models
{
    class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [MapTo("Perms")]
        public List<Permission> Permissions { get; set; }
    }
}
