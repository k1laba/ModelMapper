﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapper.Tests.Models
{
    class User : SimpleUser, IMappable
    {
        public Role Role { get; set; }
    }
}
