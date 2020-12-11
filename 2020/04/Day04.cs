using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AOC.Y2020
{
    public class Day04 : AOC.Y2020.Day<Passport, int>
    {
        public Day04() : base("04")
        {
            this.ExpectedTest1Result = 2;
            this.ExpectedTest2Result = 8;

            this.InputFormat = SplitInput.ByEmptyLine;
        }

        protected override int RunPart1()
        {
            return this.ParsedInput.Count(i => i.HasRequiredFields());
        }

        protected override int RunPart2()
        {
            this.WriteOutputFile("out.csv", string.Join("\n", this.ParsedInput.Select(i => i.ToCSV())));
            return this.ParsedInput.Count(i => i.HasValidFields());
        }
    }
}
