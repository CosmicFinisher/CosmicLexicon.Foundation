using System;
using System.Text;
using Xunit;
using CosmicLexicon.Foundation.Formats;
using Moq; // Added for mocking

namespace CosmicLexicon.Foundation.xText
{
    public class ByteArrayHelpersTests
    {
        [Fact]
        public void IsUnicodeWithUtf16LEBomReturnsTrue()
        {
            // AI Verifiable End Result: Verify IsUnicode correctly identifies UTF-16LE BOM
            var input = new byte[] { 0xFF, 0xFE, 0x41, 0x00, 0x42, 0x00 }; // UTF-16LE for "AB"
            Assert.True(ByteArrayHelpers.IsUnicode(input));
        }

        [Fact]
        public void IsUnicodeWithUtf16BEBomReturnsTrue()
        {
            // AI Verifiable End Result: Verify IsUnicode correctly identifies UTF-16BE BOM
            var input = new byte[] { 0xFE, 0xFF, 0x00, 0x41, 0x00, 0x42 }; // UTF-16BE for "AB"
            Assert.True(ByteArrayHelpers.IsUnicode(input));
        }

        [Fact]
        public void IsUnicodeWithNoBomReturnsFalse()
        {
            // AI Verifiable End Result: Verify IsUnicode correctly identifies non-BOM input
            var input = Encoding.UTF8.GetBytes("Hello");
            Assert.False(ByteArrayHelpers.IsUnicode(input));
        }

        [Fact]
        public void IsUnicodeWithNullInputReturnsFalse()
        {
            // AI Verifiable End Result: Verify IsUnicode handles null input
            Assert.False(ByteArrayHelpers.IsUnicode(null));
        }

        [Fact]
        public void IsUnicodeWithEmptyArrayReturnsFalse()
        {
            // AI Verifiable End Result: Verify IsUnicode handles empty array
            Assert.False(ByteArrayHelpers.IsUnicode(new byte[0]));
        }

        [Fact]
        public void IsUnicodeWithSingleByteReturnsFalse()
        {
            // AI Verifiable End Result: Verify IsUnicode handles single byte array
            Assert.False(ByteArrayHelpers.IsUnicode(new byte[] { 0x41 }));
        }

        [Fact]
        public void ToStringBase64WithValidInputReturnsCorrectBase64String()
        {
            // AI Verifiable End Result: Verify ToString (Base64) with valid input
            var input = Encoding.UTF8.GetBytes("Hello World");
            var expected = Convert.ToBase64String(input);
            Assert.Equal(expected, ByteArrayHelpers.ToString(input));
        }

        [Fact]
        public void ToStringBase64WithValidInputAndIndexCountReturnsCorrectBase64Substring()
        {
            // AI Verifiable End Result: Verify ToString (Base64) with index and count
            var input = Encoding.UTF8.GetBytes("Hello World");
            var expected = Convert.ToBase64String(input, 6, 5); // "World"
            Assert.Equal(expected, ByteArrayHelpers.ToString(input, 6, 5));
        }

        [Fact]
        public void ToStringBase64WithNullInputReturnsEmptyString()
        {
            // AI Verifiable End Result: Verify ToString (Base64) handles null input
            Assert.Equal(string.Empty, ByteArrayHelpers.ToString(null));
        }

        [Fact]
        public void ToStringBase64WithEmptyInputReturnsEmptyString()
        {
            // AI Verifiable End Result: Verify ToString (Base64) handles empty input
            Assert.Equal(string.Empty, ByteArrayHelpers.ToString(Array.Empty<byte>()));
        }

        [Fact]
        public void ToStringBase64WithNegativeIndexReturnsCorrectBase64StringFromStart()
        {
            // AI Verifiable End Result: Verify ToString (Base64) handles negative index
            var input = Encoding.UTF8.GetBytes("Test");
            var expected = Convert.ToBase64String(input, 0, 4);
            Assert.Equal(expected, ByteArrayHelpers.ToString(input, -1, 4));
        }

        [Fact]
        public void ToStringBase64WithNegativeCountReturnsCorrectBase64StringToEnd()
        {
            // AI Verifiable End Result: Verify ToString (Base64) handles negative count
            var input = Encoding.UTF8.GetBytes("Test");
            var expected = Convert.ToBase64String(input, 2, 2); // "st"
            Assert.Equal(expected, ByteArrayHelpers.ToString(input, 2, -1));
        }

        [Fact]
        public void ToStringBase64WithIndexOutOfBoundsReturnsEmptyString()
        {
            // AI Verifiable End Result: Verify ToString (Base64) handles index out of bounds
            var input = Encoding.UTF8.GetBytes("Test");
            Assert.Equal(string.Empty, ByteArrayHelpers.ToString(input, 10, 2));
        }

        [Fact]
        public void ToStringBase64WithCountExceedingLengthReturnsCorrectBase64StringToEnd()
        {
            // AI Verifiable End Result: Verify ToString (Base64) handles count exceeding length
            var input = Encoding.UTF8.GetBytes("Test");
            var expected = Convert.ToBase64String(input, 1, 3); // "est"
            Assert.Equal(expected, ByteArrayHelpers.ToString(input, 1, 10));
        }

        [Fact]
        public void ToStringEncodedWithValidInputAndUtf8EncodingReturnsCorrectString()
        {
            // AI Verifiable End Result: Verify ToString (Encoded) with valid UTF8 input
            var input = Encoding.UTF8.GetBytes("Hello World");
            Assert.Equal("Hello World", ByteArrayHelpers.ToString(input, Encoding.UTF8));
        }

        [Fact]
        public void ToStringEncodedWithValidInputAndIndexCountAndUtf8EncodingReturnsCorrectSubstring()
        {
            // AI Verifiable End Result: Verify ToString (Encoded) with index, count, and UTF8 encoding
            var input = Encoding.UTF8.GetBytes("Hello World");
            Assert.Equal("World", ByteArrayHelpers.ToString(input, Encoding.UTF8, 6, 5));
        }

        [Fact]
        public void ToStringEncodedWithValidInputAndAsciiEncodingReturnsCorrectString()
        {
            // AI Verifiable End Result: Verify ToString (Encoded) with valid ASCII input
            var input = Encoding.ASCII.GetBytes("Hello");
            Assert.Equal("Hello", ByteArrayHelpers.ToString(input, Encoding.ASCII));
        }

        [Fact]
        public void ToStringEncodedWithNullInputReturnsEmptyString()
        {
            // AI Verifiable End Result: Verify ToString (Encoded) handles null input
            Assert.Equal(string.Empty, ByteArrayHelpers.ToString(null, Encoding.UTF8));
        }

        [Fact]
        public void ToStringEncodedWithEmptyInputReturnsEmptyString()
        {
            // AI Verifiable End Result: Verify ToString (Encoded) handles empty input
            Assert.Equal(string.Empty, ByteArrayHelpers.ToString(Array.Empty<byte>(), Encoding.UTF8));
        }

        [Fact]
        public void ToStringEncodedWithNegativeIndexReturnsCorrectStringFromStart()
        {
            // AI Verifiable End Result: Verify ToString (Encoded) handles negative index
            var input = Encoding.UTF8.GetBytes("Test");
            Assert.Equal("Test", ByteArrayHelpers.ToString(input, Encoding.UTF8, -1, 4));
        }

        [Fact]
        public void ToStringEncodedWithNegativeCountReturnsCorrectStringToEnd()
        {
            // AI Verifiable End Result: Verify ToString (Encoded) handles negative count
            var input = Encoding.UTF8.GetBytes("Test");
            Assert.Equal("st", ByteArrayHelpers.ToString(input, Encoding.UTF8, 2, -1));
        }

        [Fact]
        public void ToStringEncodedWithIndexOutOfBoundsReturnsEmptyString()
        {
            // AI Verifiable End Result: Verify ToString (Encoded) handles index out of bounds
            var input = Encoding.UTF8.GetBytes("Test");
            Assert.Equal(string.Empty, ByteArrayHelpers.ToString(input, Encoding.UTF8, 10, 2));
        }

        [Fact]
        public void ToStringEncodedWithCountExceedingLengthReturnsCorrectStringToEnd()
        {
            // AI Verifiable End Result: Verify ToString (Encoded) handles count exceeding length
            var input = Encoding.UTF8.GetBytes("Test");
            Assert.Equal("est", ByteArrayHelpers.ToString(input, Encoding.UTF8, 1, 10));
        }

        [Fact]
        public void ToStringEncodedWithNullEncodingDefaultsToUtf8()
        {
            // AI Verifiable End Result: Verify ToString (Encoded) defaults to UTF8 with null encoding
            var input = Encoding.UTF8.GetBytes("Hello");
            Assert.Equal("Hello", ByteArrayHelpers.ToString(input, null));
        }

        // Test Case 5.2.1: Valid UTF8 Encoding Conversion
        [Fact]
        public void ToString_ValidUtf8EncodingConversion_UsesEncodingGetString()
        {
            // Collaborators to Mock: System.Text.Encoding (or a specific implementation like UTF8Encoding)
            var mockEncoding = new Mock<UTF8Encoding>();

            // Mock Configuration: Configure the mock's GetString method
            byte[] inputBytes = { 72, 101, 108, 108, 111 }; // ASCII for "Hello"
            mockEncoding.Setup(e => e.GetString(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                        .Returns("MOCKED_DECODED_STRING");

            // Test Data
            Encoding encodingUsing = mockEncoding.Object;
            int index = 0;
            int count = -1;

            // Act
            string result = ByteArrayHelpers.ToString(inputBytes, encodingUsing, index, count);

            // Assert - Precise Observable Outcome (AI Verifiable)
            Assert.Equal("MOCKED_DECODED_STRING", result);
            mockEncoding.Verify(e => e.GetString(inputBytes, index, inputBytes.Length), Times.Once); // Verify interaction
        }

        // Test Case 5.2.2: Null Encoding Provided (Default Behavior)
        [Fact]
        public void ToString_NullEncodingProvided_UsesEncodingDefault()
        {
            // Collaborators to Mock: None, as we are testing the default behavior of the static method.
            // Test Data
            byte[] input = { 72, 101, 108, 108, 111 }; // ASCII for "Hello"
            Encoding? encodingUsing = null;
            int index = 0;
            int count = -1;

            // Act
            string result = ByteArrayHelpers.ToString(input, encodingUsing, index, count);

            // Assert - Precise Observable Outcome (AI Verifiable)
            // Note: We cannot mock Encoding.Default directly. We assert based on its expected behavior.
            Assert.Equal(Encoding.Default.GetString(input), result);
        }
    }
}