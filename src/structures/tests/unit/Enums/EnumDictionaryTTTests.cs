using CosmicLexicon.Foundation.Structures.Enums;

namespace CosmicLexicon.Foundation.Structures.UnitTest.Enums
{
    public class EnumDictionaryTTTests
    {
        private enum TestEnum
        {
            Value1,
            Value2,
            Value3
        }

        [Fact]
        public void ResetValuesResetsAllValuesToGivenValue()
        {
            // Arrange
            var dictionary = new EnumDictionary<TestEnum, int>(0);

            // Act
            dictionary.ResetValues(5);

            // Assert
            Assert.Equal(5, dictionary.Member(TestEnum.Value1));
            Assert.Equal(5, dictionary.Member(TestEnum.Value2));
            Assert.Equal(5, dictionary.Member(TestEnum.Value3));
        }

        [Fact]
        public void SetValueForSingleEnumSetsValueForGivenEnum()
        {
            // Arrange
            var dictionary = new EnumDictionary<TestEnum, int>(0);

            // Act
            dictionary.SetValueFor(TestEnum.Value2, 10);

            // Assert
            Assert.Equal(0, dictionary.Member(TestEnum.Value1));
            Assert.Equal(10, dictionary.Member(TestEnum.Value2));
            Assert.Equal(0, dictionary.Member(TestEnum.Value3));
        }

        [Fact]
        public void SetValueForEnumArraySetsValueForGivenEnums()
        {
            // Arrange
            var dictionary = new EnumDictionary<TestEnum, int>(0);

            // Act
            dictionary.SetValueFor(new TestEnum[] { TestEnum.Value1, TestEnum.Value3 }, 15);

            // Assert
            Assert.Equal(15, dictionary.Member(TestEnum.Value1));
            Assert.Equal(0, dictionary.Member(TestEnum.Value2));
            Assert.Equal(15, dictionary.Member(TestEnum.Value3));
        }

        [Fact]
        public void MemberReturnsValueForGivenEnum()
        {
            // Arrange
            var dictionary = new EnumDictionary<TestEnum, int>(0);
            dictionary.SetValueFor(TestEnum.Value2, 20);

            // Act
            int result = dictionary.Member(TestEnum.Value2);

            // Assert
            Assert.Equal(20, result);
        }

        [Fact]
        public void WhereValueReturnsEnumForGivenValue()
        {
            // Arrange
            var dictionary = new EnumDictionary<TestEnum, int>(0);
            dictionary.SetValueFor(TestEnum.Value1, 25);
            dictionary.SetValueFor(TestEnum.Value2, 25);
            dictionary.SetValueFor(TestEnum.Value3, 30);

            // Act
            TestEnum[] result = dictionary.WhereValues(25);

            // Assert
            Assert.Equal(new TestEnum[] { TestEnum.Value1, TestEnum.Value2 }, result);
        }

        [Fact]
        public void SelectValuesReturnsKeyValuePairArrayForGivenValues()
        {
            // Arrange
            var dictionary = new EnumDictionary<TestEnum, int>(0);
            dictionary.SetValueFor(TestEnum.Value1, 35);
            dictionary.SetValueFor(TestEnum.Value2, 40);
            dictionary.SetValueFor(TestEnum.Value3, 35);

            // Act
            var result = dictionary.SelectValues(35);

            // Assert
            Assert.Collection(result,
                pair => 
                {
                    Assert.Equal(TestEnum.Value1, pair.Key);
                    Assert.Equal(35, pair.Value);
                },
                pair =>
                {
                    Assert.Equal(TestEnum.Value3, pair.Key);
                    Assert.Equal(35, pair.Value);
                }
            );
        }
    }
}
