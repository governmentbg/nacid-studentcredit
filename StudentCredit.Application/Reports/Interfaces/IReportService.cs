using StudentCredit.Application.Common.Dtos;
using StudentCredit.Application.Reports.Dtos.Reports;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Reports.Interfaces
{
    public interface IReportService : ITransientService
    {
        Task<SearchResultItemReportDto<ReportSearchResultItemDto>> GetReportSearchResultItem(SearchReportFilter filter);
        MemoryStream ExportReportsExcel(List<ReportSearchResultItemDto> items, SearchReportFilter filter);
    }
}