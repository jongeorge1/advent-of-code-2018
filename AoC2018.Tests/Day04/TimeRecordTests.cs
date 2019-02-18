namespace AoC2018.Tests.Day04
{
    using System;
    using AoC2018.Solutions.Day04;
    using NUnit.Framework;

    public class TimeRecordTests
    {
        [Test]
        public void CreationFromStartsShiftTimeRecordString()
        {
            var result = TimeRecord.FromString("[1518-11-14 00:02] Guard #3011 begins shift");

            Assert.That(result.DateTime, Is.EqualTo(new DateTime(1518, 11, 14, 0, 2, 0)));
            Assert.That(result.Activity, Is.EqualTo(TimeRecordActivity.StartsShift));
            Assert.That(result.GuardNumber, Is.EqualTo(3011));
        }

        [Test]
        public void CreationFromFallsAsleepTimeRecordString()
        {
            var result = TimeRecord.FromString("[1518-11-01 23:58] falls asleep");

            Assert.That(result.DateTime, Is.EqualTo(new DateTime(1518, 11, 01, 23, 58, 0)));
            Assert.That(result.Activity, Is.EqualTo(TimeRecordActivity.FallsAsleep));
            Assert.That(result.GuardNumber, Is.Null);
        }

        [Test]
        public void CreationFromWakesUpTimeRecordString()
        {
            var result = TimeRecord.FromString("[1518-06-09 00:52] wakes up");

            Assert.That(result.DateTime, Is.EqualTo(new DateTime(1518, 06, 09, 0, 52, 0)));
            Assert.That(result.Activity, Is.EqualTo(TimeRecordActivity.WakesUp));
            Assert.That(result.GuardNumber, Is.Null);
        }
    }
}
