using System;
using System.Linq;

namespace AOC.Y2020
{
    public class Day05 : EnumerableDay<Seat, int>
    {
        public Day05() : base("05")
        {
            this.ExpectedTest1Result = 820;
            this.ExpectedTest2Result = 566;
        }

        protected override int RunPart1()
        {
            return this.ParsedInput.Max(i => i.Id);
        }

        protected override int RunPart2()
        {
            var Ids = this.ParsedInput.Select(s => s.Id).OrderBy(s => s);
            var min = Ids.Min();
            return Ids.Where((v, i) => v - min != i).First() - 1;
        }
    }
}
