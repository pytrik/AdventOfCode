using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Y2020
{
    public class Bus
    {
        public long Id { get; }
        public long Index { get; }

        public Bus(long id, long index)
        {
            if (id <= 0)
                throw new ArgumentException($"Argument must be greater than 0", nameof(id));
            this.Id = id;
            this.Index = index;
        }

        public long FirstDeparture(long after)
        {
            if (after % this.Id == 0)
                return after;
            else
                return (after / this.Id) * this.Id + this.Id;
        }

        public Congruence AsCongruence()
        {
            return new Congruence(this.Id, this.Index);
        }
    }

    public class BusSchedule
    {
        public long Start { get; protected set; }
        public IEnumerable<Bus> BusLines { get; protected set; }
        public IEnumerable<Bus> AvailableBusLines => this.BusLines.Where(b => b != null);
        public static BusSchedule Parse(IEnumerable<string> input)
        {
            return new BusSchedule()
            {
                Start = long.Parse(input.First()),
                BusLines = input.Last().Split(',').Select((n, i) => n == "x" ? null : new Bus(long.Parse(n), i))
            };
        }
    }
}