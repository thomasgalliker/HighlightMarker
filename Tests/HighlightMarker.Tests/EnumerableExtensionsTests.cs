using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using FluentAssertions;

using Xunit;


namespace HighlightMarker.Tests
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void ToConsecutiveGroupsExtensionTestWithEmptyList()
        {
            // Arrange.
            var sourceCollection = new List<int>();

            // Act.
            var resultListOfGroups = sourceCollection.ToConsecutiveGroups().ToList();

            // Assert.
            resultListOfGroups.Should().NotBeNull();
            resultListOfGroups.Should().HaveCount(0);
        }

        [Fact]
        public void ToConsecutiveGroupsExtensionTest()
        {
            // Arrange.
            var sourceCollection = new List<int> { 5, 6, 7, 10, -2, -1, 3, 0 };

            // Act.
            var resultListOfGroups = sourceCollection.ToConsecutiveGroups().ToList();

            // Assert.
            resultListOfGroups.Should().NotBeNull();
            resultListOfGroups.Should().HaveCount(5);
            Assert.True(resultListOfGroups[0].Count() == 3);
            Assert.True(resultListOfGroups[1].Count() == 1);
            Assert.True(resultListOfGroups[2].Count() == 2);
            Assert.True(resultListOfGroups[3].Count() == 1);
            Assert.True(resultListOfGroups[4].Count() == 1);
        }
    }
}