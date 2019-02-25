namespace AoC2018.Solutions.Day17
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public static class MapExtensions
    {
        public static bool CanWaterFlowTo(this Map map, Point point) => map.CanWaterFlowTo(point.X, point.Y);

        public static bool CanWaterFlowTo(this Map map, int x, int y)
        {
            SoilType soilType = map.GetSoilType(x, y);
            return soilType == SoilType.Sand || soilType == SoilType.WetSand;
        }

        public static void AddWater(this Map map)
        {
            map.FlowToEnd(new Point(map.Source.X, map.YMin));
        }

        public static int CountWetTiles(this Map map)
        {
            return map.Slices.Sum(x => x.Value.Count(y => y == SoilType.Water || y == SoilType.WetSand));
        }

        public static int CountWaterTiles(this Map map)
        {
            return map.Slices.Sum(x => x.Value.Count(y => y == SoilType.Water));
        }

        public static void FlowToEnd(this Map map, Point source)
        {
            // How far can we go down before hitting either clay or water, onto which we settle.
            var slice = map.GetSlice(source.X).ToList();
            int nextObstacleDown = slice.FindIndex(source.Y, x => x == SoilType.Water || x == SoilType.Clay);

            // Mark all the squares between y and the next obstacle as wet
            for (int i = source.Y; i < (nextObstacleDown == -1 ? (map.YMax + 1) : nextObstacleDown); i++)
            {
                map.SetSoilType(source.X, i, SoilType.WetSand);
            }

            // Now, if we've gone off the map then return
            if (nextObstacleDown == -1)
            {
                return;
            }

            // We've hit something - either clay or water - that is disrupting the flow.
            // There are two possibilities here; if this location is constrained at some
            // point on either side, then the water will settle here. If it isn't it will
            // flow.

            // First find the extents the water can flow to
            int targetLevel = nextObstacleDown - 1;

            int leftExtent = map.GetMaxHorizontalMovementFrom(source.X, targetLevel, -1);
            int rightExtent = map.GetMaxHorizontalMovementFrom(source.X, targetLevel, +1);

            bool waterIsFlowingToLeft = map.CanWaterFlowTo(leftExtent, targetLevel + 1);
            bool waterIsFlowingToRight = map.CanWaterFlowTo(rightExtent, targetLevel + 1);

            // Check to see where the water is going. If it's going down from either end,
            // then it's flowing. Otherwise it's settling.
            bool isFlowing = waterIsFlowingToLeft || waterIsFlowingToRight;

            // Now populate the sqaures in the extent
            for (int x = leftExtent; x <= rightExtent; x++)
            {
                map.SetSoilType(x, targetLevel, isFlowing ? SoilType.WetSand : SoilType.Water);
            }

            // Now we need to continue flowing...
            if (waterIsFlowingToLeft)
            {
                map.FlowToEnd(new Point { X = leftExtent, Y = targetLevel + 1 });
            }

            if (waterIsFlowingToRight)
            {
                map.FlowToEnd(new Point { X = rightExtent, Y = targetLevel + 1 });
            }
        }

        public static int GetMaxHorizontalMovementFrom(this Map map, int x, int y, int direction)
        {
            while (!map.CanWaterFlowTo(x, y + 1) && map.CanWaterFlowTo(x + direction, y))
            {
                x += direction;
            }

            return x;
        }

        public static void WriteToConsole(this Map map)
        {
            int minX = map.Slices.Keys.Min();
            int maxX = map.Slices.Keys.Max();

            Console.WriteLine($"X: {minX} - {maxX}. Y: {map.YMin} - {map.YMax}");

            for (int y = map.YMin; y <= map.YMax; y++)
            {
                Console.Write($"{y:D5}   ");
                for (int x = minX; x <= maxX; x++)
                {
                    switch (map.GetSoilType(x, y))
                    {
                        case SoilType.Clay:
                            Console.Write("#");
                            break;
                        case SoilType.Sand:
                            Console.Write(".");
                            break;
                        case SoilType.WetSand:
                            Console.Write("|");
                            break;
                        case SoilType.Water:
                            Console.Write("~");
                            break;
                        default:
                            Console.Write("?");
                            break;
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
