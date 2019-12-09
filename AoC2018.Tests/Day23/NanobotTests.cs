namespace AoC2018.Tests.Day23
{
    using AoC2018.Solutions.Day23;
    using NUnit.Framework;

    public class NanobotTests
    {
        [TestCase("pos=<0,0,0>, r=4", 0, 0, 0, 4)]
        [TestCase("pos=<1,0,0>, r=1", 1, 0, 0, 1)]
        [TestCase("pos=<4,0,0>, r=3", 4, 0, 0, 3)]
        [TestCase("pos=<18862884,-4064761,-150195280>, r=66700142", 18862884, -4064761, -150195280, 66700142)]

        public void CreationTests(string input, int x, int y, int z, int radius)
        {
            var sut = new Nanobot(input);

            Assert.AreEqual(x, sut.Position.X);
            Assert.AreEqual(y, sut.Position.Y);
            Assert.AreEqual(z, sut.Position.Z);
            Assert.AreEqual(radius, sut.Radius);
        }
    }
}
