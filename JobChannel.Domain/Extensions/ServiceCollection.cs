using Microsoft.Extensions.DependencyInjection;

namespace JobChannel.Domain.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {

            return services;
        }
    }
}
