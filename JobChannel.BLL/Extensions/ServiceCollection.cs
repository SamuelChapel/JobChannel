using JobChannel.BLL.Services.CityServices;
using JobChannel.BLL.Services.ContractServices;
using JobChannel.BLL.Services.DepartmentServices;
using JobChannel.BLL.Services.JobOfferServices;
using JobChannel.BLL.Services.JobServices;
using JobChannel.BLL.Services.PoleEmploi.JobOffers;
using JobChannel.BLL.Services.RegionServices;
using Microsoft.Extensions.DependencyInjection;

namespace JobChannel.BLL.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddBLLServices(this IServiceCollection services)
        {
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IJobOfferService, JobOfferService>();
            services.AddScoped<IJobOfferPoleEmploiService, JobOfferPoleEmploiService>();

            return services;
        }
    }
}
