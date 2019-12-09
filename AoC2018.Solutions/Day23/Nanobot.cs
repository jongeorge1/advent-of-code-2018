namespace AoC2018.Solutions.Day23
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;

    public class Nanobot
    {
        public Nanobot(string definition)
        {
            string[] components = definition.Split(new[] { ", " }, StringSplitOptions.None);
            int[] positionComponents = components[0].Remove(0, 5).TrimEnd('>').Split(new[] { ',' }, StringSplitOptions.None).Select(int.Parse).ToArray();

            this.Radius = int.Parse(components[1].Remove(0, 2));
            this.Position = (positionComponents[0], positionComponents[1], positionComponents[2]);
            this.ContainingBox = new Box
            {
                Corner1 = (this.Position.X - this.Radius, this.Position.Y - this.Radius, this.Position.Z - this.Radius),
                Corner2 = (this.Position.X + this.Radius, this.Position.Y + this.Radius, this.Position.Z + this.Radius),
            };
        }

        public int Radius { get; }

        public (int X, int Y, int Z) Position { get; }

        public Box ContainingBox { get; }

        public bool InRangeOf(Nanobot target)
        {
            int distance = this.DistanceFrom(target.Position.X, target.Position.Y, target.Position.Z);

            return distance <= target.Radius;
        }

        public bool RangeContainsPoint(int x, int y, int z)
        {
            int distance = this.DistanceFrom(x, y, z);

            return distance <= this.Radius;
        }

        public bool RangeIntersectsBox(Box box)
        {
            return (this.DistanceFromRange(this.Position.X, box.Corner1.X, box.Corner2.X)
                + this.DistanceFromRange(this.Position.Y, box.Corner1.Y, box.Corner2.Y)
                + this.DistanceFromRange(this.Position.Z, box.Corner1.Z, box.Corner2.Z)) <= this.Radius;
        }

        private int DistanceFromRange(int val, int min, int max)
        {
            if (val < min)
            {
                return Math.Abs(min - val);
            }

            if (val > max)
            {
                return Math.Abs(val - max);
            }

            return 0;
        }

        private int DistanceFrom(int x, int y, int z)
        {
            return Math.Abs(x - this.Position.X)
                + Math.Abs(y - this.Position.Y)
                + Math.Abs(z - this.Position.Z);
        }
    }
}
