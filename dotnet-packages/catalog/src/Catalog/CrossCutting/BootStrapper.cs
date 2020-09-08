using AutoMapper;
using Common.Core.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Catalog.CrossCutting
{
    public class BootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration config)
        {
            // Infra
            services.AddMediatR(typeof(BootStrapper));
            services.AddAutoMapper(typeof(BootStrapper));
            services.AddScoped<IDateTimeService, DateTimeService>();
        }
    }
}
