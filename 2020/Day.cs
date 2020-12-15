namespace AOC.Y2020
{
    public abstract class Day<Tin, Tout> : AOC.Day<Tin, Tout>
    {
        public Day(string day) : base("2020", day)
        {
        }
    }

    public abstract class EnumerableDay<Tin, Tout> : AOC.EnumerableDay<Tin, Tout>
    {
        public EnumerableDay(string day) : base("2020", day)
        {
        }
    }
}