using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HighlightMarker
{
    public class HighlightMarker : IEnumerable<HighlightIndex>
    {
        private readonly IHighlightProcessor highlightProcessor;

        private IList<Range> index;
        private string searchText;
        private char[] searchTextDelimiters = { ' ' };

        public HighlightMarker(string fullText, string searchText, IHighlightProcessor highlightProcessor = null, char[] searchTextDelimiters = null)
        {
            this.index = new List<Range>();
            this.highlightProcessor = highlightProcessor;

            this.FullText = fullText;
            this.SearchText = searchText;

            if (searchTextDelimiters != null && searchTextDelimiters.Any())
            {
                this.SearchTextDelimiters = searchTextDelimiters;
            }
        }

        public string FullText { get; private set; }

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
            if (!this.index.Any())
            {
                yield return new HighlightIndex(0, this.FullText.Length, false);
                yield break;
            }

            IEnumerable<int> realRange = new List<int>();
            for (var i = 0; i < this.index.Count; i++)
            {
                var range = this.index[i];
                realRange = realRange.Union(Enumerable.Range(range.LowerBound, range.UpperBound - range.LowerBound));
            }

            var consecutiveGroups = realRange.OrderBy(x => x).ToConsecutiveGroups();
            var consecutiveRanges = new List<Range>();
            foreach (var group in consecutiveGroups)
            {
                consecutiveRanges.Add(new Range(group.Min(), group.Max()));
            }

            var lastItem = new { LowerBound = 0, UpperBound = 0, IsHighlighted = false };
            foreach (var currentItem in consecutiveRanges.OrderBy(x => x.LowerBound).ThenBy(y => y.UpperBound))
            {
                if (currentItem.LowerBound > lastItem.UpperBound)
                {
                    yield return new HighlightIndex(lastItem.UpperBound, currentItem.LowerBound - lastItem.UpperBound, false);
                }

                yield return new HighlightIndex(currentItem.LowerBound, currentItem.UpperBound - currentItem.LowerBound + 1, true);
                lastItem = new { LowerBound = currentItem.LowerBound + 1, UpperBound = currentItem.UpperBound + 1, IsHighlighted = true };
            }

            if (this.index.Max(x => x.UpperBound) < this.FullText.Length)
            {
                yield return new HighlightIndex(lastItem.UpperBound, this.FullText.Length - lastItem.UpperBound, false);
            }
        }

        private void UpdateIndex()
        {
            this.index = CreateIndex(this.FullText, this.SearchText, this.highlightProcessor, this.SearchTextDelimiters);
        }

        private static IList<Range> CreateIndex(string fulltext, string searchtext, IHighlightProcessor processor, char[] delimiters)
        {
            if (string.IsNullOrEmpty(searchtext))
            {
                return Enumerable.Empty<Range>().ToList();
            }

            var index = new List<Range>();
            var searchStrings = searchtext.Trim().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            var fullTextItems = new List<string>();

            // Process fulltext using the configured highlight processor(s)
            if (processor == null)
            {
                fullTextItems.Add(fulltext);
            }
            else
            {
                var processedStrings = processor.GetFulltextItems(fulltext);
                if (processedStrings != null && processedStrings.Length > 0)
                {
                    fullTextItems.AddRange(processedStrings);
                }
            }

            // Build highlighting index
            foreach (var searchString in searchStrings)
            {
                var length = searchString.Length;

                foreach (var fullTextItem in fullTextItems.Distinct())
                {
                    var searchStringIndex = fullTextItem.IndexOf(searchString, 0, StringComparison.CurrentCultureIgnoreCase);

                    while (searchStringIndex >= 0)
                    {
                        index.Add(new Range(searchStringIndex, searchStringIndex + length));

                        var lastIndex = searchStringIndex + length;
                        searchStringIndex = fullTextItem.IndexOf(searchString, lastIndex, StringComparison.CurrentCultureIgnoreCase);
                    }
                }
            }

            return index;
        }
    }
}
