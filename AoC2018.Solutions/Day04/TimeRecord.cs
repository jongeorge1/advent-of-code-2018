namespace AoC2018.Solutions.Day04
{
    using System;
    using System.Globalization;

    public class TimeRecord
    {
        public DateTime DateTime { get; set; }

        public int? GuardNumber { get; set; }

        public TimeRecordActivity Activity { get; set; }

        public static TimeRecord FromString(string input)
        {
            var result = new TimeRecord
            {
                DateTime = DateTime.ParseExact(input.Substring(1, 16), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
            };

            string log = input.Substring(19);
            if (log == "wakes up")
            {
                result.Activity = TimeRecordActivity.WakesUp;
            }
            else if (log == "falls asleep")
            {
                result.Activity = TimeRecordActivity.FallsAsleep;
            }
            else
            {
                result.Activity = TimeRecordActivity.StartsShift;
                result.GuardNumber = int.Parse(log.Split(' ')[1].Substring(1));
            }

            return result;
        }
    }
}
