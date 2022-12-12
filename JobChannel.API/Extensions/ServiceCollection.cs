﻿using JobChannel.BLL.Services.ContractServices;
using JobChannel.BLL.Services.JobOfferServices;
using JobChannel.BLL.Services.JobServices;
using JobChannel.BLL.Services.RegionServices;
using JobChannel.DAL.UOW;
using JobChannel.DAL.UOW.Repositories.ContractRepositories;
using JobChannel.DAL.UOW.Repositories.JobOfferRepositories;
using JobChannel.DAL.UOW.Repositories.JobRepositories;
using JobChannel.DAL.UOW.Repositories.RegionRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace JobChannel.API.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IJobOfferService, JobOfferService>();
            services.AddScoped<IJobOfferRepository, JobOfferRepository>();
            services.AddScoped<IDbSession, DbSession>();

            return services;
        }
    }
}
