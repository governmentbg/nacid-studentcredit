using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StudentCredit.Application.InstitutionSpecialities.Dtos;
using StudentCredit.Application.Nomenclatures.Interfaces;
using StudentCredit.Hosting.BackgroundServices.RabbitMq.Configurations;
using StudentCredit.Infrastructure.BackgroundServices.RabbitMqBaseConsumer;
using StudentCredit.Infrastructure.Logs;

namespace StudentCredit.Hosting.BackgroundServices.RabbitMq.Consumers.RndConsumers
{
	public class SpecialityConsumer : BaseConsumerService
	{
		private readonly IServiceProvider serviceProvider;

		public SpecialityConsumer(
			RndConsumerConnectionService consumer,
			IOptions<RndConsumerConfiguration> configuration,
			IServiceProvider serviceProvider
			)
			: base(consumer, configuration.Value.RndSpecialityUpdateExchange)
		{
			this.serviceProvider = serviceProvider;
		}

		protected override Action<string> OnReceive => SaveSpeciality;

		private async void SaveSpeciality(string body)
		{
			using (var scope = serviceProvider.CreateScope())
			{
				var loggingService = scope.ServiceProvider.GetRequiredService<ILoggingService>();

				SpecialityDto model = null;

				try
				{
					model = JsonConvert.DeserializeObject<SpecialityDto>(body);
				}
				catch (Exception ex)
				{
					var message = $"Speciality message: >> {body} << failed with Exception: \n {ex.InnerException} \n {ex.Message}";

					await loggingService.LogConsumerException(message);
				}

				if (model != null)
				{
					try
					{
						var nomenclatureService = scope.ServiceProvider.GetRequiredService<INomenclatureService<StudentCredit.Data.Nomenclatures.Speciality>>();

						var nomenclature = new StudentCredit.Data.Nomenclatures.Speciality(model.Id, model.Name, model.Id, model.EducationalQualificationId, model.IsActive);

						var existingNomenclature = await nomenclatureService.GetSingleOrDefaultNomenclatureAsync(e => e.ExternalId == model.Id);

						if (existingNomenclature != null)
						{
							await nomenclatureService.UpdateNomenclatureAsync(existingNomenclature.Id, nomenclature);
						}
						else
						{
							await nomenclatureService.InsertNomenclatureAsync(nomenclature);
						}
					}
					catch (Exception ex)
					{
						var message = $"Speciality with name {model.Name} and Id {model.Id}, failed with Exception: \n {ex.Message}";

						await loggingService.LogConsumerException(message);
					}
				}
			}
		}
	}
}
