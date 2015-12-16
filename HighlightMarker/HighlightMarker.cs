using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HighlightMarker
{
    public class HighlightMarker : IEnumerable<HighlightIndex>
    {
        private string searchText;
        private char[] searchTextDelimiters = {' '};

        public HighlightMarker(string fullText, string searchText, char[] searchTextDelimiters = null)
        {
            this.Index = new List<Range>();
            this.FullText = fullText;
            this.SearchText = searchText;
            
            if (searchTextDelimiters != null && searchTextDelimiters.Any())
            {
                this.SearchTextDelimiters = searchTextDelimiters;
            }
        }

        public string FullText { get; private set; }

        public IList<Range> Index { get; private set; }

        public string SearchText
        {
            get
            {
                return this.searchText;
            }

            set
            {
                if (value != this.searchText)
                {
                    this.searchText = value;
                    this.UpdateIndex();
                }
            }
        }

        public char[] SearchTextDelimiters
        {
            get
            {
                return this.searchTextDelimiters;
            }

            set
            {
                if (value != this.searchTextDelimiters)
                {
                    this.searchTextDelimiters = value;
                    this.UpdateIndex();
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        ///     Gets the enumerator.
        ///     The enumerator returns all HighlightIndex items which indicate at which position in string FullText
        ///     parts of the given SearchText were found and how many characters long they were.
        /// </summary>
        /// <returns>The IEnumerator&lt;HighlightIndex&gt;.</returns>
        public IEnumerator<HighlightIndex> GetEnumerator()
        {
            if (!this.Index.Any())
            {
                yield return new HighlightIndex(0, this.FullText.Length, false);
                yield break;
            }

            IEnumerable<int> realRange = new List<int>();
            for (int index = 0; index < this.Index.Count; index++)
            {
                Range i = this.Index[index];
                realRange = realRange.Union(Enumerable.Range(i.LowerBound, i.UpperBound - i.LowerBound));
            }

            IEnumerable<IList<int>> consecutiveGroups = realRange.OrderBy(x => x).ToConsecutiveGroups();
            IList<Range> consecutiveRanges = new List<Range>();
            foreach (var group in consecutiveGroups)
            {
                consecutiveRanges.Add(new Range(group.Min(), group.Max()));
            }

            var lastItem = new Tuple<Range, bool>(new Range(0, 0), false);
            foreach (Range currentItem in consecutiveRanges.OrderBy(x => x.LowerBound).ThenBy(y => y.UpperBound))
            {
                if (currentItem.LowerBound > lastItem.Item1.UpperBound)
                {
                    yield return new HighlightIndex(lastItem.Item1.UpperBound, currentItem.LowerBound - lastItem.Item1.UpperBound, false);
                }

                yield return new HighlightIndex(currentItem.LowerBound, currentItem.UpperBound - currentItem.LowerBound + 1, true);
                lastItem = new Tuple<Range, bool>(new Range(currentItem.LowerBound + 1, currentItem.UpperBound + 1), true);
            }

            if (this.Index.Max(x => x.UpperBound) < this.FullText.Length)
            {
                yield return new HighlightIndex(lastItem.Item1.UpperBound, this.FullText.Length - lastItem.Item1.UpperBound, false);
            }
        }

        private void UpdateIndex()
        {
            this.Index = CreateIndex(this.FullText, this.SearchText, this.SearchTextDelimiters);
        }

        private static IList<Range> CreateIndex(string fulltext, string searchtext, char[] delimiters)
        {
            if (string.IsNullOrEmpty(searchtext))
            {
                return Enumerable.Empty<Range>().ToList();
            }

            var index = new List<Range>();
            var searchStrings = searchtext.Trim().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            var fullTextItems = new List<string> { fulltext };

            foreach (string searchString in searchStrings)
            {
                int length = searchString.Length;

                foreach (var fullTextItem in fullTextItems)
                {
                    int searchStringIndex = fullTextItem.IndexOf(searchString, 0, StringComparison.CurrentCultureIgnoreCase);

                    while (searchStringIndex >= 0)
                    {
                        index.Add(new Range(searchStringIndex, searchStringIndex + length));

                        int lastIndex = searchStringIndex + length;
                        searchStringIndex = fullTextItem.IndexOf(searchString, lastIndex, StringComparison.CurrentCultureIgnoreCase);
                    }
                }
            }

            return index;
        }
    }
}