namespace AoC2018.Solutions.Day22
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static AoC2018.Solutions.Day22.Part02;

    public static class MapExtensions
    {
        public static IEnumerable<(int X, int Y, Tools tool, int timeTaken)> GetPotentialMovesFrom(this Map map, (int X, int Y, Tools tool, int timeTaken) currentSituation)
        {
            var locations = new List<(int X, int Y)>
            {
                (currentSituation.X + 1, currentSituation.Y),
                (currentSituation.X, currentSituation.Y + 1),
                (currentSituation.X, currentSituation.Y - 1),
                (currentSituation.X - 1, currentSituation.Y),
            };

            // Stay in bounds
            locations = locations.Where(l => l.X >= 0 && l.X < map.Width && l.Y >= 0 && l.Y < map.Height).ToList();

            foreach ((int X, int Y) current in locations)
            {
                Tools validTools = Map.AllowedTools[map.RiskLevels[current.X, current.Y]] & Map.AllowedTools[map.RiskLevels[currentSituation.X, currentSituation.Y]];

                // If the current tool is valid, stick with it... otherwise, return an entry for
                // a move with each possible option for new tool
                if ((validTools & currentSituation.tool) == currentSituation.tool)
                {
                    yield return (current.X, current.Y, currentSituation.tool, currentSituation.timeTaken + 1);
                }
                else
                {
                    foreach (Tools tool in Enum.GetValues(typeof(Tools)))
                    {
                        if ((validTools & tool) == tool)
                        {
                            yield return (current.X, current.Y, tool, currentSituation.timeTaken + 8);
                        }
                    }
                }
            }
        }

        public static void WriteToConsole(this Map map)
        {
            for (int y = 0; y < map.RiskLevels.GetLength(1); y++)
            {
                for (int x = 0; x < map.RiskLevels.GetLength(0); x++)
                {
                    switch (map.RiskLevels[x, y])
                    {
                        case 0:
                            Console.Write(".");
                            break;
                        case 1:
                            Console.Write("=");
                            break;
                        case 2:
                            Console.Write("|");
                            break;
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
