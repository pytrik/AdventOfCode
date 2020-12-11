using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC.Y2020
{
    public class Day07 : AOC.Y2020.Day<Bag, int>
    {
        protected BagsGraph Graph { get; set; }

        public Day07() : base("07")
        {
            this.ExpectedTest1Result = 4;
            this.ExpectedTest2Result = 126;
        }

        protected override void SetParsedInput()
        {
            this.Graph = new BagsGraph();
            this.Graph.Build(this.RawInput);
            this.ParsedInput = this.Graph.AsArray();
        }

        protected override int RunPart1()
        {
            return this.Graph.Get("shiny gold").GetAncestors().Count();
        }

        protected override int RunPart2()
        {
            return CountChildBags(this.Graph.Get("shiny gold"), 1) - 1;
        }

        private int CountChildBags(Bag bag, int parentCount)
        {
            return parentCount + bag.Children.Sum(childBag => this.CountChildBags(childBag.Key, childBag.Value * parentCount));
        }
    }
}
