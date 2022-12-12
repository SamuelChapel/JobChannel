using JobChannel.BLL.ContractService;
using JobChannel.BLL.JobOfferService;
using JobChannel.DAL.UOW;
using JobChannel.DAL.UOW.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JobChannel.API.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IJobOfferService, JobOfferService>();
            services.AddScoped<IJobOfferRepository, JobOfferRepository>();
            services.AddScoped<IDbSession, DbSession>();

            return services;
        }
    }
}
