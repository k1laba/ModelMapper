using Modelmapping.Tests.Entities;
using Modelmapping.Tests.Models;
using Modelmapping.Tests.ViewModels;
using ModelMapping;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace Modelmapping.Tests
{
    public class ModelMapperTests
    {
        private IModelMapper _mapper;
        public ModelMapperTests()
        {
            _mapper = new ModelMapper();
        }
        [Fact]
        public void Map_WhenCallsForComplexModel_ShouldMapCorrectly()
        {
            //arrange
            var source = InMemoryRepository.GetUserViewModel();
            var expected = InMemoryRepository.GetUser();
            //act
            var actual = _mapper.Map<UserVM, User>(source);
            //assert
            Assert.Equal(expected.ToJson(), actual.ToJson());
        }
        [Fact]
        public void Map_WhenCallsForHashSet_ShouldMapCorrectly()
        {
            //arrange
            HashSet<string> source = new HashSet<string>() { "1", "2" };
            //act
            var result = _mapper.Map<HashSet<string>, HashSet<string>>(source);
            //assert
            Assert.Equal(source.ToJson(), result.ToJson());
        }
        [Fact]
        public void Map_WhenCallsForICollectionWithCorrectBinding_ShouldMapCorrectly()
        {
            //arrange
            var expected = InMemoryRepository.GetOrder();
            var source = InMemoryRepository.GetOrderVM();
            var bindingOptions = new Dictionary<Type, Type>()
            {
                { typeof(ICollection<int>), typeof(List<int>) }
            };
            var mapper = new ModelMapper(bindingOptions);
            //act
            var actual = mapper.Map<OrderVM, Order>(source);
            //assert
            Assert.Equal(expected.ToJson(), actual.ToJson());
        }
        [Fact]
        public void Map_WhenCallsWithAddedBindingParameters_ShouldMapCorrectly()
        {
            //arrange
            var expected = InMemoryRepository.GetOrder();
            var source = InMemoryRepository.GetOrderVM();
            _mapper.Bind<ICollection<int>, List<int>>();
            //act
            var actual = _mapper.Map<OrderVM, Order>(source);
            //assert
            Assert.Equal(expected.ToJson(), actual.ToJson());
        }
        [Fact]
        public void Map_WhenCallsWithSingleGenericArgument_ShouldMapCorreclty()
        {
            //arrange
            var source = InMemoryRepository.GetUserViewModel();
            var expected = InMemoryRepository.GetUser();
            //act
            var actual = _mapper.Map<User>(source);
            //assert
            Assert.Equal(expected.ToJson(), actual.ToJson());
        }
    }
}
