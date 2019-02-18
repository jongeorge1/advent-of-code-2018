namespace AoC2018.Solutions.Day09
{
    using System.Text;

    public static class MarbleExtensions
    {
        public static Marble Clockwise(this Marble marble, int count)
        {
            Marble current = marble;
            for (int i = 0; i < count; i++)
            {
                current = current.Clockwise;
            }

            return current;
        }

        public static Marble CounterClockwise(this Marble marble, int count)
        {
            Marble current = marble;
            for (int i = 0; i < count; i++)
            {
                current = current.CounterClockwise;
            }

            return current;
        }

        public static Marble InsertAfter(this Marble marble, int number)
        {
            Marble next = marble.Clockwise;
            var newMarble = new Marble
            {
                Number = number,
                Clockwise = next,
                CounterClockwise = marble,
            };

            marble.Clockwise = newMarble;
            next.CounterClockwise = newMarble;

            return newMarble;
        }

        public static Marble Remove(this Marble marble)
        {
            Marble prev = marble.CounterClockwise;
            Marble next = marble.Clockwise;

            prev.Clockwise = next;
            next.CounterClockwise = prev;

            marble.Clockwise = null;
            marble.CounterClockwise = null;

            return marble;
        }

        public static string Visualise(this Marble start, Marble current)
        {
            var visualisation = new StringBuilder();

            Marble active = start;

            do
            {
                if (active == current)
                {
                    visualisation.Append("(");
                    visualisation.Append(active.Number);
                    visualisation.Append(")");
                }
                else
                {
                    visualisation.Append(active.Number);
                }

                visualisation.Append(" ");
                active = active.Clockwise;
            }
            while (active != start);

            return visualisation.ToString();
        }
    }
}
