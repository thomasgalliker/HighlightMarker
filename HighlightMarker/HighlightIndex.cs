using System;
using System.Diagnostics;

namespace HighlightMarker
{
    [DebuggerDisplay("FromIndex = {FromIndex}, Length = {Length}, IsHighlighted = {IsHighlighted}")]
    public struct HighlightIndex : IEquatable<HighlightIndex>
    {
        public HighlightIndex(int fromIndex, int length, bool isHighlighted)
        {
            this.FromIndex = fromIndex;
            this.Length = length;
            this.IsHighlighted = isHighlighted;
        }

        public readonly int FromIndex;

        public readonly bool IsHighlighted;

        public readonly int Length;

        public bool Equals(HighlightIndex other)
        {
            return other.FromIndex == this.FromIndex &&
                   other.Length == this.Length &&
                   other.IsHighlighted == this.IsHighlighted;
        }

        public override bool Equals(object other)
        {
            if (!(other is HighlightIndex))
            {
                return false;
            }

            return this.Equals((HighlightIndex)other);
        }

        public override int GetHashCode()
        {
            return
                this.FromIndex.GetHashCode() ^
                this.Length.GetHashCode() ^
                this.IsHighlighted.GetHashCode();
        }

        public static bool operator ==(HighlightIndex a, HighlightIndex b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(HighlightIndex a, HighlightIndex b)
        {
            return !a.Equals(b);
        }
    }
}