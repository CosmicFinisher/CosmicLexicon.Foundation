using System;
using System.Linq;
using System.Reflection;
using Xunit;
using CosmicLexicon.Foundation.xReflection;
using Xunit.Sdk;

namespace CosmicLexicon.Foundation.xReflection
{
    internal class TestClassWithDefaultConstructor
    {
        public TestClassWithDefaultConstructor()
        {
        }
    }

    public class ConstructorListTests
    {
        // Test class with various constructors for dynamic testing
        internal class DynamicTestClass
        {
            public DynamicTestClass() { }
            public DynamicTestClass(int value) { }
            public DynamicTestClass(string name, int value) { }
            private DynamicTestClass(bool hidden) { } // Private constructor
        }

        // Test class with no public constructors
        internal class NoPublicConstructorTestClass
        {
            private NoPublicConstructorTestClass() { }
        }

        [Fact]
        public void ConstructorList_ValidTypeAndHashCode_CreatesInstance()
        {
            // Arrange
            Type type = typeof(TestClass);
            int hashCode = type.GetHashCode();

            // Act
            ConstructorList constructorList = new ConstructorList(type, hashCode);

            // Assert
            Assert.NotNull(constructorList);
        }

        [Fact]
        public void ConstructorList_NullType_ThrowsArgumentNullException()
        {
            // Arrange
            Type? type = null;
            int hashCode = 0;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ConstructorList(type, hashCode));
        }

        [Fact]
        public void CreateInstance_ValidArguments_CreatesInstance()
        {
            // Arrange
            Type type = typeof(TestClass);
            int hashCode = type.GetHashCode();
            ConstructorList constructorList = new ConstructorList(type, hashCode);
            object[] args = new object[] { 1, "test" };

            // Act
            object instance = constructorList.CreateInstance(args);

            // Assert
            Assert.NotNull(instance);
            Assert.IsType<TestClass>(instance);
        }

        [Fact]
        public void CreateInstance_NoArguments_CreatesInstanceWithDefaultConstructor()
        {
            // Arrange
            Type type = typeof(TestClassWithDefaultConstructor);
            int hashCode = type.GetHashCode();
            ConstructorList constructorList = new ConstructorList(type, hashCode);

            // Act
            object instance = constructorList.CreateInstance();

            // Assert
            Assert.NotNull(instance);
            Assert.IsType<TestClassWithDefaultConstructor>(instance);
        }

        [Fact]
        public void CreateInstance_NoMatchingConstructor_ReturnsNull()
        {
            // Arrange
            Type type = typeof(TestClass);
            int hashCode = type.GetHashCode();
            ConstructorList constructorList = new ConstructorList(type, hashCode);
            object[] args = new object[] { "test", 1 };

            // Act
            object instance = constructorList.CreateInstance(args);

            // Assert
            Assert.Null(instance);
        }

        [Fact]
        public void CreateInstanceWithNoArgumentsShouldUseDefaultConstructor()
        {
            // Arrange
            Type type = typeof(TestClassWithDefaultConstructor);
            int hashCode = type.GetHashCode();
            ConstructorList constructorList = new ConstructorList(type, hashCode);

            // Act
            object? instance = constructorList.CreateInstance();

            // Assert
            Assert.NotNull(instance);
            Assert.IsType<TestClassWithDefaultConstructor>(instance);
        }

        [Fact]
        public void GetConstructors_ReturnsCorrectCount_ForDynamicTestClass()
        {
            // AI Verifiable Outcome: Correctly identifies the number of public constructors.
            // London School TDD: Focuses on the observable behavior of GetConstructors.
            var constructors = typeof(DynamicTestClass).GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            Assert.Equal(3, constructors.Length); // Should only count public constructors
        }

        [Fact]
        public void GetConstructors_ReturnsEmptyList_ForNoPublicConstructorTestClass()
        {
            // AI Verifiable Outcome: Correctly handles classes with no public constructors.
            // London School TDD: Tests an edge case for the unit under test.
            var constructors = typeof(NoPublicConstructorTestClass).GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            Assert.Empty(constructors);
        }

        [Fact]
        public void GetConstructors_ContainsExpectedParameters_ForDynamicTestClass()
        {
            // AI Verifiable Outcome: Verifies the parameter counts of identified constructors.
            // London School TDD: Checks specific interactions and properties of the returned constructors.
            var constructors = typeof(DynamicTestClass).GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            Assert.Contains(constructors, c => c.GetParameters().Length == 0);
            Assert.Contains(constructors, c => c.GetParameters().Length == 1);
            Assert.Contains(constructors, c => c.GetParameters().Length == 2);
        }

        // Define TestClass here for ConstructorListTests to avoid dependencies on other test files
        internal class TestClass
        {
            public int IntValue { get; }
            public string StringValue { get; }

            public TestClass(int intValue, string stringValue)
            {
                IntValue = intValue;
                StringValue = stringValue;
            }

            public TestClass() { } // Required for default constructor tests
        }
    }
}
