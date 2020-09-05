using Common.Core.Helpers;
using System;
using System.Globalization;
using System.Linq;

namespace Common.Core.Extensions
{
    public static class ParseExtensions
    {
        #region ToDateTime

        public static DateTime ToDateTime(this string input) => ToDateTime(input, CultureInfo.InvariantCulture);

        public static DateTime ToDateTimeFromBrazilFormat(this string input) => ToDateTime(input, CultureHelper.BrazilCulture);

        private static DateTime ToDateTime(this string input, IFormatProvider provider)
        {
            return DateTime.TryParse(input, provider, DateTimeStyles.None, out var date) ? date : DateTime.MinValue;
        }

        public static DateTime? ToNDateTime(this string input) => ToNDateTime(input, CultureInfo.InvariantCulture);

        public static DateTime? ToNDateTimeFromBrazilFormat(this string input) => ToNDateTime(input, CultureHelper.BrazilCulture);

        private static DateTime? ToNDateTime(this string input, IFormatProvider provider)
        {
            return DateTime.TryParse(input, provider, DateTimeStyles.None, out var date) ? date : (DateTime?)null;
        }

        #endregion ToDateTime

        #region ToDecimal

        public static decimal ToDecimal(this string input) => ToDecimal(input, CultureInfo.InvariantCulture);

        public static decimal ToDecimalFromBrazilFormat(this string input) => ToDecimal(input, CultureHelper.BrazilCulture);

        public static decimal ToDecimalFromUndefinedFormat(this string input) => ToDecimal(FormatToInvarientNumber(input));

        private static decimal ToDecimal(this string input, IFormatProvider provider)
        {
            return decimal.TryParse(input, NumberStyles.Number, provider, out var result) ? result : 0;
        }

        public static decimal? ToNDecimal(this string input) => ToNDecimal(input, CultureInfo.InvariantCulture);

        public static decimal? ToNDecimalFromBrazilFormat(this string input) => ToNDecimal(input, CultureHelper.BrazilCulture);

        public static decimal? ToNDecimalFromUndefinedFormat(this string input) => ToNDecimal(FormatToInvarientNumber(input));

        private static decimal? ToNDecimal(this string input, IFormatProvider provider)
        {
            return decimal.TryParse(input, NumberStyles.Number, provider, out var result) ? result : (decimal?)null;
        }

        #endregion ToDecimal

        #region ToDouble

        public static double ToDouble(this string input) => ToDouble(input, CultureInfo.InvariantCulture);

        public static double ToDoubleFromBrazilFormat(this string input) => ToDouble(input, CultureHelper.BrazilCulture);

        public static double ToDoubleFromUndefinedFormat(this string input) => ToDouble(FormatToInvarientNumber(input));

        private static double ToDouble(this string input, IFormatProvider provider)
        {
            return double.TryParse(input, NumberStyles.Number | NumberStyles.Float, provider, out var result) ? result : 0;
        }

        public static double? ToNDouble(this string input) => ToNDouble(input, CultureInfo.InvariantCulture);

        public static double? ToNDoubleFromBrazilFormat(this string input) => ToNDouble(input, CultureHelper.BrazilCulture);

        public static double? ToNDoubleFromUndefinedFormat(this string input) => ToNDouble(FormatToInvarientNumber(input));

        private static double? ToNDouble(this string input, IFormatProvider provider)
        {
            return double.TryParse(input, NumberStyles.Number | NumberStyles.Float, provider, out var result) ? result : (double?)null;
        }

        #endregion ToDouble

        #region ToInt

        public static int ToInt(this string input) => ToInt(input, CultureInfo.InvariantCulture);

        private static int ToInt(this string input, IFormatProvider provider)
        {
            return int.TryParse(input, NumberStyles.Integer, provider, out var result) ? result : 0;
        }

        public static int? ToNInt(this string input) => ToNInt(input, CultureInfo.InvariantCulture);

        private static int? ToNInt(this string input, IFormatProvider provider)
        {
            return int.TryParse(input, NumberStyles.Integer, provider, out var result) ? result : (int?)null;
        }

        #endregion ToInt

        #region ToBoolean

        public static bool ToBoolean(this string input)
        {
            return bool.TryParse(input, out var result) && result;
        }

        public static bool? ToNBoolean(this string input)
        {
            return bool.TryParse(input, out var result) ? result : (bool?)null;
        }

        #endregion ToBoolean

        private static string FormatToInvarientNumber(string input)
        {
            var output = input.Trim().Replace(" ", "").Replace(",", ".");

            var split = output.Split('.');

            if (split.Count() <= 1) return output;

            output = string.Join("", split.Take(split.Count() - 1).ToArray());

            return $"{output}.{split.Last()}";
        }
    }
}