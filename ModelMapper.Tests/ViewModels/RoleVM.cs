using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapper.Tests.Models
{
    class RoleVM : IMappable
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
