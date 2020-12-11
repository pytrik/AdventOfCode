using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC.Y2020
{
    public abstract class DayBags : AOC.Y2020.Day<Bag, int>
    {
        protected BagsGraph Graph { get; set; }

        protected DayBags(string day) : base(day)
        {
        }

        protected override bool TryParseInput()
        {
            this.Graph = new BagsGraph();
            this.Graph.Build(this.RawInput);
            this.ParsedInput = this.Graph.AsArray();
            return true;
        }
    }

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

    public class BagsGraph
    {
        private Dictionary<string, Bag> cache = new();

        public IEnumerable<Bag> Build(IEnumerable<string> input)
        {
            return input.Select(this.BagFromString).ToArray();
        }

        public Bag Get(string name) => this.cache[name];

        public Bag[] AsArray() => this.cache.Select(kvp => kvp.Value).ToArray();

        private static readonly Regex bag = new Regex(@"^(?<name>\w+\s+\w+)", RegexOptions.Compiled);
        private static readonly Regex bags = new Regex(@"(?<amount>\d+)\s*(?<name>\w+\s+\w+)", RegexOptions.Compiled);
        private Bag BagFromString(string input)
        {
            var result = this.GetOrCreateBag(bag.Match(input).Groups["name"].Value);
            foreach (var match in bags.Matches(input).ToArray())
            {
                var child = this.GetOrCreateBag(match.Groups["name"].Value);
                var numberOfChildren = int.Parse(match.Groups["amount"].Value);
                child.Parents.Add(result);
                result.Children.Add(child, numberOfChildren);
            }
            return result;
        }

        private Bag GetOrCreateBag(string name)
        {
            Bag result;
            if (this.cache.ContainsKey(name))
            {
                result = this.cache[name];
            }
            else
            {
                result = new Bag(name);
                this.cache.Add(result.Name, result);
            }
            return result;
        }
    }
}