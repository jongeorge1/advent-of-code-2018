namespace AoC2018.Solutions.Day18
{
    using System;
    using System.Linq;

    public static class MapParser
    {
        public static (char[] Map, int yOffset) Parse(string input)
        {
            string[] rows = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int yOffset = rows[0].Length;
            char[] map = rows.SelectMany(x => x.ToCharArray()).ToArray();

            return (map, yOffset);
        }
    }
}
