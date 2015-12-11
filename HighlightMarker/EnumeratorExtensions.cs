using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HighlightMarker
{
    public static class EnumeratorExtensions
    {
        public static IEnumerable<T> Cast<T>(this IEnumerator iterator)
        {
            while (iterator.MoveNext())
            {
                yield return (T)iterator.Current;
            }
        }

        public static IEnumerable<T> ToList<T>(this IEnumerator iterator)
        {
            return iterator.Cast<T>().ToList();
        }
    }
}
