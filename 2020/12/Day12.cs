using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC.Y2020
{
    public class Day12 : EnumerableDay<Movement, int>
    {
        public Day12() : base("12")
        {
            this.ExpectedTest1Result = 25;
            this.ExpectedTest2Result = 286;
        }

        protected override int RunPart1()
        {
            var navigation = new Navigation(MovePosition.Map);
            navigation.Move(this.ParsedInput);
            return navigation.ManhattanDistance;
        }

        protected override int RunPart2()
        {
            var navigation = new Navigation(MoveByWaypoint.Map);
            navigation.Move(this.ParsedInput);
            return navigation.ManhattanDistance;
        }
    }
}
