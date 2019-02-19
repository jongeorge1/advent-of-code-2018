namespace AoC2018.Solutions.Day11
{
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            int serialNumber = int.Parse(input);

            // Grid is represented as a 1d array because it's easier to work with.
            // Remember in this form, the grid is 0 based but the question expectes 1 based...
            int[] grid = Enumerable.Range(0, 90000).Select(x => this.PowerLevel(x, serialNumber)).ToArray();

            // Now we have the grid, we need to find the most powerful 3x3 bank of cells
            (int X, int Y, int PowerLevel) bestGridSoFar = (-1, -1, int.MinValue);

            for (int x = 0; x < 297; x++)
            {
                for (int y = 0; y < 297; y++)
                {
                    int bankPower = grid[this.ToIndex(x, y)]
                        + grid[this.ToIndex(x + 1, y)]
                        + grid[this.ToIndex(x + 2, y)]
                        + grid[this.ToIndex(x, y + 1)]
                        + grid[this.ToIndex(x + 1, y + 1)]
                        + grid[this.ToIndex(x + 2, y + 1)]
                        + grid[this.ToIndex(x, y + 2)]
                        + grid[this.ToIndex(x + 1, y + 2)]
                        + grid[this.ToIndex(x + 2, y + 2)];

                    if (bankPower > bestGridSoFar.PowerLevel)
                    {
                        bestGridSoFar = (x, y, bankPower);
                    }
                }
            }

            return string.Concat(bestGridSoFar.X + 1, ",", bestGridSoFar.Y + 1);
        }

        private int ToIndex(int x, int y)
        {
            return x + (y * 300);
        }

        private int PowerLevel(int cell, int serialNumber)
        {
            int x = (cell % 300) + 1;
            int y = (cell / 300) + 1;

            int rackId = x + 10;
            int startPower = rackId * y;
            int power = startPower += serialNumber;
            power *= rackId;

            int hundreds = (power % 1000) / 100;

            return hundreds - 5;
        }
    }
}
