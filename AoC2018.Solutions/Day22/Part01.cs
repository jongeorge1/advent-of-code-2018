namespace AoC2018.Solutions.Day22
{
    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var map = Map.FromInput(input);

            map.WriteToConsole();

            // Now sum up all the risk levels
            int sum = 0;
            for (int x = 0; x < map.RiskLevels.GetLength(0); x++)
            {
                for (int y = 0; y < map.RiskLevels.GetLength(1); y++)
                {
                    sum += map.RiskLevels[x, y];
                }
            }

            return sum.ToString();
        }
    }
}
