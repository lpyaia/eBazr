using Common.Core.Config;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common.Core.Extensions
{
    public static class ConfigurationExtensions
    {
        public static bool IsFeatureEnabled(this IConfiguration value, string name)
        {
            return value[$"{Configuration.SectionNames.Feature}:{name}"].ToBoolean();
        }

        public static string GetApiUrl(this IConfiguration value, string name)
        {
            return value[$"{Configuration.SectionNames.Api}:{name}"];
        }

        public static TConfig GetIntegrationConfig<TConfig>(this IConfiguration value, string name)
            where TConfig : class, new()
        {
            var settings = new TConfig();
            value.GetSection($"{Configuration.SectionNames.Integration}:{name}").Bind(settings);

            return settings;
        }

        public static Dictionary<string, string> ToDictionaryConfig(this object obj, params string[] sectionNames)
        {
            var ret = new Dictionary<string, string>();

            var xSection = sectionNames != null && sectionNames.Any() ? string.Join(":", sectionNames) : null;

            FillValues(obj, xSection, ret);

            return ret;
        }

        private static void FillValues(object obj, string sectionName, Dictionary<string, string> result)
        {
            var xSection = sectionName != null ? sectionName + ":" : sectionName;

            if (obj == null)
            {
                if (sectionName != null)
                    result.Add(sectionName, null);
            }
            else
            {
                if (obj.GetType().IsValueType || obj is string)
                {
                    result.Add(sectionName, obj.FormatString());
                }
                else if (obj is IDictionary dic)
                {
                    foreach (var key in dic.Keys)
                    {
                        var name = $"{xSection}{key}".ToLower();

                        var value = dic[key];

                        FillValues(value, name, result);
                    }
                }
                else if (obj is IEnumerable enumerable)
                {
                    var index = 0;
                    foreach (var value in enumerable)
                    {
                        var name = $"{xSection}{index}".ToLower();

                        if (value == null) continue;

                        FillValues(value, name, result);

                        index++;
                    }
                }
                else
                {
                    var properties = obj.GetType().GetProperties();

                    foreach (var prop in properties)
                    {
                        var name = $"{xSection}{prop.Name}".ToLower();
                        var value = prop.GetValue(obj);

                        FillValues(value, name, result);
                    }
                }
            }
        }
    }
}