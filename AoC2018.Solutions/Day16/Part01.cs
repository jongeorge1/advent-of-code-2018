namespace AoC2018.Solutions.Day16
{
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            (Sample[] Samples, Instruction[] instructions) data = Parser.Parse(input);

            IEnumerable<(Sample x, int)> testResults = data.Samples.Select(x => (x, x.GetMatchingOperationsCount()));

            int result = testResults.Count(x => x.Item2 >= 3);

            return result.ToString();
        }
    }
}
