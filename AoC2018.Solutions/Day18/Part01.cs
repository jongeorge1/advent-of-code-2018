namespace AoC2018.Solutions.Day18
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            (char[] Map, int yOffset) map = MapParser.Parse(input);

            ////Console.WriteLine("Initial state:");
            ////map.WriteToConsole();

            for (int i = 0; i < 10; i++)
            {
                map = map.GetNextState();

                ////Console.WriteLine($"After {i + 1} minute:");
                ////map.WriteToConsole();
            }

            int woodCount = map.Map.Count(x => x == MapAcre.Trees);
            int lumberYardCount = map.Map.Count(x => x == MapAcre.Lumberyard);

            return (woodCount * lumberYardCount).ToString();
        }
    }
}
