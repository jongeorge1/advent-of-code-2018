namespace AoC2018.Solutions.Day05
{
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            int bestResult = input.Length;

            // TODO: What can we learn about the Visual Studio Concurrency Visualiser
            // from this code?
            return ParallelEnumerable.Range('a', 26)
                .Min(c => PolymerReactor.React(
                    Regex.Replace(input, ((char)c).ToString(), string.Empty, RegexOptions.IgnoreCase))
                    .Length)
                .ToString();
        }
    }
}
