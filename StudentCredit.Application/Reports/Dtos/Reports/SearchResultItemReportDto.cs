using StudentCredit.Application.Common.Dtos;

namespace StudentCredit.Application.Reports.Dtos.Reports
{
    public class SearchResultItemReportDto<T> : SearchResultItemDto<T>
    {
        public int TotalCreditsCount { get; set; }
        public decimal? TotalCreditsSize { get; set; }
        public int TotalCreditsRefusedCount { get; set; }
    }
}