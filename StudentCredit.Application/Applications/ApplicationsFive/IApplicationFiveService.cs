using StudentCredit.Application.Applications.ApplicationsFive.Dtos;
using StudentCredit.Application.Applications.Common.Interfaces;
using StudentCredit.Application.Common.Dtos;

namespace StudentCredit.Application.Applications.ApplicationsFive
{
	public interface IApplicationFiveService : IApplicationService<ApplicationFiveDto>
	{
		Task<SearchResultItemDto<ApplicationFiveSearchResultItemDto>> GetFiltered(SearchApplicationFiveFilter filter);

		Task<DividendReportResultDto> GenerateReport(DividendReportSearchDto dto);
	}
}
