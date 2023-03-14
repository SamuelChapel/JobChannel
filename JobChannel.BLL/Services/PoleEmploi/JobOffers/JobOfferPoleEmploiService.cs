using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using JobChannel.DAL.UOW.Repositories.CityRepositories;
using JobChannel.DAL.UOW.Repositories.ContractRepositories;
using JobChannel.DAL.UOW.Repositories.JobOfferRepositories;
using JobChannel.DAL.UOW.Repositories.JobRepositories;
using JobChannel.Domain.BO;
using JobChannel.DAL.ObjectExtensions;
using JobChannel.BLL.Services.PoleEmploi.Auth;
using Microsoft.TeamFoundation.Common;
using Microsoft.Extensions.Logging;

namespace JobChannel.BLL.Services.PoleEmploi.JobOffers
{
    internal class JobOfferPoleEmploiService : IJobOfferPoleEmploiService
    {
        private readonly ILogger _logger;

        private readonly IAuthServicePoleEmploi authServicePole;
        private readonly HttpClient client;

        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IContractRepository _contractRepository;

        public JobOfferPoleEmploiService(
            IJobOfferRepository jobOfferRepository,
            IJobRepository jobRepository,
            ICityRepository cityRepository,
            IContractRepository contractRepository,
            IAuthServicePoleEmploi authServicePoleEmploi,
            ILogger<JobOfferPoleEmploiService> logger)
        {
            authServicePole = authServicePoleEmploi;
            client = new HttpClient();
            _jobOfferRepository = jobOfferRepository;
            _jobRepository = jobRepository;
            _cityRepository = cityRepository;
            _contractRepository = contractRepository;
            _logger = logger;
        }

        public async Task CleanUpJobOffers()
        {
            var filters = new Dictionary<string, dynamic>()
            {
                {"StartDate", new DateTime(1900,1,1) },
                {"EndDate",  DateTime.Today.AddMonths(-6) }
            };

            var jobOffers = await _jobOfferRepository.GetAll(filters);

            foreach (JobOffer jobOffer in jobOffers)
            {
                await _jobOfferRepository.Delete(jobOffer);
            }
        }

        public async Task<int> GetAndInsertPoleEmploiJobOffers(GetPoleEmploiJobOffersQuery query)
        {
            List<JobOfferPoleEmploi> jo = new List<JobOfferPoleEmploi>();
            int i = 0;

            if (!query.Range.HasValue || query.Range.Value.Item2 - query.Range.Value.Item1 < 150)
            {
                var jobOffers = await GetJobOffers(query);
                if (jobOffers != null)
                    jo.AddRange(jobOffers);
            }
            else
            {
                int min = query.Range.Value.Item1;
                int max = query.Range.Value.Item2;

                for (int j = min; j <= max; j += 150)
                {
                    var query2 = new GetPoleEmploiJobOffersQuery((j, j + 149 < max ? j + 149 : max), query?.CodeRome, query?.Commune, query?.PublieeDepuis, query?.EntreprisesAdaptees);
                    var jobOffers = await GetJobOffers(query2);
                    if (jobOffers != null)
                        jo.AddRange(jobOffers);
                }
            }

            foreach (JobOfferPoleEmploi jobOffer in jo)
            {
                if (await JobOfferAlreadyExist(jobOffer))
                    break;

                if (await InsertJobOfferPoleEmploi(jobOffer))
                    i++;
            }

            return i;
        }

        public async Task GetPoleEmploiJobOffersForAllJobs()
        {
            var jobs = await _jobRepository.GetAll();

            foreach (Job job in jobs)
            {
                _logger.LogInformation("Vérification pour le job {job}", job.Name);

                await GetAndInsertPoleEmploiJobOffers(new GetPoleEmploiJobOffersQuery((0, 149), job.CodeRome, null, 1, null));
            }
        }

        private async Task<IEnumerable<JobOfferPoleEmploi>?> GetJobOffers(GetPoleEmploiJobOffersQuery query)
        {
            try
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

                if (!string.IsNullOrEmpty(result))
                {
                    var offres = JsonSerializer.Deserialize<OffresEmplois>(result, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    _logger.LogInformation("{number} offres récupérées pour le {job}", offres.Resultats.Count(), query.CodeRome);

                    return offres!.Resultats;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        private async Task<bool> InsertJobOfferPoleEmploi(JobOfferPoleEmploi jobOfferPoleEmploi)
        {
            _logger.LogInformation("Traitement de l'offre : {id} {titre} {date}", jobOfferPoleEmploi.Id, jobOfferPoleEmploi.Intitule, jobOfferPoleEmploi.DateActualisation);

            if (jobOfferPoleEmploi.RomeCode.IsNullOrEmpty() || jobOfferPoleEmploi.TypeContrat.IsNullOrEmpty() || jobOfferPoleEmploi.LieuTravail.CodePostal.IsNullOrEmpty())
                return false;

            // Get Job
            Job job = await _jobRepository.GetByRomeCode(jobOfferPoleEmploi.RomeCode);

            // Get Contract
            Contract contract = await _contractRepository.GetByCode(jobOfferPoleEmploi.TypeContrat);

            // Get City
            City? city = await _cityRepository.GetByPostCode(jobOfferPoleEmploi.LieuTravail.CodePostal);

            if (job != null && contract != null && city != null)
            {
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

                var id = await _jobOfferRepository.Create(jobOffer);
                return id != 0;
            }
            else
            {
                _logger.LogWarning("Offre {id} {titre} {date} non insérée", jobOfferPoleEmploi.Id, jobOfferPoleEmploi.Intitule, jobOfferPoleEmploi.DateActualisation);
                return false;
            }
        }

        private async Task<bool> JobOfferAlreadyExist(JobOfferPoleEmploi jobOfferPoleEmploi)
        {
            var filters = new Dictionary<string, dynamic>()
            {
                {
                    "Url", jobOfferPoleEmploi.OrigineOffre.UrlOrigine.NormalizeAndRemoveDiacriticsAndToLower()
                }
            };

            var jobOffers = await _jobOfferRepository.GetAll(filters);
            return jobOffers.Any();
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