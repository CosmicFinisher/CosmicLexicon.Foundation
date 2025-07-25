using OpenEchoSystem.Core.xCollections;

namespace OpenEchoSystem.Core.xCollections
{

    public partial class ConcurrentObjectPoolBaseTests
    {
        public class ConcurrentObjectPoolTests
        {
            [Fact]
            public void GenerateObject_ReturnsObjectFromFactory()
            {
                // Arrange
                var pool = new ConcurrentObjectPool<object>(() => new object());

                // Act
                var obj = pool.GenerateObject();

                // Assert
                Assert.NotNull(obj);
            }

            [Fact]
            public void GenerateObject_ThrowsExceptionWhenFactoryReturnsNull()
            {
                // Arrange
                var pool = new ConcurrentObjectPool<object>(() => null);

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => pool.GenerateObject());
            }

            [Fact]
            public void GenerateObject_ThrowsExceptionWhenFactoryReturnsWrongType()
            {
                // Arrange
                var pool = new ConcurrentObjectPool<string>(() => 1);

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => pool.GenerateObject());
            }
        }
    }
}
