namespace CosmicLexicon.Foundation.Introspection.Modules
{
    public static class GenericTypeExtensions
    {
        extension<T>(T value)
        {
            public bool IsDefault() =>
        EqualityComparer<T>.Default.Equals(value, default!);
        }
    }
}