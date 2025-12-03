using System;
using System.Linq;
using System.Reflection;
using Xunit;
using CosmicLexicon.Foundation.xReflection;
using Xunit.Sdk;

namespace CosmicLexicon.Foundation.xReflection
{
    public class ReflectionExtensionsTests
    {
        [Fact]
        public void Attributes_HasAttribute_ReturnsAttribute()
        {
            // Arrange
            MemberInfo memberInfo = typeof(TestClass).GetProperty("TestProperty");

            // Act
            TestAttribute[] attributes = memberInfo.Attributes<TestAttribute>();

            // Assert
            Assert.Single(attributes);
            Assert.IsType<TestAttribute>(attributes[0]);
        }

        [Fact]
        public void Attributes_NoAttribute_ReturnsEmptyArray()
        {
            // Arrange
            MemberInfo memberInfo = typeof(TestClass).GetProperty("NonAttributedProperty");

            // Act
            TestAttribute[] attributes = memberInfo.Attributes<TestAttribute>();

            // Assert
            Assert.Empty(attributes);
        }

        [Fact]
        public void Call_ValidMethod_ReturnsResult()
        {
            // Arrange
            TestClass testObject = new TestClass();
            string methodName = "TestMethod";
            object[] inputVariables = new object[] { 5 };

            // Act
            int result = testObject.Call<int>(methodName, inputVariables);

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void Call_NullObject_ThrowsArgumentNullException()
        {
            // Arrange
            object testObject = null;
            string methodName = "TestMethod";
            object[] inputVariables = new object[] { 5 };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => testObject.Call<int>(methodName, inputVariables));
        }
        public class TestAttribute : Attribute { }

        public class TestClass
        {
            [TestAttribute]
            public string TestProperty { get; set; } = "Test";

            public int TestMethod(int value)
            {
                return value;
            }
        }
    }
}
