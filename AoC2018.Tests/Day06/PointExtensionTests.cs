namespace AoC2018.Tests.Day06
{
    using System.Drawing;
    using AoC2018.Solutions.Day06;
    using NUnit.Framework;

    public class PointExtensionTests
    {
        private static readonly Point[] Points = new[]
            {
                new Point { X = 1, Y = 1 },
                new Point { X = 1, Y = 6 },
                new Point { X = 8, Y = 3 },
                new Point { X = 3, Y = 4 },
                new Point { X = 5, Y = 5 },
                new Point { X = 8, Y = 9 },
            };

        [Test]
        public void FindClosestWithSingleMatch()
        {
            var testPoint = new Point { X = 2, Y = 2 };

            Point[] closest = testPoint.FindClosest(Points);

            Assert.That(closest.Length, Is.EqualTo(1));
        }

        [Test]
        public void FindClosestWithExactMatch()
        {
            var testPoint = new Point { X = 1, Y = 1 };

            Point[] closest = testPoint.FindClosest(Points);

            Assert.That(closest.Length, Is.EqualTo(1));
        }

        [Test]
        public void FindClosestWithMultipleMatches()
        {
            var testPoint = new Point { X = 1, Y = 4 };

            Point[] closest = testPoint.FindClosest(Points);

            Assert.That(closest.Length, Is.EqualTo(2));
        }
    }
}
