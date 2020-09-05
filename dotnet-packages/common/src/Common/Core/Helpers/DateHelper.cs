using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Common.Core.Helpers
{
    public static class DateHelper
    {
        public const string DefaultFormat = "yyyy-MM-dd HH:mm:ss";

        public static TimeZoneInfo BrazilTimeZone = GetBrazilTimeZone();

        [ExcludeFromCodeCoverage]
        private static TimeZoneInfo GetBrazilTimeZone()
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            return isWindows ? TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")
                : TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
        }
    }
}