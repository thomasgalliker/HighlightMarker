using System.Collections.Generic;
using System.Linq;

namespace HighlightMarker
{
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Transforms an IEnumerable into groups of integer lists which are consecutivly ascending.
        ///     Please make sure that it - depending on the application of this method - is neccessary to pre-sort the IEnumerable
        ///     in order to retrieve the right groups.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The groups of consecutive integer lists.</returns>
        public static IEnumerable<List<int>> ToConsecutiveGroups(this IEnumerable<int> source)
        {
            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                {
                    yield break;
                }
                else
                {
                    int current = iterator.Current;
                    var group = new List<int> { current };

                    while (iterator.MoveNext())
                    {
                        int next = iterator.Current;
                        if (next < current || current + 1 < next)
                        {
                            yield return group;
                            group = new List<int>();
                        }

                        current = next;
                        group.Add(current);
                    }

                    if (group.Any())
                    {
                        yield return group;
                    }
                }
            }
        }
    }
}