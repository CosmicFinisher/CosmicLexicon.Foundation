using CosmicLexicon.Foundation.Introspection.Modules;
using Xunit;

namespace CosmicLexicon.Foundation.Structures
{
    public class AssemblyTypeFilterTests
    {
        [Fact]
        public void FilterClassesHasCorrectValue()
        {
            // Act
            AssemblyTypeFilter filter = AssemblyTypeFilter.FilterClasses;

            // Assert
            Assert.Equal(0, (int)filter);
        }

        [Fact]
        public void FilterInterfacesHasCorrectValue()
        {
            // Act
            AssemblyTypeFilter filter = AssemblyTypeFilter.FilterInterfaces;

            // Assert
            Assert.Equal(1, (int)filter);
        }
    }
}