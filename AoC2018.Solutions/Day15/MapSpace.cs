namespace AoC2018.Solutions.Day15
{
    using System;

    public class MapSpace
    {
        public int Location { get; set; }

        public Unit Unit { get; set; }

        public static MapSpace Parse(char input, int location)
        {
            var result = new MapSpace();

            switch (input)
            {
                case '#':
                    return null;

                case '.':
                    break;

                case 'E':
                    result.Unit = new Elf { CurrentLocation = result };
                    break;

                case 'G':
                    result.Unit = new Goblin { CurrentLocation = result };
                    break;
            }

            result.Location = location;
            return result;
        }

        public override string ToString()
        {
            return this.Unit?.ToString() ?? ".";
        }
    }
}
