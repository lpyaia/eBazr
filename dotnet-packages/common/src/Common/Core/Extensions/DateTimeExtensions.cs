using Common.Core.Helpers;
using FluentAssertions.Common;
using System;

namespace Common.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static long GetTotalMinutes(this DateTime value, DateTime endTime)
        {
            return (long)Math.Round((endTime - value).TotalMinutes, MidpointRounding.AwayFromZero);
        }

        public static DateTime TrimMilliseconds(this DateTime value) => new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, 0);

        public static DateTimeOffset ToBrazilOffset(this DateTime value)
        {
            return value.ToDateTimeOffset(DateHelper.BrazilTimeZone.GetUtcOffset(value));
        }

        public static DateTime ToUtcFromBrazilDate(this DateTime value)
        {
            return value.ToBrazilOffset().ToUniversalTime().DateTime;
        }

        public static DateTime ToBrazilDateFromUtc(this DateTime value)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(value, DateHelper.BrazilTimeZone);
        }
    }
}