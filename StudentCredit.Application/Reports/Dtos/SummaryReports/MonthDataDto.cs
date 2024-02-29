namespace StudentCredit.Application.Reports.Dtos.SummaryReports
{
    public class MonthDataDto : SheetRowDto
    {
        public int Id { get; set; }

        public int Month { get; set; }

        public int SheetYearId { get; set; }
    }
}
