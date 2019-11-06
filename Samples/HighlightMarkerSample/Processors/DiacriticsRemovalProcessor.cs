using Diacritics.Extensions;

using HighlightMarker;

namespace HighlightMarkerSample.Data.Processors
{
    public class DiacriticsRemovalProcessor : IHighlightProcessor
    {
        public string[] GetFulltextItems(string fulltext)
        {
            var fullTextWithoutDiacritics = fulltext.RemoveDiacritics();
            if (fullTextWithoutDiacritics != fulltext)
            {
                return new[] { fulltext, fullTextWithoutDiacritics };
            }

            return new[] { fulltext };
        }
    }
}