using OpenEchoSystem.Core.xText.xString;
using Xunit;

namespace OpenEchoSystem.Core.xText
{
    public class StringCaseTests
    {
        [Fact]
        public void StringCaseValuesAreCorrect()
        {
            Assert.Equal(0, (int)StringCase.Lower);
            Assert.Equal(1, (int)StringCase.Upper);
            Assert.Equal(2, (int)StringCase.Title);
            Assert.Equal(3, (int)StringCase.Invert);
        }
    }
}