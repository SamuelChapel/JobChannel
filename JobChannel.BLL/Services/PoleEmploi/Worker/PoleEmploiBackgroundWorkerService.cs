using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobChannel.BLL.Services.JobOfferServices;
using JobChannel.BLL.Services.PoleEmploi.JobOffers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NCrontab;

namespace JobChannel.BLL.Services.PoleEmploi.Worker
{
    public class PoleEmploiBackgroundWorkerService : BackgroundService
    {
        private readonly CrontabSchedule _schedule;
        private DateTime _nextRun;
        private const string CronExpression = "0 0 * * *";

        private readonly IServiceProvider _serviceProvider;

        public PoleEmploiBackgroundWorkerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _schedule = CrontabSchedule.Parse(CronExpression);
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                if (DateTime.Now > _nextRun)
                {
                    await CleanupJobOffers();
                    await LookForNewJobOffers("M1805");
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }

                TimeSpan waitTime = _nextRun - DateTime.Now;
                await Task.Delay((int)waitTime.TotalMilliseconds, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private async Task CleanupJobOffers()
        {
            using IServiceScope scope = _serviceProvider.CreateScope();

            var _jobOfferService = scope.ServiceProvider.GetRequiredService<IJobOfferService>();

            var filters = new Dictionary<string, dynamic>()
            {
                {"StartDate", DateTime.MinValue },
                {"EndDate",  DateTime.Today.AddMonths(-6) }
            };

            var jobOffers = await _jobOfferService.GetAll(filters);
        }

        private async Task LookForNewJobOffers(string codeRome)
        {
            var query = new GetPoleEmploiJobOffersQuery(null, codeRome, null, 1, null);

            using IServiceScope scope = _serviceProvider.CreateScope();

            var _jobOfferPoleEmploiService = scope.ServiceProvider.GetRequiredService<IJobOfferPoleEmploiService>();

            var nbInserted = await _jobOfferPoleEmploiService.GetAndInsertPoleEmploiJobOffers(query);
        }
    }
}
