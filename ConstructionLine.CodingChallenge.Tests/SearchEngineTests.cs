using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        [Test]
        public void WhenSearchOptionsNotProvided_ShouldThrowException()
        {
            var shirts = new List<Shirt>();
            var searchEngine = new SearchEngine(shirts);
            SearchOptions searchOptions = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                searchEngine.Search(searchOptions);
            });
        }

        [Test]
        public void WhenColorsInSearchOptionsNotProvided_ShouldThrowException()
        {
            var shirts = new List<Shirt>();
            var searchEngine = new SearchEngine(shirts);
            SearchOptions searchOptions = new SearchOptions { Colors = null };

            Assert.Throws<ArgumentNullException>(() =>
            {
                searchEngine.Search(searchOptions);
            });
        }

        [Test]
        public void WhenSizesInSearchOptionsNotProvided_ShouldThrowException()
        {
            var shirts = new List<Shirt>();
            var searchEngine = new SearchEngine(shirts);
            SearchOptions searchOptions = new SearchOptions { Colors = new List<Color>(), Sizes = null };

            Assert.Throws<ArgumentNullException>(() =>
            {
                searchEngine.Search(searchOptions);
            });
        }

        [Test]
        public void WhenSearchingForRedAndAnySizeShirts_ShouldReturnOnlyRedShirt()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void WhenSearchingForRedAndSmallSizeShirts_ShouldReturnOnlyRedSmallShirt()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void WhenSearchingForMultipleColorsAndMultipleSizes_ShouldReturnCorrectResults()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Black },
                Sizes = new List<Size> { Size.Small, Size.Medium }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }


        [Test]
        public void WhenSearchingForAnyColorAndAnySizeShirts_ShouldReturnInputShirtResult()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void WhenSearchingForNonMatchingColor_ShouldReturnNoResults()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Yellow },
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void WhenSearchingForNonMatchingSize_ShouldReturnNoResults()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Medium }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }
    }
}

