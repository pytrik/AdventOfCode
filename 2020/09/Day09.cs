using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC.Y2020
{
    public class Day09 : EnumerableDay<long, long>
    {
        public Day09() : base("09")
        {
            this.ExpectedTest1Result = 127;
            this.ExpectedTest2Result = 62;
        }

        private bool hasMatchInPreamble(int index, int preamble)
        {
            for (var i = index - preamble; i < index; i++)
                for (var j = i + 1; j < index; j++)
                    if (this.ParsedInput[i] + this.ParsedInput[j] == this.ParsedInput[index])
                        return true;
            return false;
        }

        protected override long RunPart1()
        {
            var preamble = this.ParsedInput.Length <= 50 ? 5 : 25;
            for (var toCheckIndex = preamble; toCheckIndex < this.ParsedInput.Length; toCheckIndex++)
            {
                if (!hasMatchInPreamble(toCheckIndex, preamble))
                    return this.ParsedInput[toCheckIndex];
            }

            return 0;
        }

        protected override long RunPart2()
        {
            var mustSumTo = this.RunPart1();
            for (var start = 0; start < this.ParsedInput.Length; start++)
            {
                var result = new List<long>();
                var current = start;
                var sum = 0L;
                while (sum < mustSumTo)
                {
                    var value = this.ParsedInput[current];
                    result.Add(value);
                    sum += value;
                    current++;
                }

                if (sum == mustSumTo)
                    return result.Min() + result.Max();
            }
            return 0;
        }
    }
}
