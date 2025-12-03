using System.ComponentModel;

namespace CosmicLexicon.Foundation.Structures
{
    [EditorBrowsable(EditorBrowsableState.Never)]

    public static class ListExtensions2
    {
        extension<T>(List<T> list)
        {
            public List<T> AddIfNotExists(IEnumerable<T> values)
            {
                var set = new HashSet<T>(list);
                foreach (var value in values)
                {
                    if (set.Add(value))
                    {
                        list.Add(value);
                    }
                }
                return list;
            }
            public bool AddIfNotExists(T value)
            {
                var set = new HashSet<T>(list);
                if (set.Add(value))
                {
                    list.Add(value);
                    return true;
                }
                return false;
            }
        }
    }
}
