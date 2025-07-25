using Xunit;
using OpenEchoSystem.Core.xReflection.xAssembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using OpenEchoSystem.Core.xReflection.xAssembly;

namespace OpenEchoSystem.Core.xReflection.xAssembly
{
    public class TypeExtensionsTests
    {
        [Fact]
        public void IsNullable_ValueType_ReturnsFalse()
        {
            // Arrange
            Type type = typeof(int);

            // Act
            bool result = type.IsNullable();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsNullable_ReferenceType_ReturnsTrue()
        {
            // Arrange
            Type type = typeof(string);

            // Act
            bool result = type.IsNullable();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetAggregatedConstructorParamTypes_HasConstructors_ReturnsParameterTypes()
        {
            // Arrange
            Type type = typeof(TestClass);

            // Act
            Type[] result = type.GetAggregatedConstructorParamTypes();

            // Assert
            Assert.Equal(2, result.Length);
            Assert.Contains(typeof(int), result);
            Assert.Contains(typeof(string), result);
        }

        [Fact]
        public void ShouldGenericEquals_NoGenericParameters_ReturnsTrue()
        {
            // Arrange
            Type type = typeof(string);

            // Act
            bool result = type.ShouldGenericEquals();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ShouldGenericEquals_HasGenericParameters_ReturnsFalse()
        {
            // Arrange
            Type type = typeof(List<string>);

            // Act
            bool result = type.ShouldGenericEquals();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GuidEqual_EqualGuids_ReturnsTrue()
        {
            // Arrange
            Type type1 = typeof(string);
            Type type2 = typeof(string);

            // Act
            bool result = type1.GuidEqual(type2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void HasInterface_ImplementsInterface_ReturnsTrue()
        {
            // Arrange
            Type type = typeof(TestClassImplementingInterface);
            Type interfaceType = typeof(ITestInterface);

            // Act
            bool result = type.HasInterface<ITestInterface>(InterfaceFilter.None, false);

            // Assert
            Assert.True(result);
        }

        public interface ITestInterface { }
        public class TestClassImplementingInterface : ITestInterface { }
        public class TestClass
        {
            public TestClass(int value, string name) { }
        }
    }
}
