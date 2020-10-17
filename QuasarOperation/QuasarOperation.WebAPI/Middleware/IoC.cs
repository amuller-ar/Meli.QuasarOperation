﻿using Microsoft.Extensions.DependencyInjection;
using QuasarOperation.DataAccess;
using QuasarOperation.Domain.Interfaces.Repostories;
using QuasarOperation.Domain.Interfaces.Services;
using QuasarOperation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuasarOperation.WebAPI.Middleware
{
    public static class IoC
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services)
        {
            //agregar servicios aca
            services.AddScoped<IMessageRecovery, MessageRecovery>();
            services.AddTransient<ILocatorService, LocatorService>();
            services.AddTransient<ISatelliteRepository, SatelliteInMemoryRepository>();

            return services;
        }
    }
}
