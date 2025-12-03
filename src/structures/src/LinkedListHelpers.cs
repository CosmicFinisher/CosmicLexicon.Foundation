using System.Linq.Expressions;

namespace CosmicLexicon.Foundation.Structures
{
    public static class LinkedListHelpers
    {
        public static List<TOut> ToList<TOut>(TOut startNode, Expression<Func<TOut, TOut?>> next)
        {
            ArgumentNullException.ThrowIfNull(next);
            var nextNodes = new List<TOut>();
            var compiledNext = next.Compile();

            var currentNode = startNode;
            var visitedNodes = new HashSet<TOut>();
            while (currentNode != null)
            {
                if (!visitedNodes.Add(currentNode))
                {
                    // Circular reference detected
                    break;
                }
                nextNodes.Add(currentNode);
                try
                {
                    currentNode = compiledNext.Invoke(currentNode);
                }
                catch (Exception ex)
                {
                     // ignored
                }
                if (currentNode == null)
                {
                    break;
                }
            }

            return nextNodes;
        }
    }
}
