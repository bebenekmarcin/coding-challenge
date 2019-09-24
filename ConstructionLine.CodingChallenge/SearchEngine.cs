using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly IQueryable<Shirt> _shirts;


        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts.AsQueryable();
        }

        public SearchResults Search(SearchOptions options)
        {
            if (options == null) throw new ArgumentNullException();
            if (options.Colors == null) throw new ArgumentNullException();
            if (options.Sizes == null) throw new ArgumentNullException();

            var matchingShirts = _shirts;
            if (options.Colors.Any())
                matchingShirts = matchingShirts.Where(s => options.Colors.Contains(s.Color));
            if (options.Sizes.Any())
                matchingShirts = matchingShirts.Where(s => options.Sizes.Contains(s.Size));

            var matchingColorCounts = Color.All.Select(c => new ColorCount { Color = c, Count = matchingShirts.Count(s => s.Color == c) });
            var matchingSizeCounts = Size.All.Select(s => new SizeCount { Size = s, Count = matchingShirts.Count(shirt => shirt.Size == s) });

            return new SearchResults
            {
                Shirts = matchingShirts.ToList(),
                ColorCounts = matchingColorCounts.ToList(),
                SizeCounts = matchingSizeCounts.ToList(),
            };
        }
    }
}