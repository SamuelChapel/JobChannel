using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using JobChannel.Domain.BO;
using JobChannel.Tests.Integrations.Fixtures;
using Xunit;

namespace JobChannel.Tests.Integrations.JobOfferController
{
    public class JobOfferIntegrationTests : AbstractIntegrationTest
    {
        public JobOfferIntegrationTests(ApiWebApplicationFactory factory) : base(factory)
        {
        }

        [Theory]
        [InlineData(2)]
        public async void GetJobOfferByIdShouldReturnOk(int id)
        {
            // Arrange
            string uri = $"api/JobOffer/{id}";
            var expectedJoOffer = new JobOffer()
            {
                Id = id,
                Title = "Développeur C#/.NET   (H/F)",
                Description = "Pour accompagner un projet pour un client du secteur de l'énergie, nous recherchons actuellement notre futur Développeur C#/.NET (H/F).\n\nDans ce cadre, et au sein d'une équipe constituée d'un chef de projet et de deux développeurs, plusieurs missions pourront vous être confiées :\n- Proposition de nouvelles solutions techniques,\n- Conception et développement de nouvelles fonctionnalités,\n- Réalisation de tests unitaires,\n- Rédaction et mise à jour de documentation technique.\n\nCes missions ne sont pas exhaustives et pourront être amenées à évoluer en fonction de l'environnement et des spécificités clients.\n\nVous êtes un habitué de l'environnement.NET ? Vous avez envie de confirmer vos acquis en C# et de développer votre expertise ? Vous avez idéalement des connaissances en WPF. Vous appréciez le travail en équipe ? Coconstruisez votre avenir en quatuor avec un manager, un Chargé des Ressources Humaines et un commercial !\n\nAu-delà de la mission, vous recherchez une structure qui saura reconnaître vos compétences et vous accompagner dans vos souhaits d'évolution.\n\nVous êtes disponible dès maintenant ou sous préavis : Aucun soucis, nous saurons être patients !\n\nCes quelques lignes vous tentent, on en discute ?\n\nSi vous souhaitez en savoir plus, vous aurez l'occasion d'échanger avec notre équipe RH !\n\nEt pour la suite du recrutement ?\n\nA la suite d'un court échange téléphonique avec notre équipe RH, vous les rencontrerez de visu ou en visio lors d'un entretien RH. Un échange technique vous sera ensuite proposé avec un de nos développeurs ou le chef du projet. Enfin, un dernier échange aura lieu avec un de nos managers.",
                PublicationDate = new DateTime(2022, 12, 27, 16, 05, 28),
                ModificationDate = new DateTime(2022, 12, 28, 8, 48, 37),
                Experience = "3 ans",
                Salary = "Annuel de 30000 Euros à 50000 Euros sur 12 mois",
                Url = "https://candidat.pole-emploi.fr/offres/recherche/detail/146HTSX",
                Contract = new Contract()
                {
                    Id = 2,
                    Code = "CDI",
                    Name = "Contrat à durée indéterminée"
                },
                Job = new Job()
                {
                    Name = "Études et développement informatique",
                    CodeRome = "M1805",
                    Id = 1
                },
                Company = "CREATIVE INGENIERIE",
                City = new City()
                {
                    Id = 102,
                    Name = "Saint-Herblain",
                    Code = "44162",
                    Population = 47415,
                    Postcodes = new List<string>()
                    {
                        "44800"
                    },
                    Department = new Department()
                    {
                        Code = "44",
                        Name = "Loire-Atlantique",
                        Id = 12,
                        Region = new Region()
                        {
                            Id = 52,
                            Code = "52",
                            Name = "Pays de la Loire"
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(uri);

            // Assert
            Assert.True(response.IsSuccessStatusCode);

            var actualJobOffer = await response.Content.ReadFromJsonAsync<JobOffer>();
            Assert.Equal(expectedJoOffer, actualJobOffer);
        }

        [Theory]
        [InlineData(1)]
        public async void GetJobOfferByIdShouldReturnNotFound(int id)
        {
            // Arrange
            string uri = $"api/JobOffer/{id}";

            // Act
            var response = await _client.GetAsync(uri);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void UpdateJobOfferShouldReturnNotFound()
        {
            // Arrange
            string uri = $"api/JobOffer/1";

            var jobOffer = new JobOffer();

            // Act
            var response = await _client.PutAsJsonAsync(uri, jobOffer);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
