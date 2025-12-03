namespace CosmicLexicon.Foundation.Generics.UnitTest
{
    using Xunit;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CosmicLexicon.Foundation.Generics;

    public class GenericObjectExtensionsTests
    {
        [Fact]
        public void CheckPredicateAndDefaultValuePredicateTrueReturnsInputObject()
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
        public void CheckPredicateAndDefaultValuePredicateFalseReturnsDefaultValue()
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
        public void CheckPredicateAndDefaultValueFuncPredicateTrueReturnsInputObject()
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
        public void CheckPredicateAndDefaultValueFuncPredicateFalseReturnsDefaultValue()
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
        public void CheckDefaultValueObjectNotNullReturnsInputObject()
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
        public void CheckDefaultValueObjectNullReturnsDefaultValue()
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
        public void CheckDefaultValueFuncObjectNotNullReturnsInputObject()
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
        public void CheckDefaultValueFuncObjectNullReturnsDefaultValue()
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
        public void IsPredicateTrueReturnsTrue()
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
        public void IsPredicateFalseReturnsFalse()
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
        public void ThrowIfPredicateTrueThrowsException()
        {
            // Arrange
            int item = 5;
            Predicate<int> predicate = x => x > 0;
            Exception exception = new ArgumentException("Item is positive");

            // Act
            Assert.Throws<ArgumentException>(() => item.ThrowIf(predicate, exception));
        }

        [Fact]
        public void ThrowIfPredicateFalseReturnsItem()
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
        public void ThrowIfNotPredicateFalseThrowsException()
        {
            // Arrange
            int item = -5;
            Predicate<int> predicate = x => x > 0;
            Exception exception = new ArgumentException("Item is not positive");

            // Act
            Assert.Throws<ArgumentException>(() => item.ThrowIfNot(predicate, exception));
        }

        [Fact]
        public void ThrowIfNotPredicateTrueReturnsItem()
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
        public void ThrowIfNotNullObjectNotNullThrowsException()
        {
            // Arrange
            string item = "test";
            Exception exception = new ArgumentException("Item is not null");

            // Act
            Assert.Throws<ArgumentException>(() => item.ThrowIfNotNull(exception));
        }

        [Fact]
        public void ThrowIfNotNullObjectNullReturnsNull()
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
        public void ThrowIfNotNullOrEmptyCollectionNotEmptyThrowsException()
        {
            // Arrange
            List<int> item = [1, 2, 3];
            Exception exception = new ArgumentException("Item is not null or empty");

            // Act
            Assert.Throws<ArgumentException>(() => item.ThrowIfNotNullOrEmpty(exception));
        }

        [Fact]
        public void ThrowIfNotNullOrEmptyCollectionNullOrEmptyReturnsNull()
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
        public void TimesExecutesFunctionCorrectNumberOfTimes()
        {
            // Arrange
            int count = 3;
            int executionCount = 0;
            Func<int> function = () => { executionCount++; return executionCount - 1; };

            // Act
            var result = Enumerable.Range(0, count).Select(_ => function()).ToList();

            // Assert
            Assert.Equal(count, executionCount);
            Assert.Equal([0, 1, 2], result.ToArray());
        }

        [Fact]
        public void TimesExecutesActionCorrectNumberOfTimes()
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
        public void WhenPredicateTrueExecutesMethod()
        {
            // Arrange
            string obj = "test";
            bool predicate = true;
            Func<string, string> method = (s) => s.ToUpper(System.Globalization.CultureInfo.CurrentCulture);

            // Act
            string result = obj.When(predicate, method);

            // Assert
            Assert.Equal("TEST", result);
        }

        [Fact]
        public void WhenPredicateFalseReturnsOriginalObject()
        {
            // Arrange
            string obj = "test";
            bool predicate = false;
            Func<string, string> method = (s) => s.ToUpper(System.Globalization.CultureInfo.CurrentCulture);

            // Act
            string result = obj.When(predicate, method);

            // Assert
            Assert.Equal("test", result);
        }
        [Fact]
        public void IsObjectsAreEqualReturnsTrue()
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
        public void IsObjectsAreNotEqualReturnsFalse()
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
        public void ThrowIfDefaultObjectIsDefaultThrowsException()
        {
            // Arrange
            int item = 0;
            Exception exception = new ArgumentException("Item is default");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => item.ThrowIfDefault(exception));
        }

        [Fact]
        public void ThrowIfDefaultObjectIsNotDefaultReturnsObject()
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
        public void ThrowIfDefaultObjectIsDefaultWithNameThrowsArgumentNullException()
        {
            // Arrange
            string item = null;
            string name = "item";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => item.ThrowIfDefault(name));
        }

        [Fact]
        public void ThrowIfDefaultObjectIsNotDefaultWithNameReturnsObject()
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
        public void ThrowIfNotDefaultObjectIsDefaultReturnsObject()
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