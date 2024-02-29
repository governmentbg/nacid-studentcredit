using Microsoft.AspNetCore.Http;
using StudentCredit.Application.Applications.ApplicationsTwo.Dtos;
using StudentCredit.Application.Common.Dtos;
using StudentCredit.Infrastructure.Interfaces.Registration;
using System.Linq.Expressions;

namespace StudentCredit.Application.Applications.ApplicationsTwo
{
	public interface IApplicationTwoService : ITransientService
	{
		Task<SearchResultItemDto<ApplicationTwoSearchResultItemDto>> GetApplicationsFiltered(SearchApplicationTwoFilter filter);
		Task<ApplicationTwoDto> GetById(int id);
		Task<int> Create(ApplicationTwoDto dto);
		Task AddRecordEntry(RecordEntryDto dto);
		void Edit(ApplicationTwoDto model);
		Task ImportFromExcel(IFormFile file);
		MemoryStream ExportInExcel(ApplicationTwoDto dto, List<RecordEntryDto> records, params Expression<Func<RecordEntryDto, ExcelTableTuple>>[] expr);
	}
}
