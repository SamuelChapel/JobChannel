using FluentValidation;
using JobChannel.API.Controllers.JobOffers.Requests;
using JobChannel.API.Controllers.Security.Requests;
using JobChannel.BLL.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace JobChannel.API.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            ValidatorOptions.Global.LanguageManager = new FrenchLanguageManager();

            services.AddScoped<IValidator<JobOfferUpdateRequest>, JobOfferUpdateRequestValidator>();
            services.AddScoped< IValidator < JobOfferCreateRequest >, JobOfferCreateRequestValidator>();
            services.AddScoped< IValidator < JobOfferFindRequest >, JobOfferFindRequestValidator>();
            services.AddScoped<IValidator<GetPoleEmploiJobOffersRequest>, GetPoleEmploiJobOffersRequestValidator>();
            services.AddScoped<IValidator<AuthenticateRequest>, AuthenticateRequestValidator>();

            return services;
        }
    }
}
