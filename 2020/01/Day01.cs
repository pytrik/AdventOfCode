namespace AOC.Y2020
{
    public class Day01 : EnumerableDay<int, int>
    {
        public Day01() : base("01")
        {
            this.ExpectedTest1Result = 514579;
            this.ExpectedTest2Result = 241861950;
        }

        protected override int RunPart1()
        {
            foreach (var i in this.ParsedInput)
                foreach (var j in this.ParsedInput)
                    if (i + j == 2020)
                        return i * j;
            return 0;
        }

        protected override int RunPart2()
        {
            foreach (var i in this.ParsedInput)
                foreach (var j in this.ParsedInput)
                    foreach (var k in this.ParsedInput)
                        if (i + j + k == 2020)
                            return i * j * k;
            return 0;
        }
    }
}
