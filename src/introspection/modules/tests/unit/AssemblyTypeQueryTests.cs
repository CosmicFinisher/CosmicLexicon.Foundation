using CosmicLexicon.Foundation.Introspection.Modules;
using Xunit;

namespace CosmicLexicon.Foundation.Structures
{
    public class AssemblyTypeQueryTests
    {
        [Fact]
        public void AllQueryHasCorrectValue()
        {
            // Act
            AssemblyTypeCollectionQuery query = AssemblyTypeCollectionQuery.All;

            // Assert
            Assert.Equal(0, (int)query);
        }

        [Fact]
        public void ExportedQueryHasCorrectValue()
        {
            // Act
            AssemblyTypeCollectionQuery query = AssemblyTypeCollectionQuery.Exported;

            // Assert
            Assert.Equal(1, (int)query);
        }

        [Fact]
        public void NonPublicQueryHasCorrectValue()
        {
            // Act
            AssemblyTypeCollectionQuery query = AssemblyTypeCollectionQuery.NonPublic;

            // Assert
            Assert.Equal(2, (int)query);
        }

        [Fact]
        public void PublicQueryHasCorrectValue()
        {
            // Act
            AssemblyTypeCollectionQuery query = AssemblyTypeCollectionQuery.Public;

            // Assert
            Assert.Equal(3, (int)query);
        }

        [Fact]
        public void NestedQueryHasCorrectValue()
        {
            // Act
            AssemblyTypeCollectionQuery query = AssemblyTypeCollectionQuery.Nested;

            // Assert
            Assert.Equal(4, (int)query);
        }
    }
}