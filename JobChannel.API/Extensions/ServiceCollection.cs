using JobChannel.BLL.Services.CityServices;
using JobChannel.BLL.Services.ContractServices;
using JobChannel.BLL.Services.DepartmentServices;
using JobChannel.BLL.Services.JobOfferServices;
using JobChannel.BLL.Services.JobServices;
using JobChannel.BLL.Services.RegionServices;
using JobChannel.DAL.UOW;
using JobChannel.DAL.UOW.Repositories.CityRepositories;
using JobChannel.DAL.UOW.Repositories.ContractRepositories;
using JobChannel.DAL.UOW.Repositories.DepartmentRepositories;
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

            return services;
        }
    }
}
