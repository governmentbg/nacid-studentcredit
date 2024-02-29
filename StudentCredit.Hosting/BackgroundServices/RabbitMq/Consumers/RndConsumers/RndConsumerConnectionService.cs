using Microsoft.Extensions.Options;
using StudentCredit.Hosting.BackgroundServices.RabbitMq.Configurations;
using StudentCredit.Infrastructure.BackgroundServices.RabbitMqBaseConsumer;

namespace StudentCredit.Hosting.BackgroundServices.RabbitMq.Consumers.RndConsumers
{
    public class RndConsumerConnectionService : BaseConsumerConnectionService
    {
		public RndConsumerConnectionService(IOptions<RndConsumerConfiguration> configuration)
			: base(configuration.Value)
		{

		}
	}
}
