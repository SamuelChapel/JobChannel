﻿using System;
using System.Threading;
using System.Threading.Tasks;
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
        private const string CronExpression = "0 */1 * * *";

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
                if (DateTime.Now >= _nextRun)
                {
                    await CleanupJobOffers();
                    await LookForNewJobOffers();
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

            var _jobOfferPoleEmploiService = scope.ServiceProvider.GetRequiredService<IJobOfferPoleEmploiService>();

            await _jobOfferPoleEmploiService.CleanUpJobOffers();
        }

        private async Task LookForNewJobOffers()
        {
            using IServiceScope scope = _serviceProvider.CreateScope();

            var _jobOfferPoleEmploiService = scope.ServiceProvider.GetRequiredService<IJobOfferPoleEmploiService>();

            await _jobOfferPoleEmploiService.GetPoleEmploiJobOffersForAllJobs();
        }
    }
}
