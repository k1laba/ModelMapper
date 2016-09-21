using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapper.Tests.Models
{
    class UserVM : SimpleUserVM
    {
        public RoleVM Role { get; set; }
    }
}
