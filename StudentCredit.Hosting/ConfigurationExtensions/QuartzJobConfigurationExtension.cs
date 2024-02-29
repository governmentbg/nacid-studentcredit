using Quartz;
using StudentCredit.Hosting.BackgroundServices.SummaryReportJob;
using StudentCredit.Infrastructure.ConfigurationExtensions.Models;

namespace StudentCredit.Hosting.ConfigurationExtensions
{
    public static class QuartzJobConfigurationExtension
	{
		public static IServiceCollection ConfigureQuartz(this IServiceCollection services, SummaryReportJobConfiguration config)
		{
			services.AddQuartz(q =>
			{
				var jobKey = new JobKey("SummaryReportJob");
				q.AddJob<SummaryReportJob>(opt => opt.WithIdentity(jobKey));

				q.AddTrigger(opts => opts
					.ForJob(jobKey)
					.WithIdentity("summaryreporttrigger")
					.WithCronSchedule("1 1 1 1 1/1 ? *", x => x.InTimeZone(TimeZoneInfo.Local)));
			});

			services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

			return services;
		}
	}
}
