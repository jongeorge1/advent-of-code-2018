namespace AoC2018.Solutions.Day13
{
    using System.Collections.Generic;

    public class Minecart
    {
        public int Position { get; set; }

        public int NextMove { get; set; }

        public int NextIntersectionBehavior { get; set; } = 0;

        public static List<Minecart> FindInMap(ref string map, int yOffset)
        {
            var minecarts = new List<Minecart>();

            FindMinecarts(ref map, minecarts, '<', '-', -1);
            FindMinecarts(ref map, minecarts, '>', '-', 1);
            FindMinecarts(ref map, minecarts, '^', '|', -yOffset);
            FindMinecarts(ref map, minecarts, 'v', '|', yOffset);

            return minecarts;
        }

        private static void FindMinecarts(ref string map, List<Minecart> minecarts, char minecart, char replacement, int nextMove)
        {
            int index = map.IndexOf(minecart);

            while (index != -1)
            {
                minecarts.Add(new Minecart
                {
                    Position = index,
                    NextMove = nextMove,
                });

                index = map.IndexOf(minecart, index + 1);
            }

            map = map.Replace(minecart, replacement);
        }
    }
}
