using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using CosmicLexicon.Foundation.xReflection.xAssembly;

namespace CosmicLexicon.Foundation.xReflection.xAssembly
{
    public class PropertyInfoExtensionsTests
    {
        [Fact]
        public void HasNullableOperatorWithNullablePropertyReturnsTrue()
        {
            // Arrange
            PropertyInfo property = typeof(TestClass).GetProperty(nameof(TestClass.NullableInt));

            // Act
            bool result = property.HasNullableOperator();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void HasNullableOperatorWithNonNullablePropertyReturnsFalse()
        {
            // Arrange
            PropertyInfo property = typeof(TestClass).GetProperty(nameof(TestClass.NonNullableInt));

            // Act
            bool result = property.HasNullableOperator();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HasAnInaccessibleSetterWithProtectedInternalSetterReturnsTrue()
        {
            // Arrange
            PropertyInfo property = typeof(TestClass).GetProperty(nameof(TestClass.ProtectedInternalSetter));

            // Act
            bool result = !property.IsAccessibleFromSetter();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void HasAnInaccessibleSetterWithPublicSetterReturnsFalse()
        {
            // Arrange
            PropertyInfo property = typeof(TestClass).GetProperty(nameof(TestClass.PublicSetter));

            // Act
            bool result = property.HasAnInaccessibleSetter();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsNullableWithNullablePropertyReturnsTrue()
        {
            // Arrange
            PropertyInfo property = typeof(TestClass).GetProperty(nameof(TestClass.NullableInt));

            // Act
            bool result = property.IsNullable();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullableWithNonNullablePropertyReturnsFalse()
        {
            // Arrange
            PropertyInfo property = typeof(TestClass).GetProperty(nameof(TestClass.NonNullableInt));

            // Act
            bool result = property.IsNullable();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void TryGetCustomAttributeWithAttributeReturnsTrueAndAttribute()
        {
            // Arrange
            PropertyInfo property = typeof(TestClass).GetProperty(nameof(TestClass.AttributedProperty));

            // Act
            bool result = property.TryGetCustomAttribute<TestAttribute>(out IEnumerable<TestAttribute> attributes);

            // Assert
            Assert.True(result);
            Assert.NotNull(attributes);
            Assert.Equal(1, attributes.Count());
        }

        [Fact]
        public void TryGetCustomAttributeWithNoAttributeReturnsFalseAndNull()
        {
            // Arrange
            PropertyInfo property = typeof(TestClass).GetProperty(nameof(TestClass.NonNullableInt));

            // Act
            bool result = property.TryGetCustomAttribute<TestAttribute>(out IEnumerable<TestAttribute> attributes);

            // Assert
            Assert.False(result);
            Assert.Null(attributes);
        }

        [Fact]
        public void IsAccessibleFromSetterWithPublicSetterReturnsTrue()
        {
            // Arrange
            PropertyInfo property = typeof(TestClass).GetProperty(nameof(TestClass.PublicSetter));

            // Act
            bool result = property.IsAccessibleFromSetter();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsAccessibleFromSetterWithProtectedInternalSetterReturnsFalse()
        {
            // Arrange
            PropertyInfo property = typeof(TestClass).GetProperty(nameof(TestClass.ProtectedInternalSetter));

            // Act
            bool result = property.IsAccessibleFromSetter();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AnySetterWithSetterReturnsTrue()
        {
            // Arrange
            PropertyInfo property = typeof(TestClass).GetProperty(nameof(TestClass.PublicSetter));

            // Act
            bool result = property.AnySetter();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AnySetterWithNoSetterReturnsFalse()
        {
            // Arrange
            PropertyInfo property = typeof(TestClass).GetProperty(nameof(TestClass.ReadOnlyProperty));

            // Act
            bool result = property.AnySetter();

            // Assert
            Assert.False(result);
        }

        public class TestClass
        {
            public int? NullableInt { get; set; }
            public int NonNullableInt { get; set; }
            public string PublicSetter { get; set; }
            protected internal string ProtectedInternalSetter { get; set; }
            public string ReadOnlyProperty { get; }

            [TestAttribute]
            public string AttributedProperty { get; set; }
        }

        public class TestAttribute : Attribute { }
    }
}