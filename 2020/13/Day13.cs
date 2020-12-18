using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC.Y2020
{
    public class Day13 : Day<BusSchedule, long>
    {
        public Day13() : base("13")
        {
            this.ExpectedTest1Result = 295;
            this.ExpectedTest2Result = 1068781;
        }

        protected override long RunPart1()
        {
            var schedule = this.ParsedInput;
            var departures = schedule.AvailableBusLines.Select(l => (id: l.Id, time: l.FirstDeparture(schedule.Start)));
            var sortedDepartures = departures.OrderBy(d => d.time);
            return (sortedDepartures.First().time - schedule.Start) * sortedDepartures.First().id;
        }

        protected override long RunPart2()
        {
            var congruences = this.ParsedInput.AvailableBusLines.Select(l => l.AsCongruence());
            var intersection = Congruence.RelativelyPrimeIntersection(congruences);
            return intersection.Offset;
        }
    }
}
