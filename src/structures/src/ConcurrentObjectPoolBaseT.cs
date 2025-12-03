using System.Collections.Concurrent;

namespace CosmicLexicon.Foundation.Structures
{
    public abstract class ConcurrentObjectPoolBase<T> : IConcurrentObjectPool<T>, IDisposable
        where T : class
    {
        protected ConcurrentObjectPoolBase()
        {
        }


        public void Seed(int seedSize)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(seedSize);
            for (int i = 0; i < seedSize; i++)
            {
                _objects.Add(GenerateObject());
            }

        }

        private readonly ConcurrentBag<T> _objects = [];
        public abstract T GenerateObject();
        public virtual T Rent()
        {
            if (_objects.TryTake(out var item))
            {
                return item;
            }
            return GenerateObject();
        }
        public virtual void ReturnToPool(T returnObject)
        {
            ArgumentNullException.ThrowIfNull(returnObject);
            _objects.Add(returnObject);
        }

        public void Dispose()
        {
            while (_objects.TryTake(out var item))
            {
                if (item is IDisposable disposable)
                {
                    try
                    {
                        disposable.Dispose();
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
        }
    }
}