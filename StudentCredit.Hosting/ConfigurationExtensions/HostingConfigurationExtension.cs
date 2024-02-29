using StudentCredit.Hosting.BackgroundServices.RabbitMq.Configurations;

namespace StudentCredit.Hosting.ConfigurationExtensions
{
	public static class HostingConfigurationExtension
	{
		public static IConfiguration ConfigureHostingConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services
				.Configure<RndConsumerConfiguration>(configuration.GetSection("RndConsumerConfiguration"))
				.AddOptions();

			return configuration;
		}
	}
}
