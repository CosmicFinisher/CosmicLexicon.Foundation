using System.Reflection;

namespace CosmicLexicon.Foundation.Introspection.Modules.UnitTest
{
    public class AssemblyExtensionsTests
    {
        [Fact]
        public void QueryTypesAllReturnsAllDefinedTypes()
        {
            // Arrange
            Assembly assembly = typeof(AssemblyExtensionsTests).Assembly;

            // Act
            IEnumerable<Type> result = assembly.QueryTypes(AssemblyTypeCollectionQuery.All);

            // Assert
            Assert.Contains(typeof(AssemblyExtensionsTests), result);
        }

        [Fact]
        public void GetTypesAllReturnsAllTypes()
        {
            // Arrange
            Assembly assembly = typeof(AssemblyExtensionsTests).Assembly;

            // Act
            IEnumerable<Type> result = assembly.GetTypes(AssemblyTypeQuery.All);

            // Assert
            Assert.Contains(typeof(AssemblyExtensionsTests), result);
        }

        [Fact]
        public void FilterTypesByInterfaceValidInterfaceReturnsFilteredTypes()
        {
            // Arrange
            Assembly assembly = typeof(AssemblyExtensionsTests).Assembly;
            Type interfaceType = typeof(ITestInterface);

            // Act
            IEnumerable<Type> result = assembly.FilterTypesByInterface(interfaceType, AssemblyTypeQuery.All);

            // Assert
            Assert.Contains(typeof(TestClassImplementingInterface), result);
            Assert.DoesNotContain(typeof(AssemblyExtensionsTests), result);
        }

        [Fact]
        public void FilterTypesByBaseTypeValidBaseClassReturnsFilteredTypes()
        {
            // Arrange
            Assembly assembly = typeof(AssemblyExtensionsTests).Assembly;
            Type baseClassType = typeof(BaseTestClass);

            // Act
            IEnumerable<Type> result = assembly.FilterTypesByBaseType(baseClassType, AssemblyTypeQuery.All);

            // Assert
            Assert.Contains(typeof(TestClassInheritingFromBase), result);
            Assert.DoesNotContain(typeof(AssemblyExtensionsTests), result);
        }

        [Fact]
        public void CompareTypeDefenitionSameTypeReturnsTrue()
        {
            // Arrange
            Type type1 = typeof(string);
            Type type2 = typeof(string);

            // Act
            bool result = type1.CompareTypeDefenition(type2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetTypeDefenitionIfItIsGenericGenericTypeReturnsGenericTypeDefinition()
        {
            // Arrange
            Type type = typeof(List<string>);

            // Act
            Type result = type.GetTypeDefenitionIfItIsGeneric();

            // Assert
            Assert.Equal(typeof(List<>), result);
        }

        public interface ITestInterface { }
        public class TestClassImplementingInterface : ITestInterface { }
        public class BaseTestClass { }
        public class TestClassInheritingFromBase : BaseTestClass { }
    }
}
