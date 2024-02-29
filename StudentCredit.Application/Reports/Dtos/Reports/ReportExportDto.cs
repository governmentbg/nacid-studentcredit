namespace StudentCredit.Application.Reports.Dtos.Reports
{
    public class ReportExportDto
    {
        public List<ReportSearchResultItemDto> ReportResult { get; set; }
        public SearchReportFilter ReportFilter { get; set; }
    }
}
