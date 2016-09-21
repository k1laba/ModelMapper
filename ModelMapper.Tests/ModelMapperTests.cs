using ModelMapper.Tests.Models;
using ModelMapping;
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
            var viewModel = new UserVM()
            {
                Id = 1,
                Name = "beqa",
                Date = new DateTime(1970, 1, 1),
                Role = new RoleVM() { Id = 2, Name = "role 1" }
            };
            var expected = new User()
            {
                Id = 1,
                Name = "beqa",
                Date = new DateTime(1970, 1, 1),
                Role = new Role() { Id = 2, Name = "role 1" }
            };
            //act
            var actual = _mapper.MapToEntity(viewModel);
            //assert
            Assert.Equal(expected.ToJson(), actual.ToJson());
        }
        [Fact]
        public void MapToViewModel_WhenCallsForComplexModel_ShouldMapCorrectly()
        {
            //arrange
            var entity = new User()
            {
                Id = 1,
                Name = "beqa",
                Date = new DateTime(1970, 1, 1),
                Role = new Role() { Id = 2, Name = "role 1" }
            };
            var expected = new UserVM()
            {
                Id = 1,
                Name = "beqa",
                Date = new DateTime(1970, 1, 1),
                Role = new RoleVM() { Id = 2, Name = "role 1" }
            };
            //act
            var actual = _mapper.MapToViewModel(entity);
            //assert
            Assert.Equal(expected.ToJson(), actual.ToJson());
        }
    }
}
