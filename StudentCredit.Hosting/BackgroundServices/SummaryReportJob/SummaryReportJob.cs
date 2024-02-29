using Microsoft.Extensions.Options;
using Quartz;
using StudentCredit.Application.Reports.Interfaces;
using StudentCredit.Data.Logs.Enums;
using StudentCredit.Infrastructure.ConfigurationExtensions.Models;
using StudentCredit.Infrastructure.Logs;

namespace StudentCredit.Hosting.BackgroundServices.SummaryReportJob
{
    public class SummaryReportJob : IJob
    {
        private readonly ISummaryReportService summaryReportService;
        private readonly ILoggingService loggingService;
        private readonly SummaryReportJobConfiguration config;

        public SummaryReportJob(
            ISummaryReportService summaryReportService,
            IOptions<SummaryReportJobConfiguration> options,
            ILoggingService loggingService
            )
        {
            this.summaryReportService = summaryReportService;
            this.loggingService = loggingService;
            config = options.Value;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await loggingService.LogMessageAsync($"Start job!");

            var currentMonth = DateTime.Now.Month;

            try
            {
                if (currentMonth == config.MonthToCreateSheet)
                {
                    await summaryReportService.CreateSheets();

                    await summaryReportService.CreateSheetYears();

                    await summaryReportService.CreateSheetYearsMonth();

                    await loggingService.LogMessageAsync($"Successfully created sheet!");

                }
                else
                {
                    await summaryReportService.CreateSheetYearsMonth();

                    await loggingService.LogMessageAsync($"Successfully created sheet years month!");

                }

                await loggingService.LogMessageAsync($"Job executed successfully!");
            }
            catch (Exception ex)
            {
                await loggingService.LogExceptionAsync(ex, LogType.SummaryReportJobExceptionLog);
            }

        }
    }
}
