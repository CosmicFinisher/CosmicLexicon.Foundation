using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicLexicon.Foundation.Structures
{
        public delegate object? ObjectGenerator();
    public sealed class ConcurrentObjectPool<T> : ConcurrentObjectPoolBase<T>
       where T : class
    {
        public ConcurrentObjectPool(ObjectGenerator objectGenerator) : base()
        {
            ArgumentNullException.ThrowIfNull(objectGenerator);
            ObjectGenerator = objectGenerator;
        }

        private ObjectGenerator ObjectGenerator { get; }

        /// <summary>
        /// Generates an object of type T using the provided object generator.
        /// </summary>
        /// <returns>An object of type T.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the generated object cannot be cast to type T.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the generated object is null.</exception>
        /// <remarks>
        /// The <see cref="ObjectGenerator"/> delegate is responsible for creating the object.
        /// Ensure that the delegate returns an object of the expected type T.
        ///
        /// **Security Considerations:**
        /// - The <see cref="ObjectGenerator"/> delegate can create any type of object, potentially leading to unexpected behavior or vulnerabilities. Restrict the types of objects that the delegate can create.
        /// - If the <see cref="ObjectGenerator"/> delegate uses external input to create the object, it could be vulnerable to injection attacks. Sanitize and validate any external input used by the delegate.
        /// - Implement a retry mechanism with a limited number of attempts to handle exceptions thrown by the <see cref="ObjectGenerator"/> delegate.
        /// </remarks>
        public override T GenerateObject()
        {
            var generatedObject = ObjectGenerator();
            ArgumentNullException.ThrowIfNull(generatedObject, $"Factory Result Object was Null. Expected Type {typeof(T).Name}");
            try
            {
                return (T)generatedObject;
            }
            catch (InvalidCastException)
            {
                throw new InvalidOperationException($"Failed To Cast Factory Result Object to {typeof(T).Name}");
            }
        }
    }
   
}
