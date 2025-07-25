using System;
using System.Linq;
using Xunit;
using OpenEchoSystem.Core.xCollections.Enums;

namespace OpenEchoSystem.Core.xCollections.Enums
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
        public void MakeArray_TReturn_ReturnsArrayOfCorrectSize()
        {
            // Act
            int[] result = EnumHelper.MakeArray<TestEnum, int>();

            // Assert
            Assert.Equal(3, result.Length);
        }

        [Fact]
        public void MakeArray_ReturnsArrayOfCorrectSize()
        {
            // Act
            TestEnum[] result = EnumHelper.MakeArray<TestEnum>();

            // Assert
            Assert.Equal(3, result.Length);
        }
    }
}
