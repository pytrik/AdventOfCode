using System.Collections.Generic;
using System.Linq;

namespace AOC
{
    public abstract class EnumerableDay<Tin, Tout> : Day<Tin[], Tout>
    {
        protected EnumerableDay(string year, string day) : base(year, day) { }

        protected override Tin[] ParseInput(IEnumerable<string> raw)
        {
            var converter = StringConverter.ToTypeEnumerable<Tin>();
            return converter(raw).ToArray();
        }
    }
}