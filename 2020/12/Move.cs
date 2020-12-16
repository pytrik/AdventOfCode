using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Y2020
{
    public class MovePosition
    {
        public static Dictionary<char, Action<Navigation, int>> Map => new()
        {
            { 'N', (nav, amount) => Change(nav, amount, 0) },
            { 'S', (nav, amount) => Change(nav, -amount, 0) },
            { 'E', (nav, amount) => Change(nav, 0, amount) },
            { 'W', (nav, amount) => Change(nav, 0, -amount) },
            { 'L', (nav, amount) => Turn(nav, -amount) },
            { 'R', (nav, amount) => Turn(nav, amount) },
            { 'F', (nav, amount) => ForwardDirection(nav)(nav, amount) }
        };

        private static void Change(Navigation navigation, int x, int y)
        {
            navigation.Position = (navigation.Position.x + x, navigation.Position.y + y);
        }

        private static void Turn(Navigation navigation, int angle)
        {
            navigation.Direction = (navigation.Direction + angle) % 360;
            if (navigation.Direction < 0)
                navigation.Direction += 360;
        }

        private static Dictionary<int, Action<Navigation, int>> angleDirection = new()
        {
            { 0, Map['E'] },
            { 90, Map['S'] },
            { 180, Map['W'] },
            { 270, Map['N'] },
        };
        private static Action<Navigation, int> ForwardDirection(Navigation navigation)
        {
            int direction = navigation.Direction;
            if (angleDirection.ContainsKey(direction))
                return angleDirection[direction];
            else
                throw new InvalidDirectionException($"Could not determine direction from {direction}, must be a mulitple of 90");
        }
    }

    public class MoveByWaypoint
    {
        public static Dictionary<char, Action<Navigation, int>> Map => new()
        {
            { 'N', (nav, amount) => Change(nav, amount, 0) },
            { 'S', (nav, amount) => Change(nav, -amount, 0) },
            { 'E', (nav, amount) => Change(nav, 0, amount) },
            { 'W', (nav, amount) => Change(nav, 0, -amount) },
            { 'L', (nav, amount) => Turn(nav, -amount) },
            { 'R', (nav, amount) => Turn(nav, amount) },
            { 'F', (nav, amount) => ForwardInner(nav, amount) }
        };

        private static void Change(Navigation navigation, int x, int y)
        {
            navigation.Waypoint = (navigation.Waypoint.x + x, navigation.Waypoint.y + y);
        }

        private static void ForwardInner(Navigation navigation, int times)
        {
            navigation.Position = (
                navigation.Position.x + navigation.Waypoint.x * times,
                navigation.Position.y + navigation.Waypoint.y * times
            );
        }

        private static int[] cos = new[] { 1, 0, -1, 0 };
        private static int[] sin = new[] { 0, 1, 0, -1 };
        private static void Turn(Navigation navigation, int angle)
        {
            if (angle < 0)
                angle += 360;
            angle %= 360;
            angle /= 90;

            var x = cos[angle] * navigation.Waypoint.x - sin[angle] * navigation.Waypoint.y;
            var y = sin[angle] * navigation.Waypoint.x + cos[angle] * navigation.Waypoint.y;
            navigation.Waypoint = (x, y);
        }
    }
}