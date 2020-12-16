using System;
using System.Collections.Generic;

namespace AOC.Y2020
{
    public class Navigation
    {
        private readonly Dictionary<char, Action<Navigation, int>> map;

        public Navigation(Dictionary<char, Action<Navigation, int>> map)
        {
            this.map = map ?? throw new ArgumentNullException(nameof(map));
        }

        // north: positive X, south negative X
        // east: positive Y, west negative Y
        public (int x, int y) Position { get; set; } = (0, 0);

        public (int x, int y) Waypoint { get; set; } = (1, 10);

        // 0 is east, clockwise is positive
        public int Direction { get; set; } = 0;

        public int ManhattanDistance => Math.Abs(this.Position.x) + Math.Abs(this.Position.y);

        public void Move(Movement movement) => this.map[movement.Move](this, movement.Amount);
        public void Move(IEnumerable<Movement> movements)
        {
            foreach (var movement in movements)
                this.Move(movement);
        }
    }
}