using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Reports.Dtos.FulfillmentOfLimits
{
    public class FulfillmentOfLimitsReportDto
    {
        public NomenclatureDto Year { get; set; }

        public decimal YearLimit { get; set; }

        public List<FulfillmentOfLimitsInfoDto> FulfillmentOfLimits { get; set; } = new List<FulfillmentOfLimitsInfoDto>();
    }
}
