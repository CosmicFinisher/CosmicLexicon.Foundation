using Xunit;
using OpenEchoSystem.Core.xReflection;
using System;
using System.Linq;
using System.Reflection;

namespace OpenEchoSystem.Core.xReflection
{
    public class ConstructorTests
    {
        public class TestClassWithMultipleConstructors
        {
            public TestClassWithMultipleConstructors() { }
            public TestClassWithMultipleConstructors(int value) { }
            public TestClassWithMultipleConstructors(string name, int value) { }
            private TestClassWithMultipleConstructors(bool privateFlag) { } // Private constructor
        }

        public class TestClassWithNoPublicConstructors
        {
            private TestClassWithNoPublicConstructors(int value) { }
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[] { typeof(TestClassWithMultipleConstructors).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(c => c.GetParameters().Length == 0), Array.Empty<ParameterInfo>() };
            yield return new object[] { typeof(TestClassWithMultipleConstructors).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(c => c.GetParameters().Length == 1), typeof(TestClassWithMultipleConstructors).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(c => c.GetParameters().Length == 1).GetParameters() };
            yield return new object[] { typeof(TestClassWithMultipleConstructors).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(c => c.GetParameters().Length == 2), typeof(TestClassWithMultipleConstructors).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(c => c.GetParameters().Length == 2).GetParameters() };
        }

        public static IEnumerable<object[]> CreateInstanceTestData()
        {
            yield return new object[] { typeof(TestClassWithMultipleConstructors).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(c => c.GetParameters().Length == 0), Array.Empty<object>() };
            yield return new object[] { typeof(TestClassWithMultipleConstructors).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(c => c.GetParameters().Length == 1), new object[] { 123 } };
            yield return new object[] { typeof(TestClassWithMultipleConstructors).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(c => c.GetParameters().Length == 2), new object[] { "test", 456 } };
        }

        public static IEnumerable<object[]> IsMatchTestData()
        {
            yield return new object[] { typeof(TestClassWithMultipleConstructors).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(c => c.GetParameters().Length == 0), Array.Empty<object>(), true };
            yield return new object[] { typeof(TestClassWithMultipleConstructors).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(c => c.GetParameters().Length == 1), new object[] { 123 }, true };
            yield return new object[] { typeof(TestClassWithMultipleConstructors).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(c => c.GetParameters().Length == 2), new object[] { "test", 456 }, true };
            yield return new object[] { typeof(TestClassWithMultipleConstructors).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(c => c.GetParameters().Length == 1), new object[] { "wrong type" }, false }; // Type mismatch
            yield return new object[] { typeof(TestClassWithMultipleConstructors).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(c => c.GetParameters().Length == 1), new object[] { 123, "extra" }, false }; // Argument count mismatch
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void Constructor_WithConstructorInfoAndParameters_CreatesInstance(ConstructorInfo constructorInfo, ParameterInfo[] parameters)
        {
            // AI Verifiable Outcome: Verifies successful instantiation of Constructor with valid metadata.
            // London School TDD: Focuses on the core constructor logic, using parameterized data for dynamic testing.
            Constructor constructor = new Constructor(constructorInfo, parameters);

            Assert.NotNull(constructor);
            Assert.Equal(parameters.Length, constructor.ParameterLength);
        }

        [Fact]
        public void Constructor_WithNullConstructorInfo_ThrowsArgumentNullException()
        {
            // AI Verifiable Outcome: Confirms ArgumentNullException for null ConstructorInfo.
            // London School TDD: Tests error handling and input validation.
            ConstructorInfo? constructorInfo = null;
            ParameterInfo[] parameters = Array.Empty<ParameterInfo>();

            Assert.Throws<ArgumentNullException>(() => new Constructor(constructorInfo!, parameters));
        }

        [Theory]
        [MemberData(nameof(CreateInstanceTestData))]
        public void CreateInstance_WithValidArguments_CreatesInstance(ConstructorInfo constructorInfo, object[] args)
        {
            // AI Verifiable Outcome: Verifies instance creation with diverse valid arguments.
            // London School TDD: Uses dynamic data to cover various constructor signatures.
            ParameterInfo[] parameters = constructorInfo.GetParameters();
            Constructor constructor = new Constructor(constructorInfo, parameters);

            object instance = constructor.CreateInstance(args);

            Assert.NotNull(instance);
            Assert.IsType(constructorInfo.DeclaringType, instance);
        }

        [Fact]
        public void CreateInstance_NullArguments_ThrowsArgumentNullException()
        {
            // AI Verifiable Outcome: Confirms ArgumentNullException for null arguments array.
            // London School TDD: Tests edge case for argument handling.
            ConstructorInfo constructorInfo = typeof(TestClassWithMultipleConstructors).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(c => c.GetParameters().Length == 0);
            ParameterInfo[] parameters = constructorInfo.GetParameters();
            Constructor constructor = new Constructor(constructorInfo, parameters);

            Assert.Throws<ArgumentNullException>(() => constructor.CreateInstance(null!));
        }

        [Fact]
        public void CreateInstance_MismatchArguments_ReturnsNull()
        {
            // AI Verifiable Outcome: Confirms null return for argument type mismatch.
            // London School TDD: Tests failure mode when arguments do not match constructor.
            ConstructorInfo constructorInfo = typeof(TestClassWithMultipleConstructors).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(c => c.GetParameters().Length == 1 && c.GetParameters()[0].ParameterType == typeof(int));
            ParameterInfo[] parameters = constructorInfo.GetParameters();
            Constructor constructor = new Constructor(constructorInfo, parameters);

            object[] args = new object[] { "wrong type" }; // Mismatched type

            object instance = constructor.CreateInstance(args);
            Assert.Null(instance);
        }

        [Theory]
        [MemberData(nameof(IsMatchTestData))]
        public void IsMatch_WithVariousArguments_ReturnsExpectedResult(ConstructorInfo constructorInfo, object[] args, bool expectedResult)
        {
            // AI Verifiable Outcome: Verifies correct argument matching for various scenarios.
            // London School TDD: Uses parameterized data to test valid and invalid argument combinations for IsMatch.
            ParameterInfo[] parameters = constructorInfo.GetParameters();
            Constructor constructor = new Constructor(constructorInfo, parameters);

            bool result = constructor.IsMatch(args);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void IsMatch_NullArguments_ThrowsArgumentNullException()
        {
            // AI Verifiable Outcome: Confirms ArgumentNullException for null arguments array in IsMatch.
            // London School TDD: Tests edge case for argument handling.
            ConstructorInfo constructorInfo = typeof(TestClassWithMultipleConstructors).GetConstructors(BindingFlags.Public | BindingFlags.Instance).First(c => c.GetParameters().Length == 0);
            ParameterInfo[] parameters = constructorInfo.GetParameters();
            Constructor constructor = new Constructor(constructorInfo, parameters);

            Assert.Throws<ArgumentNullException>(() => constructor.IsMatch(null!));
        }
    }
}