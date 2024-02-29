using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Reports.Dtos.SummaryReports
{
    public class SheetDto : SheetRowDto
    {
        public int Id { get; set; }

        public NomenclatureDto Year { get; set; }

        public NomenclatureDto Bank { get; set; }

        public decimal? Limit { get; set; }

        public decimal? FulfillmentOfTheLimit { get; set; }

        public List<SheetYearDto> SheetList { get; set; } = new List<SheetYearDto>();
    }
}
