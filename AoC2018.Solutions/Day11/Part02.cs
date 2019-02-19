namespace AoC2018.Solutions.Day11
{
    using System;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            int serialNumber = int.Parse(input);

            // Grid is represented as a 1d array because it's easier to work with.
            // Remember in this form, the grid is 0 based but the question expectes 1 based...
            int[] grid = Enumerable.Range(0, 90000).Select(x => this.PowerLevel(x, serialNumber)).ToArray();

            // Now we have the grid, we need to find the most powerful 3x3 bank of cells
            (int X, int Y, int Size, int PowerLevel) bestGridSoFar = (-1, -1, -1, int.MinValue);

            for (int x = 0; x < 300; x++)
            {
                for (int y = 0; y < 300; y++)
                {
                    int sizeMax = Math.Min(300 - x, 300 - y);
                    int previousSizePower = 0;

                    // This loop is calculating the power of a bank of every
                    // possible size starting at x, y. It does this by using the
                    // power for the previous bank size and just adding the edge
                    // values to get the new total
                    for (int i = 0; i < sizeMax; i++)
                    {
                        int additionalPower = 0;

                        // Add the column to the right of the previous bank
                        for (int dy = 0; dy < i; dy++)
                        {
                            additionalPower += grid[this.ToIndex(x + i, y + dy)];
                        }

                        // Now add the row to the bottom of the previous bank
                        for (int dx = 0; dx < (i + 1); dx++)
                        {
                            additionalPower += grid[this.ToIndex(x + dx, y + i)];
                        }

                        previousSizePower += additionalPower;

                        if (previousSizePower > bestGridSoFar.PowerLevel)
                        {
                            bestGridSoFar = (x, y, i, previousSizePower);
                        }
                    }
                }
            }

            return string.Concat(bestGridSoFar.X + 1, ",", bestGridSoFar.Y + 1, ",", bestGridSoFar.Size + 1);
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
