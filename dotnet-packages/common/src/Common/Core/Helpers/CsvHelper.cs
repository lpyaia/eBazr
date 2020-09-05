﻿using Common.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common.Core.Helpers
{
    public static class CsvHelper
    {
        public static string Serialize<T>(IEnumerable<T> list)
        {
            if (list == null) return string.Empty;

            const string delimiter = ";";

            var sb = new StringBuilder();
            var properties = typeof(T).GetProperties();

            MakeHeader(properties, sb, delimiter);

            foreach (var item in list)
                MakeRows(sb, item, properties, delimiter);

            return sb.ToString();
        }

        private static void MakeRows(StringBuilder sb, object item, PropertyInfo[] properties, string delimiter)
        {
            foreach (var property in properties)
            {
                var value = property.GetValueString(item);

                if (value != null)
                {
                    if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
                        value = value.FormatBooleanToBrazilFormat();
                    else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                        value = value.FormatDateToDefaultFormatString();

                    sb.Append(value);
                }

                if (Array.IndexOf(properties, property) < (properties.Length - 1))
                    sb.Append(delimiter);
            }

            sb.AppendLine();
        }

        private static void MakeHeader(PropertyInfo[] properties, StringBuilder sb, string delimiter)
        {
            foreach (var property in properties)
            {
                var columnName = property.GetCustomAttributes<DisplayNameAttribute>().FirstOrDefault()?.DisplayName ?? property.Name;

                sb.Append(columnName);

                if (Array.IndexOf(properties, property) < (properties.Length - 1))
                    sb.Append(delimiter);
            }

            sb.AppendLine();
        }
    }
}