using System.Collections.Generic;
using System.Linq;

namespace AOC.Y2020
{
    public class Adapter
    {
        public List<Adapter> ConnectsTo { get; protected set; }
        public long Joltage { get; }

        public Adapter(long joltage)
        {
            Joltage = joltage;
        }

        private long nrOfConnections = 0;
        public long CountConnections()
        {
            if (this.nrOfConnections == 0)
                if (this.ConnectsTo.Count() == 0)
                    this.nrOfConnections = 1;
                else
                    this.nrOfConnections = this.ConnectsTo.Sum(c => c.CountConnections());

            return this.nrOfConnections;
        }

        public static IEnumerable<Adapter> BuildGraph(IEnumerable<long> joltages)
        {
            var adapters = new List<Adapter>();

            // By going throught the joltages in descending order we can
            //  guarantee that the possible connections for any adapter have
            //  already been created, since they're always a higher joltage.
            foreach (var joltage in joltages.OrderByDescending(i => i))
            {
                var connections = adapters.Where(a => a.Joltage - joltage <= 3);
                var adapter = new Adapter(joltage) { ConnectsTo = connections.ToList() };
                adapters.Add(adapter);
            }

            return adapters;
        }
    }
}