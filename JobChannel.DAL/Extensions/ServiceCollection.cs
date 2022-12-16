using JobChannel.DAL.UOW;
using JobChannel.DAL.UOW.Repositories.CityRepositories;
using JobChannel.DAL.UOW.Repositories.ContractRepositories;
using JobChannel.DAL.UOW.Repositories.DepartmentRepositories;
using JobChannel.DAL.UOW.Repositories.JobOfferRepositories;
using JobChannel.DAL.UOW.Repositories.JobRepositories;
using JobChannel.DAL.UOW.Repositories.RegionRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace JobChannel.DAL.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddDALServices(this IServiceCollection services)
        {
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IJobOfferRepository, JobOfferRepository>();
            services.AddScoped<IDbSession, DbSession>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
