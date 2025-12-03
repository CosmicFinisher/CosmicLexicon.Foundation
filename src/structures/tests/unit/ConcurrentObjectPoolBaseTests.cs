namespace CosmicLexicon.Foundation.Structures
{

    public partial class ConcurrentObjectPoolBaseTests
    {
        [Fact]
        public void SeedAddsObjectsToPool()
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
        public void DisposeDisposesDisposableObjects()
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

            public void Dispose() => IsDisposed = true;
        }


        private class ConcreteObjectPool : ConcurrentObjectPoolBase<DisposableObject>
        {
            private readonly List<DisposableObject> _objects = [];
            private bool _disposed;

            public override DisposableObject GenerateObject()
            {
                var obj = new DisposableObject();
                _objects.Add(obj);
                return obj;
            }

            public int GetObjectsCount() => _objects.Count;

            public bool AreObjectsDisposed() => _objects.All(o => o.IsDisposed);
        }
    }
}
