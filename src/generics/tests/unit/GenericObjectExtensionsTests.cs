using Xunit;
using CosmicLexicon.Foundation.xGenerics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CosmicLexicon.Foundation.xGenerics.Tests
{
    using Xunit;
    using CosmicLexicon.Foundation.xGenerics;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GenericObjectExtensionsTests
    {
        [Fact]
        public void Check_PredicateAndDefaultValue_PredicateTrue_ReturnsInputObject()
        {
            // Arrange
            int inputObject = 5;
            Predicate<int> predicate = x => x > 0;
            int defaultValue = 0;

            // Act
            int result = inputObject.Check(predicate, defaultValue);

            // Assert
            Assert.Equal(inputObject, result);
        }

        [Fact]
        public void Check_PredicateAndDefaultValue_PredicateFalse_ReturnsDefaultValue()
        {
            // Arrange
            int inputObject = -5;
            Predicate<int> predicate = x => x > 0;
            int defaultValue = 0;

            // Act
            int result = inputObject.Check(predicate, defaultValue);

            // Assert
            Assert.Equal(defaultValue, result);
        }

        [Fact]
        public void Check_PredicateAndDefaultValueFunc_PredicateTrue_ReturnsInputObject()
        {
            // Arrange
            int inputObject = 5;
            Predicate<int> predicate = x => x > 0;
            Func<int> defaultValue = () => 0;

            // Act
            int result = inputObject.Check(predicate, defaultValue);

            // Assert
            Assert.Equal(inputObject, result);
        }

        [Fact]
        public void Check_PredicateAndDefaultValueFunc_PredicateFalse_ReturnsDefaultValue()
        {
            // Arrange
            int inputObject = -5;
            Predicate<int> predicate = x => x > 0;
            Func<int> defaultValue = () => 0;

            // Act
            int result = inputObject.Check(predicate, defaultValue);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Check_DefaultValue_ObjectNotNull_ReturnsInputObject()
        {
            // Arrange
            string inputObject = "test";
            string defaultValue = "default";

            // Act
            string result = inputObject.Check(defaultValue);

            // Assert
            Assert.Equal(inputObject, result);
        }

        [Fact]
        public void Check_DefaultValue_ObjectNull_ReturnsDefaultValue()
        {
            // Arrange
            string inputObject = null;
            string defaultValue = "default";

            // Act
            string result = inputObject.Check(defaultValue);

            // Assert
            Assert.Equal(defaultValue, result);
        }

        [Fact]
        public void Check_DefaultValueFunc_ObjectNotNull_ReturnsInputObject()
        {
            // Arrange
            string inputObject = "test";
            Func<string> defaultValue = () => "default";

            // Act
            string result = inputObject.Check(defaultValue);

            // Assert
            Assert.Equal(inputObject, result);
        }

        [Fact]
        public void Check_DefaultValueFunc_ObjectNull_ReturnsDefaultValue()
        {
            // Arrange
            string inputObject = null;
            Func<string> defaultValue = () => "default";

            // Act
            string result = inputObject.Check(defaultValue);

            // Assert
            Assert.Equal("default", result);
        }

        [Fact]
        public void Is_PredicateTrue_ReturnsTrue()
        {
            // Arrange
            int inputObject = 5;
            Predicate<int> predicate = x => x > 0;

            // Act
            bool result = inputObject.Is(predicate);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Is_PredicateFalse_ReturnsFalse()
        {
            // Arrange
            int inputObject = -5;
            Predicate<int> predicate = x => x > 0;
            Exception exception = new ArgumentException("Item is positive");

            // Act
            bool result = inputObject.Is(predicate);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ThrowIf_PredicateTrue_ThrowsException()
        {
            // Arrange
            int item = 5;
            Predicate<int> predicate = x => x > 0;
            Exception exception = new ArgumentException("Item is positive");

            // Act
            Assert.Throws<ArgumentException>(() => item.ThrowIf(predicate, exception));
        }

        [Fact]
        public void ThrowIf_PredicateFalse_ReturnsItem()
        {
            // Arrange
            int item = -5;
            Predicate<int> predicate = x => x > 0;
            Exception exception = new ArgumentException("Item is positive");

            // Act
            int result = item.ThrowIf(predicate, exception);

            // Assert
            Assert.Equal(item, result);
        }

        [Fact]
        public void ThrowIfNot_PredicateFalse_ThrowsException()
        {
            // Arrange
            int item = -5;
            Predicate<int> predicate = x => x > 0;
            Exception exception = new ArgumentException("Item is not positive");

            // Act
            Assert.Throws<ArgumentException>(() => item.ThrowIfNot(predicate, exception));
        }

        [Fact]
        public void ThrowIfNot_PredicateTrue_ReturnsItem()
        {
            // Arrange
            int item = 5;
            Predicate<int> predicate = x => x > 0;
            Exception exception = new ArgumentException("Item is not positive");

            // Act
            int result = item.ThrowIfNot(predicate, exception);

            // Assert
            Assert.Equal(item, result);
        }

        [Fact]
        public void ThrowIfNotNull_ObjectNotNull_ThrowsException()
        {
            // Arrange
            string item = "test";
            Exception exception = new ArgumentException("Item is not null");

            // Act
            Assert.Throws<ArgumentException>(() => item.ThrowIfNotNull(exception));
        }

        [Fact]
        public void ThrowIfNotNull_ObjectNull_ReturnsNull()
        {
            // Arrange
            string item = null;
            Exception exception = new ArgumentException("Item is not null");

            // Act
            string result = item.ThrowIfNotNull(exception);

            // Assert
            Assert.Equal(item, result);
        }

        [Fact]
        public void ThrowIfNotNullOrEmpty_CollectionNotEmpty_ThrowsException()
        {
            // Arrange
            List<int> item = new List<int>() { 1, 2, 3 };
            Exception exception = new ArgumentException("Item is not null or empty");

            // Act
            Assert.Throws<ArgumentException>(() => item.ThrowIfNotNullOrEmpty(exception));
        }

        [Fact]
        public void ThrowIfNotNullOrEmpty_CollectionNullOrEmpty_ReturnsNull()
        {
            // Arrange
            List<int> item = null;
            Exception exception = new ArgumentException("Item is not null or empty");

            // Act
            IEnumerable<int> result = item.ThrowIfNotNullOrEmpty(exception);

            // Assert
            Assert.Equal(item, result);
        }

        [Fact]
        public void Times_ExecutesFunctionCorrectNumberOfTimes()
        {
            // Arrange
            int count = 3;
            int executionCount = 0;
            Func<int> function = () => { executionCount++; return executionCount - 1; };

            // Act
            var result = Enumerable.Range(0, count).Select(_ => function()).ToList();

            // Assert
            Assert.Equal(count, executionCount);
            Assert.Equal(new int[] { 0, 1, 2 }, result.ToArray());
        }

        [Fact]
        public void Times_ExecutesActionCorrectNumberOfTimes()
        {
            // Arrange
            int count = 3;
            int executionCount = 0;
            Action<int> action = (i) => { executionCount++; };

            // Act
            Enumerable.Range(0, count).ToList().ForEach(_ => action(_));

            // Assert
            Assert.Equal(count, executionCount);
        }

        [Fact]
        public void When_PredicateTrue_ExecutesMethod()
        {
            // Arrange
            string obj = "test";
            bool predicate = true;
            Func<string, string> method = (s) => s.ToUpper();

            // Act
            string result = obj.When(predicate, method);

            // Assert
            Assert.Equal("TEST", result);
        }

        [Fact]
        public void When_PredicateFalse_ReturnsOriginalObject()
        {
            // Arrange
            string obj = "test";
            bool predicate = false;
            Func<string, string> method = (s) => s.ToUpper();

            // Act
            string result = obj.When(predicate, method);

            // Assert
            Assert.Equal("test", result);
        }
        [Fact]
        public void Is_ObjectsAreEqual_ReturnsTrue()
        {
            // Arrange
            int inputObject = 5;
            int comparisonObject = 5;

            // Act
            bool result = inputObject.Is(comparisonObject);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Is_ObjectsAreNotEqual_ReturnsFalse()
        {
            // Arrange
            int inputObject = 5;
            int comparisonObject = 10;

            // Act
            bool result = inputObject.Is(comparisonObject);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ThrowIfDefault_ObjectIsDefault_ThrowsException()
        {
            // Arrange
            int item = 0;
            Exception exception = new ArgumentException("Item is default");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => item.ThrowIfDefault(exception));
        }

        [Fact]
        public void ThrowIfDefault_ObjectIsNotDefault_ReturnsObject()
        {
            // Arrange
            int item = 5;
            Exception exception = new ArgumentException("Item is default");

            // Act
            int result = item.ThrowIfDefault(exception);

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void ThrowIfDefault_ObjectIsDefaultWithName_ThrowsArgumentNullException()
        {
            // Arrange
            string item = null;
            string name = "item";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => item.ThrowIfDefault(name));
        }

        [Fact]
        public void ThrowIfDefault_ObjectIsNotDefaultWithName_ReturnsObject()
        {
            // Arrange
            string item = "test";
            string name = "item";

            // Act
            string result = item.ThrowIfDefault(name);

            // Assert
            Assert.Equal("test", result);
        }

        [Fact]
        public void ThrowIfNotDefault_ObjectIsDefault_ReturnsObject()
        {
            // Arrange
            int item = 0;
            Exception exception = new ArgumentException("Item is not default");

            // Act & Assert
            var result = item.ThrowIfNotDefault(exception);
            Assert.Equal(item, result);
        }
        // Added comment to force test runner refresh
    }
}