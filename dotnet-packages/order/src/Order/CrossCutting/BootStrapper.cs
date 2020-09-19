using AutoMapper;
using Common.Core.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Order.CrossCutting
{
    public class BootStrapper
    {
        protected BootStrapper()
        {
        }

        public static void RegisterServices(IServiceCollection services)
        {
            // Infra
            services.AddMediatR(typeof(BootStrapper));
            services.AddAutoMapper(typeof(BootStrapper));
            services.AddScoped<IDateTimeService, DateTimeService>();
        }
    }
}