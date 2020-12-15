using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC.Y2020
{
    public class Day10 : EnumerableDay<long, long>
    {
        public Day10() : base("10")
        {
            this.ExpectedTest1Result = 220;
            this.ExpectedTest2Result = 19208;
        }

        private IEnumerable<long> allJoltages => new[] { 0, this.ParsedInput.Max() + 3 }.Concat(this.ParsedInput);
        private IEnumerable<Adapter> allAdapters => Adapter.BuildGraph(this.allJoltages);

        protected override long RunPart1()
        {
            var sorted = allJoltages.OrderBy(i => i).ToArray();
            var differences = new List<long>();

            for (var i = 1; i < sorted.Length; i++)
                differences.Add(sorted[i] - sorted[i - 1]);

            return differences
                .GroupBy(i => i)
                .Aggregate(1, (result, group) => result * group.Count());
        }

        protected override long RunPart2()
        {
            var start = allAdapters.OrderBy(a => a.Joltage).First();
            return start.CountConnections();
        }
    }
}
