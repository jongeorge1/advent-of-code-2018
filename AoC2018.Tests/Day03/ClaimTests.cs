namespace AoC2018.Tests.Day03
{
    using AoC2018.Solutions.Day03;
    using NUnit.Framework;

    public class ClaimTests
    {
        [TestCase("#1 @ 1,3: 4x4", 1, 1, 3, 4, 4, 4, 6)]
        public void CreationFromClaimString(string input, int expectedNumber, int expectedX, int expectedY, int expectedWidth, int expectedHeight, int expectedMaxX, int expectedMaxY)
        {
            var claim = Claim.FromString(input);
            Assert.That(claim.Number, Is.EqualTo(expectedNumber));
            Assert.That(claim.Position.X, Is.EqualTo(expectedX));
            Assert.That(claim.Position.Y, Is.EqualTo(expectedY));
            Assert.That(claim.Size.Width, Is.EqualTo(expectedWidth));
            Assert.That(claim.Size.Height, Is.EqualTo(expectedHeight));
        }
    }
}
