namespace AoC2018.Tests.Day15
{
    using System.Collections.Generic;
    using System.Linq;
    using AoC2018.Solutions.Day15;
    using NUnit.Framework;

    public class UnitExtensionTests
    {
        [Test]
        public void FindInRangeSpacesTests()
        {
            var state = State.Parse("#######\r\n#E..G.#\r\n#...#.#\r\n#.G.#G#\r\n#######");

            // Get the elf in the top left corner
            Unit targetUnit = state.Map[8].Unit;

            MapSpace[] inRangeSpaces = targetUnit.FindSpacesInRangeOfAnEnemy(state);
            IEnumerable<int> inRangeLocations = inRangeSpaces.Select(x => x.Location);
            Assert.That(inRangeLocations, Is.EquivalentTo(new[] { 10, 12, 16, 19, 22, 24 }));
        }

        [TestCase("#######\r\n#E..G.#\r\n#...#.#\r\n#.G.#G#\r\n#######", 8, new[] { 8, 9, 10 })]
        [TestCase("#######\r\n#.E...#\r\n#...?.#\r\n#..?G?#\r\n#######", 9, new[] { 9, 10, 11, 18 })]
        public void GetPathToClosestTests(string input, int startLocation, int[] expectedPath)
        {
            var state = State.Parse(input);

            // Get the elf in the top left corner
            Unit targetUnit = state.Map[startLocation].Unit;
            MapSpace[] inRangeSpaces = targetUnit.FindSpacesInRangeOfAnEnemy(state);
            MapSpace[] path = targetUnit.GetPathToClosest(inRangeSpaces, state);
            IEnumerable<int> pathLocations = path.Select(x => x.Location);

            Assert.That(pathLocations, Is.EquivalentTo(expectedPath));
        }
    }
}
