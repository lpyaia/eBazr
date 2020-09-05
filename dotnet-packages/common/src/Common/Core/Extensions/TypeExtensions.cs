using Common.Core.Helpers;
using System;
using System.Globalization;
using System.Reflection;

namespace Common.Core.Extensions
{
    public static class TypeExtensions
    {
        public static T Clone<T>(this T item)
        {
            if (item == null) return default;

            return (T)item.GetType().InvokeMember("MemberwiseClone", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic, null, item, null);
        }

        public static string GetValueString(this PropertyInfo prop, object obj)
        {
            if (obj == null || prop == null) return null;

            var value = prop.GetValue(obj);

            return value == null ? null : FormatString(value, prop.PropertyType);
        }

        public static string FormatString(this object value)
        {
            return value == null ? null : FormatString(value, value.GetType());
        }

        private static string FormatString(this object value, Type type)
        {
            if (type == typeof(DateTime) || type == typeof(DateTime?))
                return FormatDateToDefaultFormatString(value);

            return value.ToString();
        }

        public static string FormatDateToDefaultFormatString(this object value)
        {
            return Convert.ToDateTime(value, CultureInfo.InvariantCulture).ToString(DateHelper.DefaultFormat);
        }

        public static string FormatBooleanToBrazilFormat(this object value)
        {
            return string.Equals(value.ToString(), "true", StringComparison.InvariantCultureIgnoreCase) ? "Sim" : "Não";
        }
    }
}