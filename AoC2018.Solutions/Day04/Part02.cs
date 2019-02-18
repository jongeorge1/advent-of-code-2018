namespace AoC2018.Solutions.Day04
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var timeRecords = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(TimeRecord.FromString).ToList();
            timeRecords = this.SortAndPopulateGuardNumbers(timeRecords);

            int[] guardIds = timeRecords.Where(x => x.GuardNumber.HasValue).Select(x => x.GuardNumber.Value).Distinct().ToArray();
            var guardMinutesAsleep = guardIds.ToDictionary(x => x, _ => new int[60]);
            DateTime[] uniqueDates = timeRecords.Select(x => x.DateTime.Date).Distinct().ToArray();
            DateTime currentDate = DateTime.MinValue;
            int? currentGuard = null;
            int minuteLastFellAsleep = -1;

            foreach (TimeRecord current in timeRecords)
            {
                // Have we changed day or guard?
                if (currentDate != current.DateTime.Date || currentGuard != current.GuardNumber)
                {
                    currentDate = current.DateTime.Date;
                    currentGuard = current.GuardNumber;
                }

                // Has the sleeping status changed?
                if (current.Activity == TimeRecordActivity.FallsAsleep)
                {
                    minuteLastFellAsleep = current.DateTime.Minute;
                }
                else if (current.Activity == TimeRecordActivity.WakesUp)
                {
                    // Record the minutes they were sleeping and reset.
                    for (int minute = minuteLastFellAsleep; minute < current.DateTime.Minute; minute++)
                    {
                        guardMinutesAsleep[currentGuard.Value][minute]++;
                    }
                }
            }

            // Now, for each guard, find the minute they slept most during
            IEnumerable<(int GuardId, int MinuteWithMostSleeps, int MaximumTimesAsleep)> guardSleepiestMinutes = guardMinutesAsleep.Select(x =>
                {
                    int maximumTimesAsleep = x.Value.Max();
                    int minuteWithMostSleeps = Array.IndexOf(x.Value, maximumTimesAsleep);
                    return (x.Key, minuteWithMostSleeps, maximumTimesAsleep);
                });

            // And find the guard most often asleep on the same minute
            (int GuardId, int MinuteWithMostSleeps, int MaximumTimesAsleep) guardMostOftenAsleepOnSameMinute = guardSleepiestMinutes.OrderByDescending(x => x.MaximumTimesAsleep).First();

            return (guardMostOftenAsleepOnSameMinute.GuardId * guardMostOftenAsleepOnSameMinute.MinuteWithMostSleeps).ToString();
        }

        private List<TimeRecord> SortAndPopulateGuardNumbers(List<TimeRecord> timeRecords)
        {
            var result = timeRecords.OrderBy(x => x.DateTime).ToList();
            int? currentGuardNumber = null;

            foreach (TimeRecord current in result)
            {
                if (current.GuardNumber.HasValue)
                {
                    currentGuardNumber = current.GuardNumber;
                }
                else
                {
                    current.GuardNumber = currentGuardNumber;
                }
            }

            return result;
        }
    }
}
