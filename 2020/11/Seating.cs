using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AOC.Y2020
{
    public enum SeatState
    {
        None,
        Empty,
        Occupied
    }

    public class Seating : IEquatable<Seating>
    {
        private static readonly Dictionary<char, SeatState> charSeat = new()
        {
            { '.', SeatState.None },
            { 'L', SeatState.Empty },
            { '#', SeatState.Occupied }
        };

        private static readonly List<(int row, int column)> directions = new()
        {
            (-1, -1),
            (-1, 0),
            (-1, 1),
            (0, -1),
            (0, 1),
            (1, -1),
            (1, 0),
            (1, 1)
        };

        public SeatState[,] Seats { get; private set; }
        public int Rows => this.Seats.GetLength(0);
        public int Columns => this.Seats.GetLength(1);

        public Seating(SeatState[,] seats)
        {
            this.Seats = seats;
        }

        public int SeatsInState(SeatState state)
        {
            return this.Seats.Cast<SeatState>().Count(s => s == state);
        }

        public int NeighboursInState(int row, int column, SeatState state, int maxDistance)
        {
            return GetDirectionalNeighbours(row, column, maxDistance)
                .Count(point => this.Seats[point.row, point.column] == state);
        }

        private Dictionary<(int row, int column), List<(int row, int column)>> directionalNeighbours = new();
        private List<(int row, int column)> GetDirectionalNeighbours(int row, int column, int maxDistance)
        {
            var point = (row, column);
            List<(int row, int column)> result = null;
            if (this.directionalNeighbours.ContainsKey(point))
                result = this.directionalNeighbours[point];
            else
            {
                result = new List<(int row, int column)>();
                foreach (var direction in directions)
                {
                    var distance = 1;
                    while (distance <= maxDistance)
                    {
                        var neighbour = (row: row + distance * direction.row, column: column + distance * direction.column);
                        if (neighbour.row < 0 || neighbour.row >= this.Rows ||
                            neighbour.column < 0 || neighbour.column >= this.Columns)
                        {
                            break;
                        }
                        else if (this.Seats[neighbour.row, neighbour.column] != SeatState.None)
                        {
                            result.Add(neighbour);
                            break;
                        }
                        distance++;
                    }
                }
                this.directionalNeighbours.Add(point, result);
            }
            return result;
        }

        private static SeatState SeatFromChar(char ch)
        {
            if (charSeat.ContainsKey(ch))
                return charSeat[ch];
            else throw new ArgumentException($"'{ch}' is not a valid seat state", nameof(ch));
        }

        public override string ToString()
        {
            var chars = charSeat.ToDictionary(c => c.Value, c => c.Key);
            var sb = new StringBuilder();
            for (var row = 0; row < this.Rows; row++)
            {
                for (var column = 0; column < this.Columns; column++)
                {
                    sb.Append(chars[this.Seats[row, column]]);
                }
                sb.Append('\n');
            }

            return sb.ToString();
        }

        public bool Equals(Seating other)
        {
            return ArraysAreEqual<SeatState>(this.Seats, other?.Seats);
        }

        public static bool ArraysAreEqual<T>(Array one, Array other)
        {
            if (one == null || other == null)
                return one == other;

            return one.Rank == other.Rank &&
                   Enumerable.Range(0, one.Rank).All(dimension => one.GetLength(dimension) == other.GetLength(dimension)) &&
                   one.Cast<T>().SequenceEqual(other.Cast<T>());
        }

        public static Seating Parse(IEnumerable<string> input)
        {
            var grid = input.ToArray();
            int rows = grid.Length;
            int columns = grid.First().Length;
            var seats = new SeatState[rows, columns];
            for (var row = 0; row < rows; row++)
                for (var column = 0; column < columns; column++)
                {
                    seats.SetValue(SeatFromChar(grid[row][column]), row, column);
                }
            return new Seating(seats);
        }
    }
}
