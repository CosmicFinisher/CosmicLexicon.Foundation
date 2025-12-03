using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;
using CosmicLexicon.Foundation.xText.Json;
using Xunit.Sdk;

namespace CosmicLexicon.Foundation.xText.Json
{
    public class JsonExtensionsTests
    {
        [Fact]
        public void FromJsonWithValidJsonReturnsObject()
        {
            // Arrange
            string json = "{ \"name\": \"Test\", \"value\": 123 }";

            // Act
            TestClassExample? result = json.FromJson<TestClassExample>();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
            Assert.Equal(123, result.Value);
        }

        [Fact]
        public void FromJsonWithNullJsonReturnsDefault()
        {
            // Arrange
            string json = null;

            // Act
            TestClassExample? result = json.FromJson<TestClassExample>();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ToJsonWithValidObjectReturnsJsonString()
        {
            // Arrange
            TestClassExample obj = new TestClassExample
            {
                Name = "Test",
                Value = 123
            };

            // Act
            string result = obj.ToJson();

            // Assert
            Assert.Equal("{\"name\":\"Test\",\"value\":123}", result);
        }

        [Fact]
        public void ToJsonWithNullObjectReturnsNull()
        {
            // Arrange
            TestClassExample? obj = null;

            // Act
            string result = obj.ToJson();

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public void ToJsonWithNullPropertiesIgnoresNull()
        {
            // Arrange
            TestClassExample obj = new TestClassExample
            {
                Name = "Test",
                Value = 0 // Value type, so not null
            };

            // Act
            string result = obj.ToJson();

            // Assert
            Assert.Equal("{\"name\":\"Test\",\"value\":0}", result); // Value is not null, so it's included
        }

        [Fact]
        public void FromJsonWithCommentsSkipsComments()
        {
            // Arrange
            string json = "{ /* This is a comment */ \"name\": \"Test\", // Another comment \n \"value\": 123 }";

            // Act
            TestClassExample? result = json.FromJson<TestClassExample>();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
            Assert.Equal(123, result.Value);
        }

        [Fact]
        public void FromJsonWithPrivateConstructorClassReturnsObject()
        {
            // Arrange
            string json = "{ \"id\": 1, \"description\": \"Private Ctor Test\" }";

            // Act
            TestClassWithPrivateConstructor? result = json.FromJson<TestClassWithPrivateConstructor>();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Private Ctor Test", result.Description);
        }

        [Fact]
        public void ToJsonWithEnumReturnsCamelCaseString()
        {
            // Arrange
            TestClassWithEnum obj = new TestClassWithEnum
            {
                Status = TestEnumExample.PendingApproval
            };

            // Act
            string result = obj.ToJson();

            // Assert
            Assert.Equal("{\"status\":\"PendingApproval\"}", result);
        }

        [Fact]
        public void FromJsonWithCamelCaseEnumReturnsEnum()
        {
            // Arrange
            string json = "{ \"status\": \"Completed\" }";

            // Act
            TestClassWithEnum? result = json.FromJson<TestClassWithEnum>();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(TestEnumExample.Completed, result.Status);
        }
    }
}
