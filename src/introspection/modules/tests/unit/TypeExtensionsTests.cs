namespace CosmicLexicon.Foundation.Introspection.Modules.UnitTest
{
    public class TypeExtensionsTests
    {
        [Fact]
        public void IsNullableValueTypeReturnsFalse()
        {
            // Arrange
            Type type = typeof(int);

            // Act
            bool result = type.IsNullable();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsNullableReferenceTypeReturnsTrue()
        {
            // Arrange
            Type type = typeof(string);

            // Act
            bool result = type.IsNullable();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetAggregatedConstructorParamTypesHasConstructorsReturnsParameterTypes()
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
        public void ShouldGenericEqualsNoGenericParametersReturnsTrue()
        {
            // Arrange
            Type type = typeof(string);

            // Act
            bool result = type.ShouldGenericEquals();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ShouldGenericEqualsHasGenericParametersReturnsFalse()
        {
            // Arrange
            Type type = typeof(List<string>);

            // Act
            bool result = type.ShouldGenericEquals();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GuidEqualEqualGuidsReturnsTrue()
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
        public void HasInterfaceImplementsInterfaceReturnsTrue()
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
