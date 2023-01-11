using System.Net.Http;
using Xunit;

namespace JobChannel.Tests.Integrations.Fixtures
{
    //Classe qui servira de base pour un test d'intégration
    //Lance un serveur de test pour l'API et un client Http pour envoyer les requêtes 
    public abstract class AbstractIntegrationTest : IClassFixture<ApiWebApplicationFactory>
    {
        protected readonly ApiWebApplicationFactory _factory;
        protected readonly HttpClient _client;

        public AbstractIntegrationTest(ApiWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }
    }
}
