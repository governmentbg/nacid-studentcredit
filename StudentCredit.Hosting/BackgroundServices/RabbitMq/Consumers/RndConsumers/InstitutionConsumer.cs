using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StudentCredit.Application.InstitutionSpecialities.Dtos;
using StudentCredit.Application.InstitutionSpecialities.Services;
using StudentCredit.Hosting.BackgroundServices.RabbitMq.Configurations;
using StudentCredit.Infrastructure.BackgroundServices.RabbitMqBaseConsumer;
using StudentCredit.Infrastructure.Logs;

namespace StudentCredit.Hosting.BackgroundServices.RabbitMq.Consumers.RndConsumers
{
	public class InstitutionConsumer : BaseConsumerService
	{
		private readonly IServiceProvider serviceProvider;

		public InstitutionConsumer(
			RndConsumerConnectionService consumer,
			IOptions<RndConsumerConfiguration> configuration,
			IServiceProvider serviceProvider
			)
			: base(consumer, configuration.Value.RndOrganizationUpdateExchange)
		{
			this.serviceProvider = serviceProvider;
		}

		protected override Action<string> OnReceive => SaveInstitution;

		private async void SaveInstitution(string body)
		{
			using (var scope = serviceProvider.CreateScope())
			{
				var loggingService = scope.ServiceProvider.GetRequiredService<ILoggingService>();

				InstitutionDto model = null;

				try
				{
					model = JsonConvert.DeserializeObject<InstitutionDto>(body);
				}
				catch (Exception ex)
				{
					var message = $"Institution message: >> {body} << failed with Exception: \n {ex.InnerException} \n {ex.Message}";

					await loggingService.LogConsumerException(message);
				}

				if (model != null)
				{
					try
					{
						var institutionSpecialitiesService = scope.ServiceProvider.GetRequiredService<InstitutionSpecialityService>();

						await institutionSpecialitiesService.SaveInstitution(model);
					}
					catch (Exception ex)
					{
						var message = $"Institution with name {model.Name} and Id {model.Id}, failed with Exception: \n {ex.InnerException} \n {ex.Message}";

						await loggingService.LogConsumerException(message);
					}
				}
			}
		}
	}
}
