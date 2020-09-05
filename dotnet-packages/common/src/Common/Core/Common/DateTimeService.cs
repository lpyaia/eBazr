using System;

namespace Common.Core.Common
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now()
        {
            return DateTime.UtcNow;
        }
    }
}