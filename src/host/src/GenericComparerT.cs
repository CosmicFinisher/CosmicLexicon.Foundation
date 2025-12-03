namespace CosmicLexicon.Foundation.Host
{
    public sealed class GenericComparer<TData> : IComparer<TData>
    {
        public static GenericComparer<TData> Comparer { get; } = new GenericComparer<TData>();

        public int Compare(TData x, TData y) => Comparer<TData>.Default.Compare(x, y);
    }
}
