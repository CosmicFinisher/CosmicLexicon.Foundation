using System.Collections.Generic;

namespace CosmicLexicon.Foundation.xRuntime
{
    using System;

    public sealed class GenericComparer<TData> : IComparer<TData>
    {
        public static GenericComparer<TData> Comparer { get; } = new GenericComparer<TData>();

        public int Compare(TData x, TData y)
        {
            return Comparer<TData>.Default.Compare(x, y);
        }
    }
}
