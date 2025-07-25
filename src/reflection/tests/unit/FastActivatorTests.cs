using System;
using System.Linq;
using Xunit;
using OpenEchoSystem.Core.xReflection;

namespace OpenEchoSystem.Core.xReflection
{
    public class FastActivatorTests
    {
        [Fact]
        public void CreateInstanceWithTypeAndParameterlessConstructorCreatesInstance()
        {
            // Arrange
            Type type = typeof(TestClassWithParameterlessConstructor);

            // Act
            object instance = FastActivator.CreateInstance(type);

            // Assert
            Assert.NotNull(instance);
            Assert.IsType(type, instance);
        }

        [Fact]
        public void CreateInstanceWithTypeAndArgumentsCreatesInstance()
        {
            // Arrange
            Type type = typeof(TestClassWithArguments);
            object[] args = new object[] { 1, "test" };

            // Act
            object instance = FastActivator.CreateInstance(type, args);

            // Assert
            Assert.NotNull(instance);
            Assert.IsType(type, instance);
        }

        [Fact]
        public void CreateInstanceGenericWithParameterlessConstructorCreatesInstance()
        {
            // Arrange

            // Act
            TestClassWithParameterlessConstructor instance = FastActivator.CreateInstance<TestClassWithParameterlessConstructor>();

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void CreateInstanceGenericWithArgumentsCreatesInstance()
        {
            // Arrange
            object[] args = new object[] { 1, "test" };

            // Act
            TestClassWithArguments instance = FastActivator.CreateInstance<TestClassWithArguments>(args);

            // Assert
            Assert.NotNull(instance);
        }

        // Test classes for FastActivator
        public class TestClassWithParameterlessConstructor
        {
            public TestClassWithParameterlessConstructor() { }
            public string Message { get; set; } = "Default";
        }

        public class TestClassWithArguments
        {
            public int Value { get; }
            public string Name { get; }
            public TestClassWithArguments(int value, string name)
            {
                Value = value;
                Name = name;
            }
        }

        public class TestClassOnlyParameterized
        {
            public int Value { get; }
            public TestClassOnlyParameterized(int value)
            {
                Value = value;
            }
        }

        public class TestClassWithPrivateConstructor
        {
            private TestClassWithPrivateConstructor() { }
            public static TestClassWithPrivateConstructor Create() => new TestClassWithPrivateConstructor();
        }

        public struct TestStructWithParameterlessConstructor
        {
            public int Value { get; set; }
            public TestStructWithParameterlessConstructor() { Value = 0; }
        }

        public struct TestStructOnlyParameterized
        {
            public int Value { get; }
            public TestStructOnlyParameterized(int value)
            {
                Value = value;
            }
        }

        public abstract class AbstractTestClass { }

        public interface ITestInterface { }

        // MemberData for parameterized tests
        public static TheoryData<Type, object[]?, object?> CreateInstanceTestData()
        {
            return new TheoryData<Type, object[]?, object?>
            {
                { typeof(TestClassWithParameterlessConstructor), null, new TestClassWithParameterlessConstructor() },
                { typeof(TestClassWithArguments), new object[] { 10, "hello" }, new TestClassWithArguments(10, "hello") },
                { typeof(TestClassOnlyParameterized), new object[] { 20 }, new TestClassOnlyParameterized(20) },
                { typeof(TestStructWithParameterlessConstructor), null, new TestStructWithParameterlessConstructor() },
                { typeof(TestStructOnlyParameterized), new object[] { 30 }, new TestStructOnlyParameterized(30) },
            };
        }

        public static TheoryData<Type, object[]?> CreateInstanceFailureTestData()
        {
            return new TheoryData<Type, object[]?>
            {
                { typeof(TestClassWithArguments), new object[] { "wrong", 10 } }, // Mismatched types
                { typeof(TestClassWithArguments), new object[] { 10 } }, // Not enough arguments
                { typeof(TestClassWithArguments), new object[] { 10, "hello", true } }, // Too many arguments
                { typeof(TestClassOnlyParameterized), null }, // No parameterless constructor
                { typeof(TestClassWithPrivateConstructor), null }, // Private constructor
                { typeof(AbstractTestClass), null }, // Abstract class
                { typeof(ITestInterface), null }, // Interface
            };
        }

        [Theory]
        [MemberData(nameof(CreateInstanceTestData))]
        public void CreateInstance_WithValidInputs_CreatesInstance(Type type, object[]? args, object? expectedInstance)
        {
            // AI Verifiable Outcome: Confirms successful instance creation for various valid types and arguments.
            // London School TDD: Uses parameterized tests to dynamically verify correct behavior for diverse inputs.
            object instance = (args == null) ? FastActivator.CreateInstance(type) : FastActivator.CreateInstance(type, args);

            Assert.NotNull(instance);
            Assert.IsType(type, instance);

            // For TestClassWithArguments, verify properties
            if (instance is TestClassWithArguments tcwa && expectedInstance is TestClassWithArguments extcwa)
            {
                Assert.Equal(extcwa.Value, tcwa.Value);
                Assert.Equal(extcwa.Name, tcwa.Name);
            }
            // For TestClassOnlyParameterized, verify properties
            else if (instance is TestClassOnlyParameterized tcop && expectedInstance is TestClassOnlyParameterized extcop)
            {
                Assert.Equal(extcop.Value, tcop.Value);
            }
        }

        [Theory]
        [MemberData(nameof(CreateInstanceFailureTestData))]
        public void CreateInstance_WithInvalidInputs_ReturnsNull(Type type, object[]? args)
        {
            // AI Verifiable Outcome: Confirms that CreateInstance returns null for various invalid scenarios.
            // London School TDD: Tests edge cases and failure paths to ensure robustness.
            object? instance = (args == null) ? FastActivator.CreateInstance(type) : FastActivator.CreateInstance(type, args);
            Assert.Null(instance);
        }

        [Fact]
        public void CreateInstance_NullType_ThrowsArgumentNullException()
        {
            // AI Verifiable Outcome: Confirms ArgumentNullException when type is null.
            // London School TDD: Tests input validation.
            Type type = null!;
            Assert.Throws<ArgumentNullException>(() => FastActivator.CreateInstance(type));
        }

        [Fact]
        public void CreateInstanceGeneric_WithParameterlessConstructor_CreatesInstance()
        {
            // AI Verifiable Outcome: Confirms successful instance creation for generic types with parameterless constructors.
            // London School TDD: Verifies the generic overload of CreateInstance.
            TestClassWithParameterlessConstructor instance = FastActivator.CreateInstance<TestClassWithParameterlessConstructor>();
            Assert.NotNull(instance);
            Assert.Equal("Default", instance.Message);
        }

        [Fact]
        public void CreateInstanceGeneric_WithArguments_CreatesInstance()
        {
            // AI Verifiable Outcome: Confirms successful instance creation for generic types with arguments.
            // London School TDD: Verifies the generic overload with arguments.
            object[] args = new object[] { 100, "generic test" };
            TestClassWithArguments instance = FastActivator.CreateInstance<TestClassWithArguments>(args);
            Assert.NotNull(instance);
            Assert.Equal(100, instance.Value);
            Assert.Equal("generic test", instance.Name);
        }

        [Fact]
        public void CreateInstanceGeneric_NoParameterlessConstructor_ReturnsDefault()
        {
            // AI Verifiable Outcome: Confirms default(T) is returned for reference types without a parameterless constructor.
            // London School TDD: Tests the behavior for an uncreatable reference type via parameterless generic activation.
            TestClassOnlyParameterized instance = FastActivator.CreateInstance<TestClassOnlyParameterized>();
            Assert.Null(instance);
        }

        [Fact]
        public void CreateInstanceGeneric_WithMismatchedArguments_ReturnsDefault()
        {
            // AI Verifiable Outcome: Confirms default(T) is returned for argument type mismatches.
            // London School TDD: Tests the failure case for mismatched arguments in generic activation.
            object[] args = new object[] { "wrong type", 123 };
            TestClassWithArguments instance = FastActivator.CreateInstance<TestClassWithArguments>(args);
            Assert.Null(instance);
        }

        [Fact]
        public void CreateInstanceGeneric_AbstractClass_ReturnsDefault()
        {
            // AI Verifiable Outcome: Confirms default(T) is returned for abstract classes.
            // London School TDD: Tests activation of abstract types.
            AbstractTestClass instance = FastActivator.CreateInstance<AbstractTestClass>();
            Assert.Null(instance);
        }

        [Fact]
        public void CreateInstanceGeneric_Interface_ReturnsDefault()
        {
            // AI Verifiable Outcome: Confirms default(T) is returned for interfaces.
            // London School TDD: Tests activation of interface types.
            ITestInterface instance = FastActivator.CreateInstance<ITestInterface>();
            Assert.Null(instance);
        }

        [Fact]
        public void CreateInstanceGeneric_ValueTypeNoParameterlessConstructor_ReturnsDefaultValueType()
        {
            // AI Verifiable Outcome: Confirms default value type is returned for structs without matching constructor.
            // London School TDD: Tests value type activation when no suitable public constructor is found.
            TestStructOnlyParameterized instance = FastActivator.CreateInstance<TestStructOnlyParameterized>();
            Assert.Equal(default(TestStructOnlyParameterized), instance);
        }

        [Fact]
        public void CreateInstanceGeneric_PrivateConstructor_ReturnsDefault()
        {
            // AI Verifiable Outcome: Confirms default(T) is returned for types with only private constructors.
            // London School TDD: Tests activation failure for inaccessible constructors.
            TestClassWithPrivateConstructor instance = FastActivator.CreateInstance<TestClassWithPrivateConstructor>();
            Assert.Null(instance);
        }
    }
}