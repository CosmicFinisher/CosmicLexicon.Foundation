using Xunit;
using System.Collections.Generic;
using ConsmicLexicon.Foundation.Structures;
using ConsmicLexicon.Foundation.Formats;
using System;

namespace ConsmicLexicon.Foundation.xTests
{
    public class HighLevelAcceptanceTests
    {
        [Fact]
        public void CollectionManipulationTest()
        {
            // Arrange
            var list = new List<string>();

            // Act
            list.Add("apple");
            list.Add("banana");
            list.Remove("apple");
            bool containsBanana = list.Contains("banana");

            // Assert
            Assert.True(containsBanana);
            Assert.Single(list);
        }

        [Fact]
        public void StringProcessingTest()
        {
            // Arrange
            string input = "  Hello, World!  ";

            // Act
            string trimmed = input.Trim();
            string lowerCase = trimmed.ToLower();

            // Assert
            Assert.Equal("hello, world!", lowerCase);
        }

        [Fact]
        public void DateTimeHandlingTest()
        {
            // Arrange
            string dateString = "2024-01-01";

            // Act
            DateTime parsedDate = DateTime.Parse(dateString);
            string formattedDate = parsedDate.ToString("MM/dd/yyyy");

            // Assert
            Assert.Equal("01/01/2024", formattedDate);
        }

        [Fact]
        public void ExtensionMethodUsageTest()
        {
            // Arrange
            string input = "this is a test";

            // Act
            string capitalized = input.Capitalize();

            // Assert
            Assert.Equal("This is a test", capitalized);
        }
    }
}