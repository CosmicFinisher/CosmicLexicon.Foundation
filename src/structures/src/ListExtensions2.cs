using System.ComponentModel;

namespace CosmicLexicon.Foundation.Structures
{
    [EditorBrowsable(EditorBrowsableState.Never)]

    public static class ListExtensions2
    {
        public static List<T> AddIfNotExists<T>(this List<T> list, IEnumerable<T> values)
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
        public static bool AddIfNotExists<T>(this List<T> list,
                                     T value)
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
