namespace AoC2018.Solutions.Day17
{
    using System.Drawing;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            Point[] clayPoints = Parser.Parse(input);

            var map = Map.FromClayPoints(clayPoints);

            ////map.WriteToConsole();

            int lastSquaresOfWater = 0;

            while (true)
            {
                map.AddWater();

                if (lastSquaresOfWater == map.WaterTilesCount)
                {
                    break;
                }

                lastSquaresOfWater = map.WaterTilesCount;
            }

            ////Console.WriteLine();
            ////Console.WriteLine();
            ////map.WriteToConsole();

            return map.CountWetTiles().ToString();
        }
    }
}
