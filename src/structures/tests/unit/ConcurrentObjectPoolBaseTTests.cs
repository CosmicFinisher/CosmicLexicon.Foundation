namespace CosmicLexicon.Foundation.Structures.UnitTest
{
    public class ConcurrentObjectPoolBaseTTests
    {
        private class ConcreteObjectPool : ConcurrentObjectPoolBase<object>
        {
            public override object GenerateObject()
            {
                return new object();
            }
        }

        [Fact]
        public void SeedWithValidSeedSizeAddsObjectsToPool()
        {
            // Arrange
            var pool = new ConcreteObjectPool();
            int seedSize = 5;

            // Act
            pool.Seed(seedSize);

            // Assert
            for (int i = 0; i < seedSize; i++)
            {
                var obj = pool.Rent();
                Assert.NotNull(obj);
            }
        }

        [Fact]
        public void SeedWithNegativeSeedSizeThrowsException()
        {
            // Arrange
            var pool = new ConcreteObjectPool();
            int seedSize = -1;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => pool.Seed(seedSize));
        }

        [Fact]
        public void RentFromPoolWithObjectsReturnsObjectFromPool()
        {
            // Arrange
            var pool = new ConcreteObjectPool();
            pool.Seed(1);

            // Act
            var obj = pool.Rent();

            // Assert
            Assert.NotNull(obj);
        }

        [Fact]
        public void RentFromEmptyPoolReturnsNewObject()
        {
            // Arrange
            var pool = new ConcreteObjectPool();

            // Act
            var obj = pool.Rent();

            // Assert
            Assert.NotNull(obj);
        }

        [Fact]
        public void ReturnValidObjectToPoolAddsObject()
        {
            // Arrange
            var pool = new ConcreteObjectPool();
            var obj = new object();

            // Act
            pool.ReturnToPool(obj);
            var rentedObj = pool.Rent();

            // Assert
            Assert.Equal(obj, rentedObj);
        }

        [Fact]
        public void ReturnNullObjectToPoolThrowsException()
        {
            // Arrange
            var pool = new ConcreteObjectPool();
            object? obj = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => pool.ReturnToPool(obj!));
        }

        [Fact]
        public void DisposeWithDisposableObjectsDisposesObjects()
        {
            // Arrange
            var pool = new ConcreteObjectPool();
            var disposableObj = new DisposableObject();
            pool.ReturnToPool(disposableObj);

            // Act
            pool.Dispose();

            // Assert
            Assert.True(disposableObj.IsDisposed);
        }

        [Fact]
        public void DisposeWithNonDisposableObjectsDoesNotThrow()
        {
            // Arrange
            var pool = new ConcreteObjectPool();
            pool.ReturnToPool(new object());

            // Act & Assert
            var exception = Record.Exception(() => pool.Dispose());
            Assert.Null(exception);
        }

        private class DisposableObject : IDisposable
        {
            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }
    }
}