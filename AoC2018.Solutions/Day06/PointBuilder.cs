namespace AoC2018.Solutions.Day06
{
    using System;
    using System.Drawing;

    public static class PointBuilder
    {
        public static Point FromString(string input)
        {
            string[] components = input.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new Point
            {
                X = int.Parse(components[0]),
                Y = int.Parse(components[1]),
            };
        }
    }
}
