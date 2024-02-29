using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Reports.Dtos.SummaryReports
{
    public class SheetYearDto : SheetRowDto
    {
        public int Id { get; set; }

        public NomenclatureDto Year { get; set; }

        public List<MonthDataDto> SheetList { get; set; } = new List<MonthDataDto>();
    }
}
