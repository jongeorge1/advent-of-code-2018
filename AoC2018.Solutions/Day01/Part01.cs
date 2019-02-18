namespace AoC2018.Solutions.Day01
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            return input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).Sum().ToString();
        }
    }
}
