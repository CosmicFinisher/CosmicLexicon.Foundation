namespace CosmicLexicon.Foundation.Structures
{
    /// <summary>
    /// Defines the basic contract for a concurrent object pool.
    /// </summary>
    public interface IConcurrentObjectPool
    {
        /// <summary>
        /// Seeds the object pool with a specified number of objects.
        /// </summary>
        /// <param name="seedSize">The number of objects to add to the pool.</param>
        void Seed(int seedSize);
    }

    /// <summary>
    /// Defines the contract for a concurrent object pool that manages objects of type T.
    /// </summary>
    /// <typeparam name="T">The type of objects to manage in the pool.</typeparam>
    public interface IConcurrentObjectPool<T> : IConcurrentObjectPool
       where T : class
    {
        /// <summary>
        /// Rents an object from the pool.
        /// </summary>
        /// <returns>An object from the pool.</returns>
        T Rent();

        /// <summary>
        /// Returns an object to the pool.
        /// </summary>
        /// <param name="returnObject">The object to return to the pool.</param>
        void ReturnToPool(T returnObject);
    }
}