using System.ComponentModel;

namespace CosmicLexicon.Foundation.xGenerics
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ObjectExtensions
    {
        public static bool IsNullOrDbNull(object obj)
            => obj == null || obj == DBNull.Value;
    }
}