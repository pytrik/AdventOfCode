using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AOC.Y2020
{
    public class Day06 : AOC.Y2020.Day<Answers, int>
    {
        public Day06() : base("06")
        {
            this.ExpectedTest1Result = 11;
            this.ExpectedTest2Result = 6;

            this.split = new Regex(@"(?:\r\n){2}", RegexOptions.Compiled);
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
