namespace AoC2018.Solutions.Day15
{
    using System.Collections.Generic;
    using System.Linq;

    public static class LocationHelper
    {
        public static (int X, int Y) GetCoordinates(int location, int yOffset)
        {
            return (location % yOffset, location / yOffset);
        }

        public static int GetLocation(int x, int y, int yOffset)
        {
            return x + (y * yOffset);
        }
    }
}
