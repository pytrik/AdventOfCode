using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Y2020
{
    public class Day11 : Day<Seating, long>
    {
        public Day11() : base("11")
        {
            this.ExpectedTest1Result = 37;
            this.ExpectedTest2Result = 26;
        }

        protected override long RunPart1()
        {
            var states = SeatingNeighboursIterator.IterateToStableState(this.ParsedInput, 1, 4);
            return states.Last().SeatsInState(SeatState.Occupied);
        }

        protected override long RunPart2()
        {
            var states = SeatingNeighboursIterator.IterateToStableState(this.ParsedInput, int.MaxValue, 5);
            WriteDebug(states);
            return states.Last().SeatsInState(SeatState.Occupied);
        }

        private void WriteDebug(IEnumerable<Seating> states)
        {
            var output = states.Select(s => s.ToString());
            var seperatorLine = new string('-', states.First().Columns);
            WriteOutputFile($"./out-{states.First().Rows}.txt", string.Join($"{seperatorLine}\n", output));
        }
    }
}
