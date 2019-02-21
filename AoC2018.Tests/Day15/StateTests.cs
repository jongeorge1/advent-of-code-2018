namespace AoC2018.Tests.Day15
{
    using AoC2018.Solutions.Day15;
    using NUnit.Framework;

    public class StateTests
    {
        [Test]
        public void ParseSetsYOffsetCorrectly()
        {
            var state = State.Parse("#######\r\n#.G.E.#\r\n#E.G.E#\r\n#.G.E.#\r\n#######");

            Assert.That(state.YOffset, Is.EqualTo(7));
        }

        [Test]
        public void ParseSetsYMaxCorrectly()
        {
            var state = State.Parse("#######\r\n#.G.E.#\r\n#E.G.E#\r\n#.G.E.#\r\n#######");

            Assert.That(state.MaxY, Is.EqualTo(5));
        }

        [Test]
        public void ParseSetsWallsCorrectly()
        {
            var state = State.Parse("#######\r\n#.G.E.#\r\n#E.G.E#\r\n#.G.E.#\r\n#######");

            int[] wallLocations = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 13, 14, 20, 21, 27, 28, 29, 30, 31, 32, 33, 34 };

            foreach (int current in wallLocations)
            {
                Assert.That(state.Map[current], Is.Null);
            }
        }

        [Test]
        public void ParseSetsSpacesCorrectly()
        {
            var state = State.Parse("#######\r\n#.G.E.#\r\n#E.G.E#\r\n#.G.E.#\r\n#######");

            int[] spaceLocations = new[] { 8, 9, 10, 11, 12, 15, 16, 17, 18, 19, 22, 23, 24, 25, 26 };

            foreach (int current in spaceLocations)
            {
                Assert.That(state.Map[current], Is.InstanceOf<MapSpace>());
            }
        }

        [Test]
        public void ParseSetsElvesCorrectly()
        {
            var state = State.Parse("#######\r\n#.G.E.#\r\n#E.G.E#\r\n#.G.E.#\r\n#######");

            int[] elfLocations = new[] { 11, 15, 19, 25 };

            foreach (int current in elfLocations)
            {
                Assert.That(state.Map[current].Unit, Is.InstanceOf<Elf>());
            }
        }

        [Test]
        public void ParseSetsGoblinsCorrectly()
        {
            var state = State.Parse("#######\r\n#.G.E.#\r\n#E.G.E#\r\n#.G.E.#\r\n#######");

            int[] goblinLocations = new[] { 9, 17, 23 };

            foreach (int current in goblinLocations)
            {
                Assert.That(state.Map[current].Unit, Is.InstanceOf<Goblin>());
            }
        }
    }
}
