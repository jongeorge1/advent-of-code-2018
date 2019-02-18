namespace AoC2018.Solutions.Day09
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            string[] components = input.Split(' ');

            return this.Solve(int.Parse(components[0]), int.Parse(components[6])).ToString();
        }

        public long Solve(int players, int marbles)
        {
            long[] scores = new long[players];

            // Set up Marble 0.
            // We're going to solve this using a doubly linked list
            var marble0 = new Marble
            {
                Number = 0,
            };

            marble0.Clockwise = marble0;
            marble0.CounterClockwise = marble0;

            Marble currentMarble = marble0;
            int currentPlayer = 0;

            for (int i = 1; i <= marbles; i++)
            {
                if (i % 23 == 0)
                {
                    // Current player keeps this marble
                    scores[currentPlayer] += i;

                    // Also keeps the marble 7 counter clockwise
                    Marble marbleToRemove = currentMarble.CounterClockwise(7);
                    currentMarble = marbleToRemove.Clockwise;
                    scores[currentPlayer] += marbleToRemove.Number;
                    marbleToRemove.Remove();
                }
                else
                {
                    currentMarble = currentMarble.Clockwise(1).InsertAfter(i);
                }

                currentPlayer = ++currentPlayer % players;
            }

            return scores.Max();
        }
    }
}
