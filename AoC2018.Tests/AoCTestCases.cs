// <copyright file="Day01Part01Tests.cs" company="Endjin">
// Copyright (c) Endjin. All rights reserved.
// </copyright>

namespace Tests
{
    using AoC2018.Solutions;
    using NUnit.Framework;

    public class AoCTestCases
    {
        [TestCase(1, 1, "+1\r\n-2\r\n+3\r\n+1", "3")]
        [TestCase(1, 1, "+1\r\n+1\r\n+1", "3")]
        [TestCase(1, 1, "+1\r\n+1\r\n-2", "0")]
        [TestCase(1, 1, "-1\r\n-2\r\n-3", "-6")]
        [TestCase(1, 2, "+1\r\n-1", "0")]
        [TestCase(1, 2, "+3\r\n+3\r\n+4\r\n-2\r\n-4", "10")]
        [TestCase(1, 2, "-6\r\n+3\r\n+8\r\n+5\r\n-6", "5")]
        [TestCase(1, 2, "+7\r\n+7\r\n-2\r\n-7\r\n-4", "14")]
        [TestCase(2, 1, "abcdef\r\nbababc\r\nabbcde\r\nabcccd\r\naabcdd\r\nabcdee\r\nababab", "12")]
        [TestCase(2, 2, "abcde\r\nfghij\r\nklmno\r\npqrst\r\nfguij\r\naxcye\r\nwvxyz", "fgij")]
        [TestCase(3, 1, "#1 @ 1,3: 4x4\r\n#2 @ 3,1: 4x4\r\n#3 @ 5,5: 2x2", "4")]
        [TestCase(3, 2, "#1 @ 1,3: 4x4\r\n#2 @ 3,1: 4x4\r\n#3 @ 5,5: 2x2", "3")]
        [TestCase(4, 1, "[1518-11-01 00:00] Guard #10 begins shift\r\n[1518-11-01 00:05] falls asleep\r\n[1518-11-01 00:25] wakes up\r\n[1518-11-01 00:30] falls asleep\r\n[1518-11-01 00:55] wakes up\r\n[1518-11-01 23:58] Guard #99 begins shift\r\n[1518-11-02 00:40] falls asleep\r\n[1518-11-02 00:50] wakes up\r\n[1518-11-03 00:05] Guard #10 begins shift\r\n[1518-11-03 00:24] falls asleep\r\n[1518-11-03 00:29] wakes up\r\n[1518-11-04 00:02] Guard #99 begins shift\r\n[1518-11-04 00:36] falls asleep\r\n[1518-11-04 00:46] wakes up\r\n[1518-11-05 00:03] Guard #99 begins shift\r\n[1518-11-05 00:45] falls asleep\r\n[1518-11-05 00:55] wakes up", "240")]
        [TestCase(4, 2, "[1518-11-01 00:00] Guard #10 begins shift\r\n[1518-11-01 00:05] falls asleep\r\n[1518-11-01 00:25] wakes up\r\n[1518-11-01 00:30] falls asleep\r\n[1518-11-01 00:55] wakes up\r\n[1518-11-01 23:58] Guard #99 begins shift\r\n[1518-11-02 00:40] falls asleep\r\n[1518-11-02 00:50] wakes up\r\n[1518-11-03 00:05] Guard #10 begins shift\r\n[1518-11-03 00:24] falls asleep\r\n[1518-11-03 00:29] wakes up\r\n[1518-11-04 00:02] Guard #99 begins shift\r\n[1518-11-04 00:36] falls asleep\r\n[1518-11-04 00:46] wakes up\r\n[1518-11-05 00:03] Guard #99 begins shift\r\n[1518-11-05 00:45] falls asleep\r\n[1518-11-05 00:55] wakes up", "4455")]
        [TestCase(5, 1, "aA", "0")]
        [TestCase(5, 1, "abBA", "0")]
        [TestCase(5, 1, "abAB", "4")]
        [TestCase(5, 1, "aabAAB", "6")]
        [TestCase(5, 1, "dabAcCaCBAcCcaDA", "10")]
        [TestCase(5, 2, "dabAcCaCBAcCcaDA", "4")]
        [TestCase(6, 1, "1, 1\r\n1, 6\r\n8, 3\r\n3, 4\r\n5, 5\r\n8, 9", "17")]
        [TestCase(7, 1, "Step C must be finished before step A can begin.\r\nStep C must be finished before step F can begin.\r\nStep A must be finished before step B can begin.\r\nStep A must be finished before step D can begin.\r\nStep B must be finished before step E can begin.\r\nStep D must be finished before step E can begin.\r\nStep F must be finished before step E can begin.", "CABDFE")]
        [TestCase(8, 1, "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2", "138")]
        [TestCase(8, 2, "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2", "66")]
        public void Tests(int day, int part, string input, string expectedResult)
        {
            ISolution solution = SolutionFactory.GetSolution(day, part);
            string result = solution.Solve(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Day06Part02()
        {
            var solution = new AoC2018.Solutions.Day06.Part02();
            string result = solution.Solve("1, 1\r\n1, 6\r\n8, 3\r\n3, 4\r\n5, 5\r\n8, 9", 32);
            Assert.That(result, Is.EqualTo("16"));
        }

        [Test]
        public void Day07Part02()
        {
            var solution = new AoC2018.Solutions.Day07.Part02();
            string result = solution.Solve("Step C must be finished before step A can begin.\r\nStep C must be finished before step F can begin.\r\nStep A must be finished before step B can begin.\r\nStep A must be finished before step D can begin.\r\nStep B must be finished before step E can begin.\r\nStep D must be finished before step E can begin.\r\nStep F must be finished before step E can begin.", 2, 0);
            Assert.That(result, Is.EqualTo("15"));
        }
    }
}