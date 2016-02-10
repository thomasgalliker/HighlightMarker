namespace HighlightMarker
{
    /// <summary>
    /// You man want to use a HighlightProcessor to intercept the highlightable fulltext, 
    /// e.g. replace diacritic characters in the fulltext.
    /// </summary>
    public interface IHighlightProcessor
    {
        /// <summary>
        /// Processes the given <param name="fulltext">full test parameter</param>
        /// and returns an array of strings. All returned strings are subject to highlighting.
        /// </summary>
        /// <param name="fulltext">The original fulltext.</param>
        /// <returns></returns>
        string[] GetFulltextItems(string fulltext);
    }
}
