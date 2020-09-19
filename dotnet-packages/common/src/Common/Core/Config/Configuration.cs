using Common.Core.Common;
using Common.Core.Extensions;
using Common.Core.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Common.Core.Config
{
    public static class Configuration
    {
        public static readonly ValueGetter<EnvironmentType> Stage = new ValueGetter<EnvironmentType>(
            () => EnumHelper.ParseTo<EnvironmentType>(Environment.GetEnvironmentVariable(EnvironmentVariables.Environment)));

        public static readonly ValueGetter<string> AppName = new ValueGetter<string>(
            () => Environment.GetEnvironmentVariable(EnvironmentVariables.Application)?.ToPascalCase() ?? "APP");

        public static readonly ValueGetter<bool> Debugging = new ValueGetter<bool>(
            () => Environment.GetEnvironmentVariable(EnvironmentVariables.Debug).ToBoolean());

        public static readonly ValueGetter<bool> DebuggingSql = new ValueGetter<bool>(
           () => Environment.GetEnvironmentVariable(EnvironmentVariables.DebugSql).ToBoolean());

        public static readonly ValueGetter<string> KeyValueStore = new ValueGetter<string>(
            () => Environment.GetEnvironmentVariable(EnvironmentVariables.KvStore));

        public static readonly ValueGetter<string> AppRoutePrefix = new ValueGetter<string>(
         () => Environment.GetEnvironmentVariable(EnvironmentVariables.RoutePrefix));

        public static readonly ValueGetter<string> BaseUri = new ValueGetter<string>(
         () => Environment.GetEnvironmentVariable(EnvironmentVariables.EnvBaseUri));

        public static IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder().PrepareBuilder().Build();
        }

        public static IConfigurationBuilder PrepareBuilder(this IConfigurationBuilder config)
        {
            config.SetBasePath(Directory.GetCurrentDirectory());

            config.AddJsonFile("appsettings.json", true, true)
                  .AddJsonFile($"appsettings.{Stage.Get().GetName().ToLower()}.json", true, true);

            config.AddEnvironmentVariables();

            return config;
        }

        public static class SectionNames
        {
            public const string Feature = "FeatureSettings";
            public const string Api = "ApiSettings";
            public const string Integration = "IntegrationSettings";
        }

        public static class EnvironmentVariables
        {
            public const string Environment = "ASPNETCORE_ENVIRONMENT";
            public const string Debug = "HB_DEBUGGING";
            public const string DebugSql = "HB_DEBUGGING_SQL";
            public const string KvStore = "HB_KVSTORE";
            public const string Application = "HB_APPLICATION";
            public const string RoutePrefix = "HB_ROUTE_PREFIX";
            public const string EnvBaseUri = "HB_BASE_URI";
        }
    }
}