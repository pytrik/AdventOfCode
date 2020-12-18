using System.Collections.Generic;
using System.Linq;

namespace AOC.Y2020
{
    public class Congruence
    {
        public Congruence(long modulo, long offset)
        {
            this.Modulo = modulo;
            this.Offset = -offset % modulo;
            if (this.Offset < 0) this.Offset += this.Modulo;
        }

        public long Modulo { get; }
        public long Offset { get; }

        public long FirstIntersectingElement(Congruence other)
        {
            var value = other.Offset - this.Offset;
            while (true)
                if (value % other.Modulo == 0)
                    return value - other.Offset;
                else
                    value += this.Modulo;
        }

        private static Congruence RelativelyPrimeIntersection(Congruence one, Congruence other)
        {
            // this only works if this.Modulo and other.Module are relatively prime
            //  since it is trivial to manually check this in the input
            //  I'll skip the rather bothersome step of checking it here

            var modulo = one.Modulo * other.Modulo;
            var offset = one.FirstIntersectingElement(other);
            return new Congruence(modulo, offset);
        }

        public static Congruence RelativelyPrimeIntersection(IEnumerable<Congruence> congruences)
        {
            var sorted = congruences
                .OrderByDescending(l => l.Modulo)
                .ToList();

            var intersection = sorted.First();
            foreach (var congruence in sorted.Skip(1))
                intersection = RelativelyPrimeIntersection(intersection, congruence);

            return intersection;
        }

        public static Congruence Intersect(IEnumerable<(int modulo, int offset)> congruences)
        {
            return RelativelyPrimeIntersection(congruences.Select(c => new Congruence(c.modulo, c.offset)));
        }

        public override string ToString()
        {
            return $"Congruence {this.Offset} modulo {this.Modulo}";
        }
    }
}