namespace AoC2018.Tests.Day15
{
    using System.Collections.Generic;
    using System.Linq;
    using AoC2018.Solutions.Day15;
    using NUnit.Framework;

    public class StateExtensionTests
    {
        [TestCase("#######\r\n#E..G.#\r\n#...#.#\r\n#.G.#G#\r\n#######", 8, new[] { 9, 15 }, Description = "Walls above/left")]
        [TestCase("#######\r\n#E..G.#\r\n#...#.#\r\n#.G.#G#\r\n#######", 11, new[] { 10, 12 }, Description = "Walls above/below")]
        [TestCase("#######\r\n#E..G.#\r\n#...#.#\r\n#.G.#G#\r\n#######", 26, new[] { 19 }, Description = "Walls above/left/right")]
        [TestCase("#######\r\n#E..G.#\r\n#...#.#\r\n#.G.#G#\r\n#######", 16, new[] { 9, 15, 17 }, Description = "Unit below")]
        [TestCase("#######\r\n#E..G.#\r\n#...#E#\r\n#.G.#G#\r\n#######", 26, new int[0], Description = "Unit below")]
        public void GetAdjacentSpacesTests(string stateInput, int location, int[] expectedResults)
        {
            var state = State.Parse(stateInput);
            MapSpace startLocation = state.Map[location];
            MapSpace[] result = state.GetEmptyAdjacentSpaces(startLocation).ToArray();

            Assert.That(result, Has.Length.EqualTo(expectedResults.Length));
            IEnumerable<int> resultLocations = result.Select(x => x.Location);

            Assert.That(resultLocations, Is.EquivalentTo(expectedResults));
        }

        [TestCase("#########\r\n#G..G..G#\r\n#.......#\r\n#.......#\r\n#G..E..G#\r\n#.......#\r\n#.......#\r\n#G..G..G#\r\n#########", 1, "#########\r\n#.G...G.#\r\n#...G...#\r\n#...E..G#\r\n#.G.....#\r\n#.......#\r\n#G..G..G#\r\n#.......#\r\n#########")]
        [TestCase("#########\r\n#G..G..G#\r\n#.......#\r\n#.......#\r\n#G..E..G#\r\n#.......#\r\n#.......#\r\n#G..G..G#\r\n#########", 2, "#########\r\n#..G.G..#\r\n#...G...#\r\n#.G.E.G.#\r\n#.......#\r\n#G..G..G#\r\n#.......#\r\n#.......#\r\n#########")]
        [TestCase("#########\r\n#G..G..G#\r\n#.......#\r\n#.......#\r\n#G..E..G#\r\n#.......#\r\n#.......#\r\n#G..G..G#\r\n#########", 3, "#########\r\n#.......#\r\n#..GGG..#\r\n#..GEG..#\r\n#G..G...#\r\n#......G#\r\n#.......#\r\n#.......#\r\n#########")]
        public void RoundTests(string initialState, int numberOfRounds, string expectedState)
        {
            var state = State.Parse(initialState);

            for (int i = 0; i < numberOfRounds; i++)
            {
                state.Round();
            }

            var expected = State.Parse(expectedState);

            Assert.That(state.ToString(), Is.EqualTo(expected.ToString()));
        }
    }
}
