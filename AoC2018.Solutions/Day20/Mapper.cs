namespace AoC2018.Solutions.Day20
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public static class Mapper
    {
        public static Dictionary<Point, Doors> Map(string input)
        {
            var map = new Dictionary<Point, Doors>();
            var currentLocation = new Point(0, 0);
            map.Add(currentLocation, Doors.None);

            var branchPoints = new Stack<Point>();

            for (int pointer = 1; pointer < (input.Length - 1); pointer++)
            {
                if (input[pointer] == '(')
                {
                    // We're at a branch point. Push the current location onto the stack to
                    // record where we branched from
                    branchPoints.Push(currentLocation);
                }
                else if (input[pointer] == ')')
                {
                    currentLocation = branchPoints.Pop();
                }
                else if (input[pointer] == '|')
                {
                    currentLocation = branchPoints.Peek();
                }
                else
                {
                    Point nextLocation;
                    Doors doorToNextLocation;

                    if (input[pointer] == 'N')
                    {
                        nextLocation = new Point(currentLocation.X, currentLocation.Y - 1);
                        doorToNextLocation = Doors.North;
                    }
                    else if (input[pointer] == 'S')
                    {
                        nextLocation = new Point(currentLocation.X, currentLocation.Y + 1);
                        doorToNextLocation = Doors.South;
                    }
                    else if (input[pointer] == 'E')
                    {
                        nextLocation = new Point(currentLocation.X + 1, currentLocation.Y);
                        doorToNextLocation = Doors.East;
                    }
                    else if (input[pointer] == 'W')
                    {
                        nextLocation = new Point(currentLocation.X - 1, currentLocation.Y);
                        doorToNextLocation = Doors.West;
                    }
                    else
                    {
                        throw new ArgumentException();
                    }

                    map.Ensure(nextLocation);
                    map[currentLocation] |= doorToNextLocation;
                    map[nextLocation] |= doorToNextLocation.Opposite();
                    currentLocation = nextLocation;
                }
            }

            return map;
        }

        public static void Ensure(this Dictionary<Point, Doors> map, Point point)
        {
            if (!map.ContainsKey(point))
            {
                map[point] = Doors.None;
            }
        }

        public static Doors Opposite(this Doors door)
        {
            switch (door)
            {
                case Doors.North:
                    return Doors.South;

                case Doors.South:
                    return Doors.North;

                case Doors.East:
                    return Doors.West;

                case Doors.West:
                    return Doors.East;

                default:
                    throw new ArgumentException();
            }
        }

        public static IEnumerable<Point> GetAccessibleLocationsFrom(this Dictionary<Point, Doors> map, Point point)
        {
            Doors doors = map[point];

            if ((doors & Doors.North) == Doors.North)
            {
                yield return new Point(point.X, point.Y - 1);
            }

            if ((doors & Doors.West) == Doors.West)
            {
                yield return new Point(point.X - 1, point.Y);
            }

            if ((doors & Doors.South) == Doors.South)
            {
                yield return new Point(point.X, point.Y + 1);
            }

            if ((doors & Doors.East) == Doors.East)
            {
                yield return new Point(point.X + 1, point.Y);
            }
        }
    }
}
