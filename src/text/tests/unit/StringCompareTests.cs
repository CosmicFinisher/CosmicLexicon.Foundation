using OpenEchoSystem.Core.xText.xString;
using Xunit;

namespace OpenEchoSystem.Core.xText
{
    public class StringCompareTests
    {
        [Fact]
        public void StringCompareValuesAreCorrect()
        {
            Assert.Equal(0, (int)StringCompare.CreditCard);
            Assert.Equal(1, (int)StringCompare.Anagram);
            Assert.Equal(2, (int)StringCompare.Unicode);
        }
    }
}