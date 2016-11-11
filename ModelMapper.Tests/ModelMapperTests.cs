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
            var viewModel = InMemoryRepository.GetUserViewModel();
            var expected = InMemoryRepository.GetUser();
            //act
            var actual = _mapper.MapToEntity(viewModel);
            //assert
            Assert.Equal(expected.ToJson(), actual.ToJson());
        }
        [Fact]
        public void MapToViewModel_WhenCallsForComplexModel_ShouldMapCorrectly()
        {
            //arrange
            var entity = InMemoryRepository.GetUser();
            var expected = InMemoryRepository.GetUserViewModel();
            expected.Role.Name = null;
            //act
            var actual = _mapper.MapToViewModel(entity);
            //assert
            Assert.Equal(expected.ToJson(), actual.ToJson());
        }
        [Fact]
        public void Map_WhenCallsForHashSet_ShouldMapCorrectly()
        {
            //arrange
            HashSet<string> viewModel = new HashSet<string>() { "1", "2" };
            var mapper = new ModelMapper<HashSet<string>, HashSet<string>>();
            //act
            var result = mapper.MapToEntity(viewModel);
            //assert
            Assert.Equal(viewModel.ToJson(), result.ToJson());
        }
        [Fact]
        public void Map_WhenCallsForICollectionWithCorrectBinding_ShouldMapCorrectly()
        {
            //arrange
            var expected = InMemoryRepository.GetOrder();
            var viewModel = InMemoryRepository.GetOrderVM();
            var bindingOptions = new Dictionary<Type, Type>()
            {
                { typeof(ICollection<int>), typeof(List<int>) }
            };
            var mapper = new ModelMapper<Order, OrderVM>(bindingOptions);
            //act
            var actual = mapper.MapToEntity(viewModel);
            //assert
            Assert.Equal(expected.ToJson(), actual.ToJson());
        }
        [Fact]
        public void Map_WhenCallsWithAddedBindingParameters_ShouldMapCorrectly()
        {
            //arrange
            var expected = InMemoryRepository.GetOrder();
            var viewModel = InMemoryRepository.GetOrderVM();
            var mapper = new ModelMapper<Order, OrderVM>();
            mapper.Bind<ICollection<int>, List<int>>();
            //act
            var actual = mapper.MapToEntity(viewModel);
            //assert
            Assert.Equal(expected.ToJson(), actual.ToJson());
        }
    }
}
