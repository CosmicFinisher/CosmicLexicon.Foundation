using System;
using CosmicLexicon.Foundation.Structures;
using Xunit;

namespace CosmicLexicon.Foundation.Structures
{
    public class ConcurrentObjectPoolTTests
    {
        [Fact]
        public void GenerateObjectWithValidGeneratorReturnsObject()
        {
            // Arrange
            ObjectGenerator generator = () => new object();
            var pool = new ConcurrentObjectPool<object>(generator);

            // Act
            var obj = pool.GenerateObject();

            // Assert
            Assert.NotNull(obj);
        }

        [Fact]
        public void GenerateObjectWithInvalidGeneratorThrowsException()
        {
            // Arrange
            ObjectGenerator generator = () => null;
            var pool = new ConcurrentObjectPool<object>(generator);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => pool.GenerateObject());
        }

        [Fact]
        public void NullGeneratorThrowsArgumentNullException()
        {
            // Arrange
            ObjectGenerator? generator = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ConcurrentObjectPool<object>(generator!));
        }

        [Fact]
        public void DisposeWithDisposableObjectsDisposesAll()
        {
            // Arrange
            var disposableObj = new DisposableObject();
            ObjectGenerator generator = () => disposableObj;
            var pool = new ConcurrentObjectPool<object>(generator);
            pool.Seed(1);

            // Act
            pool.Dispose();

            // Assert
            Assert.True(disposableObj.IsDisposed);
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