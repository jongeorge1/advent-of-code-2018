namespace AoC2018.Solutions.Day12
{
    using System.Collections.Generic;
    using System.Linq;

    public class State
    {
        public List<int> PotsContainingPlants { get; set; }

        public static State Parse(string input)
        {
            return new State
            {
                PotsContainingPlants = Enumerable.Range(0, input.Length).Where(x => input[x] == '#').ToList(),
            };
        }

        public override string ToString()
        {
            return string.Join(",", this.PotsContainingPlants.OrderBy(x => x));
        }
    }
}
