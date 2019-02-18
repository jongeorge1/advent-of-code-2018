namespace AoC2018.Solutions.Day04
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
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

            // Now we know which minutes each guard was asleep... find out which guard
            // slept the most
            var totalGuardSleepTime = guardMinutesAsleep.ToDictionary(x => x.Key, x => x.Value.Sum());
            IOrderedEnumerable<int> guardsBySleepTime = guardIds.OrderByDescending(x => totalGuardSleepTime[x]);
            int sleepiestGuard = guardsBySleepTime.First();

            int guardMaximumMinuteSleeps = guardMinutesAsleep[sleepiestGuard].Max();
            int guardSleepiestMinute = Array.IndexOf(guardMinutesAsleep[sleepiestGuard], guardMaximumMinuteSleeps);

            return (sleepiestGuard * guardSleepiestMinute).ToString();
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
