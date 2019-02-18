namespace AoC2018.Solutions.Day09
{
    using System;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var part1 = new Part01();

            string[] components = input.Split(' ');

            return part1.Solve(int.Parse(components[0]), int.Parse(components[6]) * 100).ToString();
        }
    }
}
