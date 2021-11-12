using FluentAssertions;

using Xunit;

namespace HighlightMarker.Tests
{
    public class RangeTests
    {
        [Fact]
        public void ShouldHaveEmptyRange()
        {
            // Arrange.
            var range = new Range();

            // Act.
            var isEmpty = range.IsEmpty();

            // Assert.
            isEmpty.Should().BeTrue();
        }

        [Fact]
        public void ShouldNotHaveEmptyRange()
        {
            // Arrange.
            var range = new Range(0, 1);

            // Act.
            var isEmpty = range.IsEmpty();

            // Assert.
            isEmpty.Should().BeFalse();
        }

        [Fact]
        public void ShouldBeValidRange()
        {
            // Arrange.
            var range = new Range(0, 1);

            // Act.
            var isValid = range.IsValid();

            // Assert.
            isValid.Should().BeTrue();
            range.LowerBound.Should().Be(0);
            range.UpperBound.Should().Be(1);
        }

        [Fact]
        public void ShouldNotBeValidRange()
        {
            // Arrange.
            var range = new Range(0, -1);

            // Act.
            var isValid = range.IsValid();

            // Assert.
            isValid.Should().BeFalse();
        }

        [Fact]
        public void ShouldBeInsideRange()
        {
            // Arrange.
            var bigRange = new Range(0, 3);
            var smallRange = new Range(1, 2);

            // Act.
            var isInsideRange = smallRange.IsInsideRange(bigRange);

            // Assert.
            isInsideRange.Should().BeTrue();
        }

        [Fact]
        public void ShouldNotBeInsideRange()
        {
            // Arrange.
            var bigRange = new Range(0, 3);
            var smallRange = new Range(1, 2);

            // Act.
            var isInsideRange = bigRange.IsInsideRange(smallRange);

            // Assert.
            isInsideRange.Should().BeFalse();
        }
    }
}