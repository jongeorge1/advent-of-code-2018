
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2018.Solutions.Day23
{
    public class Box
    {
        public (int X, int Y, int Z) Corner1 { get; set; }

        public (int X, int Y, int Z) Corner2 { get; set; }

        public int NanobotsInRange { get; set; }

        public int EdgeLength => this.Corner2.X - this.Corner1.X + 1;

        public IEnumerable<Box> Decompose()
        {
            int newEdgeLength = this.EdgeLength / 2;

            int[] newXMins = new[] { this.Corner1.X, this.Corner1.X + newEdgeLength };
            int[] newYMins = new[] { this.Corner1.Y, this.Corner1.Y + newEdgeLength };
            int[] newZMins = new[] { this.Corner1.Z, this.Corner1.Z + newEdgeLength };

            return newXMins.SelectMany(x =>
                newYMins.SelectMany(y =>
                    newZMins.Select(z => new Box
                    {
                        Corner1 = (x, y, z),
                        Corner2 = (x + newEdgeLength - 1, y + newEdgeLength - 1, z + newEdgeLength - 1),
                    })));
        }

        public bool Intersects(Box other)
        {
            if (this.Corner2.X < other.Corner1.X)
            {
                return false;
            }

            if (other.Corner2.X < this.Corner1.X)
            {
                return false;
            }

            if (this.Corner2.Y < other.Corner1.Y)
            {
                return false;
            }

            if (other.Corner2.Y < this.Corner1.Y)
            {
                return false;
            }

            if (this.Corner2.Z < other.Corner1.Z)
            {
                return false;
            }

            if (other.Corner2.Z < this.Corner1.Z)
            {
                return false;
            }

            return true;
        }
    }
}
