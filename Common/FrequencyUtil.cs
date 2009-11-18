using System;

namespace SEOToolSet.Common
{
    public static class FrequencyUtil
    {
        public static DateTime GetNextScheduledRunDate(DateTime startDate, Frequencies frequency)
        {
            switch (frequency)
            {
                case Frequencies.Week:
                    return startDate.AddDays(7);
                case Frequencies.TwoWeeks:
                    return startDate.AddDays(14);
                case Frequencies.Month:
                    return startDate.AddMonths(1);
                default:
                    break;
            }
            return startDate;
        }
        public enum Frequencies
        {
            Week = 1,
            TwoWeeks = 2,
            Month = 3
        }
    }
}
