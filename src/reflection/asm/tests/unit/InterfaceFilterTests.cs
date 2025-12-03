using Xunit;
using CosmicLexicon.Foundation.xReflection.xAssembly;
using CosmicLexicon.Foundation.xReflection.xAssembly;

namespace CosmicLexicon.Foundation.xReflection.xAssembly.Tests
{
    public class InterfaceFilterTests
    {
        [Fact]
        public void InterfaceFilterNoneHasCorrectValue()
        {
            // Act
            InterfaceFilter filter = InterfaceFilter.None;

            // Assert
            Assert.Equal(0, (int)filter);
        }

        [Fact]
        public void InterfaceFilterNotInheritedFromInterfacesHasCorrectValue()
        {
            // Act
            InterfaceFilter filter = InterfaceFilter.NotInheritedFromInterfaces;

            // Assert
            Assert.Equal(1, (int)filter);
        }

        [Fact]
        public void InterfaceFilterNotExclusiveInterfacesHasCorrectValue()
        {
            // Act
            InterfaceFilter filter = InterfaceFilter.NotExclusiveInterfaces;

            // Assert
            Assert.Equal(2, (int)filter);
        }

        [Fact]
        public void InterfaceFilterInheritedFromInterfaceHasCorrectValue()
        {
            // Act
            InterfaceFilter filter = InterfaceFilter.InheritedFromInterface;

            // Assert
            Assert.Equal(3, (int)filter);
        }
    }
}