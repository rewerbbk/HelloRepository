using System;
using NodaTime;

namespace Utility.Extensions
{
    public static class DateTimeExtensions
    {
        public static int YearDifference(this DateTime leftValue, DateTime rightValue)
        {
            int yearDiff = (int)Math.Abs(Period.Between(LocalDateTime.FromDateTime(leftValue), LocalDateTime.FromDateTime(rightValue)).Years);

            return yearDiff;
        }

        public static int? YearDifference(this DateTime leftValue, DateTime? rightValue)
        {
            if (rightValue == null)
            {
                return null;
            }

            return YearDifference(leftValue, rightValue.Value);
        }

        public static int? YearDifference(this DateTime leftValue, string rightValue)
        {
            DateTime right;

            if (rightValue == null || !DateTime.TryParse(rightValue, out right))
            {
                return null;
            }

            return YearDifference(leftValue, right);
        }

        public static int MonthDifference(this DateTime leftValue, DateTime rightValue)
        {
            int monthDiff = (int)Math.Abs(Period.Between(LocalDateTime.FromDateTime(leftValue), LocalDateTime.FromDateTime(rightValue)).Months);
            int yearDiff = (int)Math.Abs(Period.Between(LocalDateTime.FromDateTime(leftValue), LocalDateTime.FromDateTime(rightValue)).Years);

            return monthDiff + 12 * yearDiff;
        }

        public static int? MonthDifference(this DateTime leftValue, DateTime? rightValue)
        {
            if (rightValue == null)
            {
                return null;
            }

            return MonthDifference(leftValue, rightValue.Value);
        }

        public static int? MonthDifference(this DateTime leftValue, string rightValue)
        {
            DateTime right;

            if (rightValue == null || !DateTime.TryParse(rightValue, out right))
            {
                return null;
            }

            return MonthDifference(leftValue, right);
        }

        public static int DayDifference(this DateTime leftValue, DateTime rightValue)
        {
            var dtDiff = leftValue - rightValue;

            return (int)Math.Abs(dtDiff.Days);
        }

        public static int? DayDifference(this DateTime leftValue, DateTime? rightValue)
        {
            if (rightValue == null)
            {
                return null;
            }

            return DayDifference(leftValue, rightValue.Value);
        }

        public static int? DayDifference(this DateTime leftValue, string rightValue)
        {
            DateTime right;

            if (rightValue == null || !DateTime.TryParse(rightValue, out right))
            {
                return null;
            }

            return DayDifference(leftValue, right);
        }
    }

}