using CosmicLexicon.Foundation.Structures;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

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
