﻿using ModelMapper.Tests.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapper.Tests.Models
{
    class RoleVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Mapping("Permissions")]
        public List<PermissionVM> Perms { get; set; }
    }
}
