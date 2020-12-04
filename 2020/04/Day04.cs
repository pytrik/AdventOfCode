using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC.Y2020
{
    public class Day04 : DayPassport
    {
        public Day04() : base("04")
        {
            this.ExpectedTest1Result = 2;
            this.ExpectedTest2Result = 4;
        }

        protected override int RunPart1()
        {
            return this.ParsedInput.Count(i => i.HasRequiredFields());
        }

        protected override int RunPart2()
        {
            return this.ParsedInput.Count(i => i.HasValidValues());
        }
    }
}
