using StudentCredit.Application.Common.Dtos;
using StudentCredit.Application.Credits.Dtos;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Credits
{
	public interface ICreditService : ITransientService
	{
		Task<SearchResultItemDto<CreditSearchResultItemDto>> GetCreditsFiltered(SearchCreditFilter filter);

		Task<CreditInfoDto> GetCreditInfo(int id);

		Task PayCreditByApplicationFour(int bankId, CreditType creditType, string creditNumber, string uin);
	}
}
