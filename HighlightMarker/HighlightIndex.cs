using System.Diagnostics;

namespace HighlightMarker
{
    [DebuggerDisplay("FromIndex = {FromIndex}, Length = {Length}, IsHighlighted = {IsHighlighted}")]
    public class HighlightIndex // TODO GATH: Could be come a struct for performance reasons
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HighlightIndex" /> class.
        /// </summary>
        /// <param name="fromIndex">The from index.</param>
        /// <param name="length">The length.</param>
        /// <param name="isHighlighted">The is highlighted.</param>
        public HighlightIndex(int fromIndex, int length, bool isHighlighted)
        {
            this.FromIndex = fromIndex;
            this.Length = length;
            this.IsHighlighted = isHighlighted;
        }

        /// <summary>
        ///     Gets the from index.
        /// </summary>
        /// <value>From index.</value>
        public int FromIndex { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether is highlighted.
        /// </summary>
        /// <value><c>true</c> if this instance is highlighted; otherwise, <c>false</c>.</value>
        public bool IsHighlighted { get; private set; }

        /// <summary>
        ///     Gets the length.
        /// </summary>
        /// <value>The length.</value>
        public int Length { get; private set; }
    }
}