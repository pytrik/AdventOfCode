using System;
using System.Linq;

namespace AOC.Y2020
{
    public class Day06 : EnumerableDay<Answers, int>
    {
        public Day06() : base("06")
        {
            this.ExpectedTest1Result = 11;
            this.ExpectedTest2Result = 6;

            this.InputFormat = SplitInput.ByEmptyLine;
        }

        protected override int RunPart1()
        {
            return this.ParsedInput.Select(a => a.Count()).Sum();
        }

        protected override int RunPart2()
        {
            return this.ParsedInput
                .Select(a => a.Where(kvp => kvp.Value == a.NumberOfPeople).Count())
                .Sum();
        }
    }
}
