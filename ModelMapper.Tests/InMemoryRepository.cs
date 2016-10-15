using ModelMapper.Tests.Entities;
using ModelMapper.Tests.Models;
using ModelMapper.Tests.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapper.Tests
{
    class InMemoryRepository
    {
        public static User GetUser()
        {
            var user = new User()
            {
                Id = 1,
                Name = "beqa",
                Date = new DateTime(1970, 1, 1),
                Role = new Role()
                {
                    Id = 2
                },
                ProductIds = new List<int>() { 1 },
                Citites = new List<string>() { "Tbilisi" }
            };
            user.Role.Permissions = new List<Permission>()
            {
                new Permission() { Id = 1, Name = "Permission 1" },
                new Permission() { Id = 2, Name = "Permission 2" },
            };
            return user;
        }
        public static UserVM GetUserViewModel()
        {
            var user = new UserVM()
            {
                Id = 1,
                Name = "beqa",
                Date = new DateTime(1970, 1, 1),
                Role = new RoleVM() { Id = 2, Name = "role 1" }
            };
            user.Role = new RoleVM()
            {
                Id = 2,
                Name = "role 1"
            };
            user.Role.Perms = new List<PermissionVM>()
            {
                new PermissionVM() { Id = 1, Name = "Permission 1" },
                new PermissionVM() { Id = 2, Name = "Permission 2" },
            };
            user.ProductIds = new List<int>() { 1 };
            user.Citites = new List<string>() { "Tbilisi" };
            return user;
        }
        public static OrderVM GetOrderVM()
        {
            return new OrderVM()
            {
                OrderItemsIds = new List<int>() { 1, 2, 3 }
            };
        }
        public static Order GetOrder()
        {
            return new Order()
            {
                OrderItemsIds = new List<int>() { 1, 2, 3 }
            };
        }
    }
}
