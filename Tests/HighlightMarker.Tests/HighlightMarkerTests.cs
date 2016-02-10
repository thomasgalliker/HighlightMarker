using System.Linq;

using FluentAssertions;

using Xunit;

namespace HighlightMarker.Tests
{
    public class HighlightMarkerTests
    {
        [Fact]
        public void ShouldNotMarkIfEmptyString()
        {
            // Arrange.
            const string FullText = "full text for highlight marking";
            const string SearchText = "";
            var highlightMarker = new HighlightMarker(FullText, SearchText);

            // Act.
            var highlightList = highlightMarker.ToList();

            // Assert
            highlightList.Should().NotBeNull();
            highlightList.Should().HaveCount(1);

            AssertHighlightIndex(highlightList.ElementAt(0), fromIndex: 0, length: FullText.Length, isHighlighted: false);
        }

        [Fact]
        public void ShouldNotMarkIfSearchStringNotFound()
        {
            // Arrange.
            const string FullText = "";
            const string SearchText = "loremipsum";
            var highlightMarker = new HighlightMarker(FullText, SearchText);

            // Act.
            var highlightList = highlightMarker.ToList();

            // Assert
            highlightList.Should().NotBeNull();
            highlightList.Should().HaveCount(1);

            AssertHighlightIndex(highlightList.ElementAt(0), fromIndex: 0, length: 0, isHighlighted: false);
        }

        [Fact]
        public void ShouldMarkSingleWordInTheBeginning()
        {
            // Arrange
            const string FullText = "full text for highlight marking";
            const string SearchText = "full";
            var highlightMarker = new HighlightMarker(FullText, SearchText);

            // Act
            var highlightList = highlightMarker.ToList();

            // Assert
            highlightList.Should().NotBeNull();
            highlightList.Should().HaveCount(2);

            AssertHighlightIndex(highlightList.ElementAt(0), fromIndex: 0, length: 4, isHighlighted: true);
            AssertHighlightIndex(highlightList.ElementAt(1), fromIndex: 4, length: 27, isHighlighted: false);
        }

        [Fact]
        public void ShouldMarkSingleWordInBetween()
        {
            // Arrange
            const string FullText = "full text for highlight marking";
            const string SearchText = "highlight";
            var highlightMarker = new HighlightMarker(FullText, SearchText);

            // Act
            var highlightList = highlightMarker.ToList();

            // Assert
            highlightList.Should().NotBeNull();
            highlightList.Should().HaveCount(3);

            AssertHighlightIndex(highlightList.ElementAt(0), fromIndex: 0, length: 14, isHighlighted: false);
            AssertHighlightIndex(highlightList.ElementAt(1), fromIndex: 14, length: 9, isHighlighted: true);
            AssertHighlightIndex(highlightList.ElementAt(2), fromIndex: 23, length: 8, isHighlighted: false);
        }

        [Fact]
        public void ShouldMarkSingleWordInTheEnd()
        {
            // Arrange
            const string FullText = "full text for highlight marking";
            const string SearchText = "marking";
            var highlightMarker = new HighlightMarker(FullText, SearchText);

            // Act
            var highlightList = highlightMarker.ToList();

            // Assert
            highlightList.Should().NotBeNull();
            highlightList.Should().HaveCount(2);

            AssertHighlightIndex(highlightList.ElementAt(0), fromIndex: 0, length: 24, isHighlighted: false);
            AssertHighlightIndex(highlightList.ElementAt(1), fromIndex: 24, length: 7, isHighlighted: true);
        }

        [Fact]
        public void ShouldMarkSingleLetter()
        {
            // Arrange
            const string FullText = "full text for highlight marking";
            const string SearchText = "g";
            var highlightMarker = new HighlightMarker(FullText, SearchText);

            // Act
            var highlightList = highlightMarker.ToList();

            // Assert
            highlightList.Should().NotBeNull();
            highlightList.Should().HaveCount(6);

            AssertHighlightIndex(highlightList.ElementAt(0), fromIndex: 0, length: 16, isHighlighted: false);
            AssertHighlightIndex(highlightList.ElementAt(1), fromIndex: 16, length: 1, isHighlighted: true);
            AssertHighlightIndex(highlightList.ElementAt(2), fromIndex: 17, length: 3, isHighlighted: false);
            AssertHighlightIndex(highlightList.ElementAt(3), fromIndex: 20, length: 1, isHighlighted: true);
            AssertHighlightIndex(highlightList.ElementAt(4), fromIndex: 21, length: 9, isHighlighted: false);
            AssertHighlightIndex(highlightList.ElementAt(5), fromIndex: 30, length: 1, isHighlighted: true);
        }

        [Fact]
        public void ShouldSplitWithCustomDelimiters()
        {
            // Arrange
            const string FullText = "full text for highlight marking";
            const string SearchText = "full, for;king";
            var highlightMarker = new HighlightMarker(FullText, SearchText, highlightProcessor: null, searchTextDelimiters: new[] { ',', ';' });

            // Act
            var highlightList = highlightMarker.ToList();

            // Assert
            highlightList.Should().NotBeNull();
            highlightList.Should().HaveCount(5);

            AssertHighlightIndex(highlightList.ElementAt(0), fromIndex: 0, length: 4, isHighlighted: true);
            AssertHighlightIndex(highlightList.ElementAt(1), fromIndex: 4, length: 5, isHighlighted: false);
            AssertHighlightIndex(highlightList.ElementAt(2), fromIndex: 9, length: 4, isHighlighted: true);
            AssertHighlightIndex(highlightList.ElementAt(3), fromIndex: 13, length: 14, isHighlighted: false);
            AssertHighlightIndex(highlightList.ElementAt(4), fromIndex: 27, length: 4, isHighlighted: true);
        }

        [Fact]
        public void ShouldSupportHighlightProcessor()
        {
            // Arrange
            const string FullText = "ääääää";
            const string SearchText = "a";
            var highlightMarkerWithoutProcessors = new HighlightMarker(FullText, SearchText);
            var highlightMarkerWithTestProcessor = new HighlightMarker(FullText, SearchText, highlightProcessor: new TestHighlightProcessor("ä", "a"));

            // Act
            var highlightListWithoutProcessing = highlightMarkerWithoutProcessors.ToList();
            var highlightListWithProcessing = highlightMarkerWithTestProcessor.ToList();

            // Assert
            highlightListWithoutProcessing.Should().NotBeNull();
            highlightListWithoutProcessing.Should().HaveCount(1);
            AssertHighlightIndex(highlightListWithoutProcessing.ElementAt(0), fromIndex: 0, length: 6, isHighlighted: false);

            highlightListWithProcessing.Should().NotBeNull();
            highlightListWithProcessing.Should().HaveCount(1);
            AssertHighlightIndex(highlightListWithProcessing.ElementAt(0), fromIndex: 0, length: 6, isHighlighted: true);
        }

        private static void AssertHighlightIndex(HighlightIndex highlightIndex, int fromIndex, int length, bool isHighlighted)
        {
            highlightIndex.FromIndex.Should().Be(fromIndex);
            highlightIndex.Length.Should().Be(length);
            highlightIndex.IsHighlighted.Should().Be(isHighlighted);
        }
    }

    public class TestHighlightProcessor : IHighlightProcessor
    {
        private readonly string searchText;
        private readonly string replaceText;

        public TestHighlightProcessor(string searchText, string replaceText)
        {
            this.searchText = searchText;
            this.replaceText = replaceText;
        }

        public string[] GetFulltextItems(string fulltext)
        {
            return new []
            {
                fulltext,
                fulltext.Replace(this.searchText, this.replaceText)
            };
        }
    }
}