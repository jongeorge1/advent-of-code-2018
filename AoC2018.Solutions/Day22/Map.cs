namespace AoC2018.Solutions.Day22
{
    using System;
    using static AoC2018.Solutions.Day22.Part02;

    public class Map
    {
        public static Tools[] AllowedTools { get; } = new[]
            {
                Tools.ClimbingGear | Tools.Torch,
                Tools.Neither | Tools.ClimbingGear,
                Tools.Neither | Tools.Torch,
            };

        public (int X, int Y) TargetLocation { get; set; }

        public int Depth { get; set; }

        public int[,] ErosionLevels { get; set; }

        public int[,] RiskLevels { get; set; }

        public int Height => this.ErosionLevels.GetLength(1);

        public int Width => this.ErosionLevels.GetLength(0);

        public static Map FromInput(string input, int padding = 0)
        {
            string[] rows = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int depth = int.Parse(rows[0].Substring(7));

            string[] coordinates = rows[1].Substring(8).Split(',');

            (int X, int Y) target = (int.Parse(coordinates[0]), int.Parse(coordinates[1]));
            int[,] erosionLevels = BuildErosionLevels(target, depth, padding);
            int[,] riskLevels = new int[erosionLevels.GetLength(0), erosionLevels.GetLength(1)];

            for (int x = 0; x < erosionLevels.GetLength(0); x++)
            {
                for (int y = 0; y < erosionLevels.GetLength(1); y++)
                {
                    riskLevels[x, y] = GetRiskLevel(erosionLevels[x, y]);
                }
            }

            return new Map
            {
                TargetLocation = target,
                Depth = depth,
                ErosionLevels = erosionLevels,
                RiskLevels = riskLevels,
            };
        }

        private static int[,] BuildErosionLevels((int X, int Y) target, int depth, int padding)
        {
            int maxX = target.X + padding;
            int maxY = target.Y + padding;

            int[,] erosionLevels = new int[maxX + 1, maxY + 1];

            // Because of the way geologic indices are calculated we need to populate the dictionary in a slightly
            // odd way, and in multiple passes
            int offset = 0;
            int limit = Math.Max(maxX, maxY);

            while (offset <= limit)
            {
                // Populate the row
                if (offset <= maxY)
                {
                    for (int x = offset; x <= maxX; x++)
                    {
                        if (offset == 0)
                        {
                            erosionLevels[x, offset] = GetErosionLevel(x * 16807, depth);
                        }
                        else
                        {
                            erosionLevels[x, offset] = GetErosionLevel(erosionLevels[x - 1, offset] * erosionLevels[x, offset - 1], depth);
                        }
                    }
                }

                // Now the column
                if (offset <= maxX)
                {
                    for (int y = offset + 1; y <= maxY; y++)
                    {
                        if (offset == 0)
                        {
                            erosionLevels[offset, y] = GetErosionLevel(y * 48271, depth);
                        }
                        else
                        {
                            erosionLevels[offset, y] = GetErosionLevel(erosionLevels[offset - 1, y] * erosionLevels[offset, y - 1], depth);
                        }
                    }
                }

                offset++;
            }

            // Overwrite target location with 0
            erosionLevels[target.X, target.Y] = 0;

            return erosionLevels;
        }

        private static int GetErosionLevel(long geologicIndex, int depth)
        {
            return (int)((geologicIndex + depth) % 20183);
        }

        private static int GetRiskLevel(int erosionLevel)
        {
            return erosionLevel % 3;
        }
    }
}
