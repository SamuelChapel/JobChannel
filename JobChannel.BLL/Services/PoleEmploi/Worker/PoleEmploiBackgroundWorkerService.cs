using System;
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
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private const string Schedule = "02 11 * * *";
        //private const int generalDelay = 60 * 1000; // 1 minute

        private readonly IServiceProvider _serviceProvider;

        public PoleEmploiBackgroundWorkerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _schedule = CrontabSchedule.Parse(Schedule);
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                if (DateTime.Now > _nextRun)
                {
                    await LookForNewJobOffers("M1805");
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }

                Console.WriteLine(_nextRun.ToLongDateString());

                //wait 10 secondes before each check
                await Task.Delay(10000, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    await Task.Delay(generalDelay, stoppingToken);
            //    await LookForNewJobOffers("M1805");
            //}
        }

        private async Task LookForNewJobOffers(string codeRome)
        {
            var query = new GetPoleEmploiJobOffersQuery(null, codeRome, null, 1, null);

            using IServiceScope scope = _serviceProvider.CreateScope();

            IJobOfferPoleEmploiService _jobOfferPoleEmploiService = scope.ServiceProvider.GetRequiredService<IJobOfferPoleEmploiService>();

            await _jobOfferPoleEmploiService.GetAndInsertPoleEmploiJobOffers(query);
        }
    }
}
