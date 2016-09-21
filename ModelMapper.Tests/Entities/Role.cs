﻿using ModelMapper.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapper.Tests.Models
{
    class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Mapping("Perms")]
        public List<Permission> Permissions { get; set; }
    }
}