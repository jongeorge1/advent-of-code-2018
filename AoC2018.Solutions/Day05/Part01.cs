namespace AoC2018.Solutions.Day05
{
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            string result = PolymerReactor.React(input);
            return result.Length.ToString();
        }
    }
}
