using System;
using CosmicLexicon.Foundation.Structures;
using Xunit;

namespace CosmicLexicon.Foundation.Structures
{
    public class IConcurrentObjectPoolObjectPoolTests
    {
        private class ConcreteObjectPool : IConcurrentObjectPool<object>
        {
            private int _seedSize;
            private object? _rentedObject;
            private object? _returnedObject;

            public ConcreteObjectPool()
            {
                _rentedObject = new object();
            }

            public void Seed(int seedSize)
            {
                _seedSize = seedSize;
            }

            public object Rent()
            {
                return _rentedObject ?? new object();
            }

            public void ReturnToPool(object returnObject)
            {
                _returnedObject = returnObject;
            }

            public int GetSeedSize()
            {
                return _seedSize;
            }

            public object GetRentedObject()
            {
                return _rentedObject ?? new object();
            }

            public object? GetReturnedObject()
            {
                return _returnedObject;
            }
        }

        [Fact]
        public void SeedWithValidSeedSizeSetsSeedSize()
        {
            // Arrange
            var pool = new ConcreteObjectPool();
            int seedSize = 5;

            // Act
            pool.Seed(seedSize);

            // Assert
            Assert.Equal(seedSize, pool.GetSeedSize());
        }

        [Fact]
        public void RentReturnsRentedObject()
        {
            // Arrange
            var pool = new ConcreteObjectPool();
            var rentedObject = pool.Rent();

            // Act
            var rentedAgain = pool.Rent();

            // Assert
            Assert.Same(rentedObject, rentedAgain);
        }

        [Fact]
        public void ReturnToPoolSavesReturnedObject()
        {
            // Arrange
            var pool = new ConcreteObjectPool();
            var expectedObject = new object();

            // Act
            pool.ReturnToPool(expectedObject);

            // Assert
            Assert.Same(expectedObject, pool.GetReturnedObject());
        }
    }
}