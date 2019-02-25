namespace AoC2018.Solutions.Day17
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class Map
    {
        public Map(int yMin, int yMax)
        {
            this.YMin = yMin;
            this.YMax = yMax;
            this.Slices = new Dictionary<int, SoilType[]>();
            this.Source = new Point(500, 0);
            this.WaterTilesCount = 0;
        }

        public Dictionary<int, SoilType[]> Slices { get; set; }

        public Point Source { get; }

        public int WaterTilesCount { get; set; }

        public int YMin { get; }

        public int YMax { get; }

        public static Map FromClayPoints(Point[] clayPoints)
        {
            // So far when representing grids of data, I've been using
            // 1d arrays and using an offset to translate x/y values to
            // an index. In this case, we know our range of y values but
            // not of x, so I'm going to use a dictionary of arrays, with
            // the key representing the x value and each value representing
            // a column of data.
            int yMin = clayPoints.Min(x => x.Y);
            int yMax = clayPoints.Max(x => x.Y);

            var result = new Map(yMin, yMax);

            foreach (Point current in clayPoints)
            {
                result.SetSoilType(current.X, current.Y, SoilType.Clay);
            }

            return result;
        }

        public void SetSoilType(int x, int y, SoilType soilType)
        {
            this.GetSlice(x)[y] = soilType;

            if (soilType == SoilType.Water)
            {
                this.WaterTilesCount++;
            }
        }

        public SoilType GetSoilType(int x, int y)
        {
            return this.GetSlice(x)[y];
        }

        public SoilType[] GetSlice(int x)
        {
            if (!this.Slices.TryGetValue(x, out SoilType[] slice))
            {
                slice = new SoilType[this.YMax + 1];
                this.Slices[x] = slice;
            }

            return slice;
        }
    }
}
