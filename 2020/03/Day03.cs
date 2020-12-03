using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC.Y2020
{
    public class Day03 : DayTrees
    {
        public Day03() : base("03")
        {
            this.ExpectedTest1Result = 7;
            this.ExpectedTest2Result = 336;
        }

        protected override int RunPart1()
        {
            return TraversedTrees(this.ParsedInput.ToArray(), (1, 3)).Count();
        }

        protected override int RunPart2()
        {
            var matrix = this.ParsedInput.ToArray();
            var steps = new List<(int, int)>()
            {
                (1, 1),
                (1, 3),
                (1, 5),
                (1, 7),
                (2, 1)
            };

            return steps
                .Select(step => TraversedTrees(matrix, step).Count())
                .Aggregate(1, (aggregate, result) => aggregate * result);
        }
    }
}
