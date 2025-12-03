using System;
using System.Linq;
using Xunit;
using CosmicLexicon.Foundation.Structures.Enums;

namespace CosmicLexicon.Foundation.Structures.Enums
{
    public class EnumHelperTests
    {
        private enum TestEnum
        {
            Value1,
            Value2,
            Value3
        }

        [Fact]
        public void MakeArrayTReturnReturnsArrayOfCorrectSize()
        {
            // Act
            int[] result = EnumHelper.MakeArray<TestEnum, int>();

            // Assert
            Assert.Equal(3, result.Length);
        }

        [Fact]
        public void MakeArrayReturnsArrayOfCorrectSize()
        {
            // Act
            TestEnum[] result = EnumHelper.MakeArray<TestEnum>();

            // Assert
            Assert.Equal(3, result.Length);
        }
    }
}
