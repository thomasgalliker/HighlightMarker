using System;

namespace HighlightMarker
{
    internal class Range
    {
        public Range()
        {
        }

        public Range(int lowerBound, int upperBound)
        {
            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
        }

        /// <summary>
        ///     LowerBound value of the range
        /// </summary>
        public int LowerBound { get; set; }

        /// <summary>
        ///     UpperBound value of the range
        /// </summary>
        public int UpperBound { get; set; }

        /// <summary>
        ///     Presents the Range in readable format
        /// </summary>
        /// <returns>String representation of the Range</returns>
        public override string ToString()
        {
            return String.Format("[{0} - {1}]", this.LowerBound, this.UpperBound);
        }

        /// <summary>
        ///     Determines if the range is valid
        /// </summary>
        /// <returns>True if range is valid, else false</returns>
        public bool IsValid()
        {
            return this.LowerBound.CompareTo(this.UpperBound) <= 0;
        }

        /// <summary>
        ///     Determines if the provided value is inside the range
        /// </summary>
        /// <param name="value">The value to test</param>
        /// <returns>True if the value is inside Range, else false</returns>
        public bool ContainsValue(int value)
        {
            return (this.LowerBound.CompareTo(value) <= 0) && (value.CompareTo(this.UpperBound) <= 0);
        }

        /// <summary>
        ///     Determines if this Range is inside the bounds of another range
        /// </summary>
        /// <param name="range">The parent range to test on</param>
        /// <returns>True if range is inclusive, else false</returns>
        public bool IsInsideRange(Range range)
        {
            return this.IsValid() && range.IsValid() && range.ContainsValue(this.LowerBound) && range.ContainsValue(this.UpperBound);
        }

        /// <summary>
        ///     Determines if another range is inside the bounds of this range
        /// </summary>
        /// <param name="range">The child range to test</param>
        /// <returns>True if range is inside, else false</returns>
        public bool ContainsRange(Range range)
        {
            return this.IsValid() && range.IsValid() && this.ContainsValue(range.LowerBound) && this.ContainsValue(range.UpperBound);
        }

        public bool IsEmpty()
        {
            return this.LowerBound.CompareTo(default(int)) == 0 && this.UpperBound.CompareTo(default(int)) == 0;
        }
    }
}