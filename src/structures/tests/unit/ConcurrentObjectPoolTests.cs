namespace CosmicLexicon.Foundation.Structures
{

    public partial class ConcurrentObjectPoolBaseTests
    {
        public class ConcurrentObjectPoolTests
        {
            [Fact]
            public void GenerateObjectReturnsObjectFromFactory()
            {
                // Arrange
                var pool = new ConcurrentObjectPool<object>(() => new object());

                // Act
                var obj = pool.GenerateObject();

                // Assert
                Assert.NotNull(obj);
            }

            [Fact]
            public void GenerateObjectThrowsExceptionWhenFactoryReturnsNull()
            {
                // Arrange
                var pool = new ConcurrentObjectPool<object>(() => null);

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => pool.GenerateObject());
            }

            [Fact]
            public void GenerateObjectThrowsExceptionWhenFactoryReturnsWrongType()
            {
                // Arrange
                var pool = new ConcurrentObjectPool<string>(() => 1);

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => pool.GenerateObject());
            }
        }
    }
}
