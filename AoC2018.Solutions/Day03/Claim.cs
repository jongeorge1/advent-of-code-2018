namespace AoC2018.Solutions.Day03
{
    using System.Drawing;
    using System.Text.RegularExpressions;

    public class Claim
    {
        private static readonly Regex ParserRegex = new Regex(@"#(\d*) @ (\d*),(\d*): (\d*)x(\d*)", RegexOptions.Compiled);

        public int Number { get; set; }

        public Point Position { get; set; }

        public Size Size { get; set; }

        public static Claim FromString(string input)
        {
            Match matches = ParserRegex.Match(input);

            int x = int.Parse(matches.Groups[2].Value);
            int y = int.Parse(matches.Groups[3].Value);
            int width = int.Parse(matches.Groups[4].Value);
            int height = int.Parse(matches.Groups[5].Value);

            return new Claim
            {
                Number = int.Parse(matches.Groups[1].Value),
                Position = new Point(x, y),
                Size = new Size(width, height),
            };
        }
    }
}
