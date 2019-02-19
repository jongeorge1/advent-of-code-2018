namespace AoC2018.Solutions.Day12
{
    using System.Linq;

    public class Rule
    {
        public bool[] Pattern { get; set; }

        public bool Result { get; set; }

        public static Rule Parse(string input)
        {
            bool[] pattern = input.Substring(0, 5).Select(x => x == '#').ToArray();
            bool result = input.Last() == '#';

            return new Rule { Pattern = pattern, Result = result };
        }
    }
}
