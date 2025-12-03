using CosmicLexicon.Foundation.Structures;

public class ConcurrentObjectPoolInterfaceTests
                {
                    [Fact]
                    public void Seed_AddsObjectsToPool()
                    {
                        // Arrange
                        IConcurrentObjectPool pool = new ConcreteObjectPool();
                        int seedSize = 5;
            
                        // Act
                        pool.Seed(seedSize);
            
                        // Assert
                        Assert.Equal(seedSize, ((ConcreteObjectPool)pool).GetObjectsCount());
                    }
            
                    [Fact]
                    public void Rent_ReturnsObject()
                    {
                        // Arrange
                        IConcurrentObjectPool<object> pool = new ConcreteObjectPool();
                        pool.Seed(1);
            
                        // Act
                        var obj = pool.Rent();
            
                        // Assert
                        Assert.NotNull(obj);
                    }
            
                    [Fact]
                    public void ReturnToPool_AddsObjectToPool()
                    {
                        // Arrange
                        IConcurrentObjectPool<object> pool = new ConcreteObjectPool();
                        object obj = new object();
            
                        // Act
                        pool.ReturnToPool(obj);
            
                        // Assert
                        Assert.Equal(1, ((ConcreteObjectPool)pool).GetObjectsCount());
                    }
            
                    private class ConcreteObjectPool : IConcurrentObjectPool<object>
                    {
                        private readonly List<object> _objects = new List<object>();
            
                        public void Seed(int seedSize)
                        {
                            for (int i = 0; i < seedSize; i++)
                            {
                                _objects.Add(new object());
                            }
                        }
            
                        public object Rent()
                        {
                            if (_objects.Count > 0)
                            {
                                var obj = _objects[0];
                                _objects.RemoveAt(0);
                                return obj;
                            }
                            return new object();
                        }
            
                        public void ReturnToPool(object returnObject)
                        {
                            _objects.Add(returnObject);
                        }
            
                        public int GetObjectsCount()
                        {
                            return _objects.Count;
                        }
                    }
                }
