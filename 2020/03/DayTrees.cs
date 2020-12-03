using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AOC.Y2020
{
    public abstract class DayTrees : AOC.Y2020.Day<bool[], int>
    {
        public DayTrees(string day) : base(day)
        {
        }

        private Dictionary<bool[][], HashSet<(int row, int column)>> cachedTrees = new();

        protected static HashSet<(int row, int column)> TreeCoordinates(bool[][] matrix)
        {
            var trees = new HashSet<(int, int)>();
            var nrOfRows = matrix.Length;
            var nrOfColumns = matrix.First().Length;

            for (int row = 0; row < nrOfRows; row++)
                for (int column = 0; column < nrOfColumns; column++)
                    if (matrix[row][column])
                        trees.Add((row, column));

            return trees;
        }

        protected static HashSet<(int row, int column)> Traverse(bool[][] matrix, (int row, int column) step)
        {
            var points = new HashSet<(int row, int column)>();
            var height = matrix.Count();
            var width = matrix.First().Count();
            (int row, int column) position = (0, 0);

            do
            {
                points.Add(position);
                position = (position.row + step.row, (position.column + step.column) % width);
            }
            while (position.row < height);

            return points;
        }

        protected IEnumerable<(int row, int column)> TraversedTrees(bool[][] matrix, (int row, int column) step)
        {
            HashSet<(int row, int column)> trees;
            if (this.cachedTrees.ContainsKey(matrix))
            {
                trees = this.cachedTrees[matrix];
            }
            else
            {
                trees = TreeCoordinates(matrix);
                this.cachedTrees[matrix] = trees;
            }

            return Traverse(matrix, step).Intersect(trees);
        }

        protected override bool TryParseInput()
        {
            this.ParsedInput = this.RawInput.Select(line => line.Select(ch => ch == '#').ToArray()).ToArray();
            return true;
        }
    }
}
