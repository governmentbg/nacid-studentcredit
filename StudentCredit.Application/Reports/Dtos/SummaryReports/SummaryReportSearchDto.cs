using StudentCredit.Application.Reports.Enums;

namespace StudentCredit.Application.Reports.Dtos.SummaryReports
{
    public class SummaryReportSearchDto
    {
        public SummaryReportType SummaryReportType { get; set; }

        public int? BankId { get; set; }
    }
}
