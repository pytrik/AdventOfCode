using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace AOC.Y2020
{
    public static class SeatingNeighboursIterator
    {
        public static IEnumerable<Seating> IterateToStableState(Seating seating, int maxDistance, int maxNeighours)
        {
            Seating current = seating;
            Seating previous = null;
            while (!current.Equals(previous))
            {
                yield return current;
                previous = current;
                current = Next(previous, maxDistance, maxNeighours);
            }
        }

        public static Seating Next(Seating seating, int maxDistance, int maxNeighours)
        {
            var seats = new SeatState[seating.Rows, seating.Columns];

            for (var row = 0; row < seating.Rows; row++)
                for (var column = 0; column < seating.Columns; column++)
                {
                    var neighboursOccupied = seating.NeighboursInState(row, column, SeatState.Occupied, maxDistance);
                    var state = seating.Seats[row, column];
                    if (state == SeatState.Occupied && neighboursOccupied >= maxNeighours)
                        state = SeatState.Empty;
                    else if (state == SeatState.Empty && neighboursOccupied == 0)
                        state = SeatState.Occupied;
                    seats.SetValue(state, row, column);
                }

            return new Seating(seats);
        }
    }
}