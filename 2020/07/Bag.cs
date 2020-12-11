using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Y2020
{
    public class Bag
    {
        internal Bag(string name)
        {
            this.Name = name;
        }

        public string Name { get; protected set; }
        public Dictionary<Bag, int> Children { get; } = new();
        public HashSet<Bag> Parents { get; } = new();

        public IEnumerable<Bag> GetAncestors()
        {
            var ancestors = this.Parents
                .Select(p => p.GetAncestors())
                .Aggregate(new List<Bag>(), (result, part) => result.Concat(part).ToList());
            return this.Parents.Union(ancestors).Distinct();
        }

        public override string ToString() => $"{this.Name} bag";
    }
}