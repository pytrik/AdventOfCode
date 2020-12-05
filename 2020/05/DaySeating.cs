using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC.Y2020
{
    public abstract class DaySeating : AOC.Y2020.Day<Seat, int>
    {
        public DaySeating(string day) : base(day)
        {
        }

        protected override bool TryParseInput()
        {
            this.ParsedInput = this.RawInput.Select(Seat.FromString).ToArray();
            return true;
        }
    }

    public class Seat
    {
        public int Row { get; protected set; }
        public int Column { get; protected set; }
        public int Id => this.Row * 8 + this.Column;

        private static int StringToByte(string input, char oneBit)
        {
            return input.Aggregate(0, (row, ch) => (row << 1) | (ch == oneBit ? 1 : 0));
        }

        public static Seat FromString(string input)
        {
            return new Seat()
            {
                Row = StringToByte(input.Substring(0, 7), 'B'),
                Column = StringToByte(input.Substring(7, 3), 'R')
            };
        }
    }
}