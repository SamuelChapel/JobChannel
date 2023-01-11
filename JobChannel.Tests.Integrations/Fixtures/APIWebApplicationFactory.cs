using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobChannel.Tests.Integrations.Fixtures
{
    // Cette classe créée une instance d'une web application    
    //TODO   Modifier le nom du startup de l'API à exécuter    
    public class ApiWebApplicationFactory : WebApplicationFactory<API.Startup>
    {
        private IConfiguration Configuration { get; set; }

        //La méthode est substituée pour charger un fichier de configuration pour les tests d'intégration
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                Configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Integrations.json")
                    .Build();
                config.AddConfiguration(Configuration);
            });

            builder.ConfigureTestServices(services =>
            {
                services.AddAuthentication(defaultScheme: "TestSchemeAdmin")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestSchemeAdmin", options => { });
            });

            TestData.Initialize(Configuration);
        }

    }
}
