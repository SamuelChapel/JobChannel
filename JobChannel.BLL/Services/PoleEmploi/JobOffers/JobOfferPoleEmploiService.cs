using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using JobChannel.DAL.UOW.Repositories.CityRepositories;
using JobChannel.DAL.UOW.Repositories.ContractRepositories;
using JobChannel.DAL.UOW.Repositories.JobOfferRepositories;
using JobChannel.DAL.UOW.Repositories.JobRepositories;
using JobChannel.Domain.BO;
using System.Linq;
using JobChannel.DAL.ObjectExtensions;
using JobChannel.BLL.Services.PoleEmploi.Auth;
using Microsoft.TeamFoundation.Common;

namespace JobChannel.BLL.Services.PoleEmploi.JobOffers
{
    internal class JobOfferPoleEmploiService : IJobOfferPoleEmploiService
    {
        private readonly AuthServicePoleEmploi authServicePole;
        private readonly HttpClient client;

        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IContractRepository _contractRepository;

        public JobOfferPoleEmploiService(IJobOfferRepository jobOfferRepository, IJobRepository jobRepository, ICityRepository cityRepository, IContractRepository contractRepository)
        {
            authServicePole = new AuthServicePoleEmploi();
            client = new HttpClient();
            _jobOfferRepository = jobOfferRepository;
            _jobRepository = jobRepository;
            _cityRepository = cityRepository;
            _contractRepository = contractRepository;
        }

        public async Task<int> GetAndInsertPoleEmploiJobOffers(GetPoleEmploiJobOffersQuery query)
        {
            var jo = await GetJobOffers(query);

            int i = 0;

            foreach (JobOfferPoleEmploi jobOffer in jo)
            {
                i += await InsertJobOfferPoleEmploi(jobOffer);
            }

            return i;
        }

        private async Task<IEnumerable<JobOfferPoleEmploi>> GetJobOffers(GetPoleEmploiJobOffersQuery query)
        {
            if (authServicePole.IsExpired)
                await authServicePole.GenerateAccessToken(client);
            var request = $"https://api.pole-emploi.io/partenaire/offresdemploi/v2/offres/search?";

            bool first = true;

            if (query.Range.HasValue)
            {
                request += $"range={query.Range.Value.Item1}-{query.Range.Value.Item2}";
                first = false;
            }

            if (query.CodeRome?.Length > 0)
            {
                request += $"{(first ? "" : "&")}codeROME={query.CodeRome}";
                first = false;
            }

            if (query.Commune?.Length > 0)
            {
                request += $"{(first ? "" : "&")}commune={query.Commune}";
                first = false;
            }

            if (query.PublieeDepuis.HasValue)
            {
                request += $"{(first ? "" : "&")}publieeDepuis={query.PublieeDepuis}";
                first = false;
            }

            if (query.EntreprisesAdaptees.HasValue)
            {
                request += $"{(first ? "" : "&")}entreprisesAdaptees={query.EntreprisesAdaptees?.ToString().ToLower()}";
            }

            var result = await client.GetStringAsync(request);

            var offres = JsonSerializer.Deserialize<OffresEmplois>(result, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return offres!.Resultats;
        }

        private async Task<int> InsertJobOfferPoleEmploi(JobOfferPoleEmploi jobOfferPoleEmploi)
        {
            var filters = new Dictionary<string, dynamic>()
            {
                {
                    "Url", jobOfferPoleEmploi.OrigineOffre.UrlOrigine.NormalizeAndRemoveDiacriticsAndToLower()
                }
            };

            var jobOffers = await _jobOfferRepository.GetAll(filters);
            if (jobOffers.Any() || jobOfferPoleEmploi.RomeCode.IsNullOrEmpty() || jobOfferPoleEmploi.TypeContrat.IsNullOrEmpty() || jobOfferPoleEmploi.LieuTravail.CodePostal.IsNullOrEmpty())
                return 0;

            // Get Job
            Job job = await _jobRepository.GetByRomeCode(jobOfferPoleEmploi.RomeCode);

            // Get Contract
            Contract contract = await _contractRepository.GetByCode(jobOfferPoleEmploi.TypeContrat);

            // Get City
            City city = await _cityRepository.GetByPostCode(jobOfferPoleEmploi.LieuTravail.CodePostal);

            var jobOffer = new JobOffer
            {
                City = city,
                Contract = contract,
                Job = job,
                Experience = jobOfferPoleEmploi.ExperienceLibelle,
                Company = jobOfferPoleEmploi.Entreprise.Nom,
                Description = jobOfferPoleEmploi.Description,
                Salary = jobOfferPoleEmploi.Salaire.Libelle,
                PublicationDate = jobOfferPoleEmploi.DateCreation,
                ModificationDate = jobOfferPoleEmploi.DateActualisation,
                Title = jobOfferPoleEmploi.Intitule,
                Url = jobOfferPoleEmploi.OrigineOffre.UrlOrigine
            };

            //if (jobOffers.Any())
            //{
            //    jobOffer.Id = jobOffers.First().Id;
            //    return await _jobOfferRepository.Update(jobOffer);
            //}

            return await _jobOfferRepository.Create(jobOffer);
        }
    }

    public record OffresEmplois(
        IEnumerable<JobOfferPoleEmploi> Resultats
        );

    public record JobOfferPoleEmploi(
        string Id,
        string Intitule,
        string Description,
        DateTime DateCreation,
        DateTime DateActualisation,
        OrigineOffre OrigineOffre,
        Salaire Salaire,
        string ExperienceLibelle,
        Entreprise Entreprise,
        string RomeCode,
        string TypeContrat,
        LieuTravail LieuTravail
    );

    public record Salaire(
        string Libelle
        );

    public record OrigineOffre(
        string UrlOrigine
        );

    public record Entreprise(
        string Nom,
        string Description
        );

    public record LieuTravail(
        string Libelle,
        string CodePostal
        );
}