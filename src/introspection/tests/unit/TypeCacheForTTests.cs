using System;
using System.Linq;
using System.Reflection;
using Xunit;
using CosmicLexicon.Foundation.Introspection;

namespace CosmicLexicon.Foundation.Introspection
{
    public class TypeCacheForTTests
    {
        [Fact]
        public void ConstructorsReturnsConstructorsForType()
        {
            // Arrange

            // Act
            ConstructorInfo[] constructors = TypeCacheFor<TestClass>.Constructors;

            // Assert
            Assert.NotNull(constructors);
            Assert.Equal(1, constructors.Length);
        }

        [Fact]
        public void FieldsReturnsFieldsForType()
        {
            // Arrange

            // Act
            FieldInfo[] fields = TypeCacheFor<TestClass>.Fields;

            // Assert
            Assert.NotNull(fields);
            Assert.Equal(1, fields.Length);
        }

        [Fact]
        public void PropertiesReturnsPropertiesForType()
        {
            // Arrange

            // Act
            PropertyInfo[] properties = TypeCacheFor<TestClass>.Properties;

            // Assert
            Assert.NotNull(properties);
            Assert.Equal(1, properties.Length);
        }

        [Fact]
        public void InterfacesReturnsInterfacesForType()
        {
            // Arrange

            // Act
            Type[] interfaces = TypeCacheFor<TestClass>.Interfaces;

            // Assert
            Assert.NotNull(interfaces);
            Assert.Equal(0, interfaces.Length);
        }

        [Fact]
        public void MethodsReturnsMethodsForType()
        {
            // Arrange

            // Act
            MethodInfo[] methods = TypeCacheFor<TestClass>.Methods;

            // Assert
            Assert.NotNull(methods);
            Assert.True(methods.Length > 0);
        }

        [Fact]
        public void TypeReturnsCorrectType()
        {
            // Arrange

            // Act
            Type type = TypeCacheFor<TestClass>.Type;

            // Assert
            Assert.Equal(typeof(TestClass), type);
        }

        public class TestClass
        {
            public int Value;
            public string Name { get; set; }

            public TestClass(int value, string name) { }
        }
    }
}