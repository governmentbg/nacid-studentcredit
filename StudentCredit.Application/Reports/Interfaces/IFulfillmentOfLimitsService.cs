using StudentCredit.Application.Reports.Dtos.FulfillmentOfLimits;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Reports.Interfaces
{
	public interface IFulfillmentOfLimitsService : ITransientService
	{
		Task<List<FulfillmentOfLimitsReportDto>> Get();
	}
}
