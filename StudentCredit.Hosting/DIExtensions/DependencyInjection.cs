using StudentCredit.Hosting.BackgroundServices.RabbitMq.Consumers.RndConsumers;

namespace StudentCredit.Hosting.DIExtensions
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddConsumers(this IServiceCollection services)
		{
			services
				.AddSingleton<RndConsumerConnectionService>()
				.AddHostedService<InstitutionConsumer>()
				.AddHostedService<SpecialityConsumer>()
			;

			return services;
		}
	}
}
