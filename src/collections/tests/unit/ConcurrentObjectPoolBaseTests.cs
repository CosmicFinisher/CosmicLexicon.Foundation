using OpenEchoSystem.Core.xCollections;
using OpenEchoSystem.Core.xCollections.Extensions;
using System;
using Xunit;

namespace OpenEchoSystem.Core.xCollections
{

    public partial class ConcurrentObjectPoolBaseTests
    {
        [Fact]
        public void Seed_AddsObjectsToPool()
        {
            // Arrange
            var pool = new ConcreteObjectPool();
            int seedSize = 5;

            // Act
            pool.Seed(seedSize);

            // Assert
            Assert.Equal(seedSize, pool.GetObjectsCount());
        }

        [Fact]
        public void Dispose_DisposesDisposableObjects()
        {
            // Arrange
            var pool = new ConcreteObjectPool();
            pool.Seed(5);

            // Act
            pool.Dispose();

            // Assert
            Assert.True(pool.AreObjectsDisposed());
        }
        private class DisposableObject : IDisposable
        {
            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }


        private class ConcreteObjectPool : ConcurrentObjectPoolBase<DisposableObject>
        {
            private readonly List<DisposableObject> _objects = new List<DisposableObject>();
            private bool _disposed;

            public override DisposableObject GenerateObject()
            {
                var obj = new DisposableObject();
                _objects.Add(obj);
                return obj;
            }

            public int GetObjectsCount()
            {
                return _objects.Count;
            }

            public bool AreObjectsDisposed()
            {
                return _objects.All(o => o.IsDisposed);
            }
        }
    }
}
