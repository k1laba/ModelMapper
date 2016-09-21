using ModelMapper.Tests.Entities;
using ModelMapper.Tests.Models;
using ModelMapper.Tests.ViewModels;
using ModelMapping;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace ModelMapper.Tests
{
    public class ModelMapperTests
    {
        private IModelMapper<User, UserVM> _mapper;
        public ModelMapperTests()
        {
            _mapper = new ModelMapper<User, UserVM>();
        }
        [Fact]
        public void MapToEntity_WhenCallsForComplexModel_ShouldMapCorrectly()
        {
            //arrange
            var viewModel = GetViewModel();
            var expected = GetEntity();
            //act
            var actual = _mapper.MapToEntity(viewModel);
            //assert
            Assert.Equal(expected.ToJson(), actual.ToJson());
        }
        [Fact]
        public void MapToViewModel_WhenCallsForComplexModel_ShouldMapCorrectly()
        {
            //arrange
            var entity = GetEntity();
            var expected = GetViewModel();
            //act
            var actual = _mapper.MapToViewModel(entity);
            //assert
            Assert.Equal(expected.ToJson(), actual.ToJson());
        }

        private User GetEntity()
        {
            var user = new User()
            {
                Id = 1,
                Name = "beqa",
                Date = new DateTime(1970, 1, 1),
                Role = new Role() { Id = 2, Name = "role 1" }
            };
            user.Role.Permissions = new List<Permission>()
            {
                new Permission() { Id = 1, Name = "Permission 1" },
                new Permission() { Id = 2, Name = "Permission 2" },
            };
            return user;
        }
        private UserVM GetViewModel()
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
            return user;
        }
    }
}
