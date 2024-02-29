using Microsoft.AspNetCore.Http;
using StudentCredit.Application.Reports.Dtos.SummaryReports;
using StudentCredit.Application.Reports.Enums;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Reports.Interfaces
{
    public interface ISummaryReportService : ITransientService
	{
		Task<SheetDto> GetSummaryReport(int yearId);

		Task<SheetDto> GetSummaryReportByBank(int yearId, int bankId);

		Task<List<SheetDto>> GetAllSheets(SummaryReportSearchDto dto);

		Task<SheetDto> UpdateMonthData(int sheetId, MonthDataDto dto);

		Task<SheetDto> ChangeBankYearLimit(int yearId, decimal? yearLimit);

		Task<SheetDto> ChangeYearLimit(int yearId, decimal? yearLimit);

		Task CreateSheetYearsMonth();

		Task CreateSheetYears();

		Task CreateSheets();

		Task ImportSummaryReportFromExcel(IFormFile file);

		MemoryStream ExportInExcel(List<SheetDto> sheets);
	}
}
