using System.Linq;

namespace AOC.Y2020
{
    public class Day02 : DayPassword
    {
        public Day02() : base("02")
        {
            this.ExpectedTest1Result = 2;
            this.ExpectedTest2Result = 1;
        }

        protected override int RunPart1()
        {
            return this.ParsedInput.Count(pw => pw.IsValidSledRental());
        }

        protected override int RunPart2()
        {
            return this.ParsedInput.Count(pw => pw.IsValidTobogganCorporate());
        }
    }
}