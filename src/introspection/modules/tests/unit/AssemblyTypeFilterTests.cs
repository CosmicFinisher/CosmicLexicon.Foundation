namespace CosmicLexicon.Foundation.Introspection.Modules.UnitTest
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