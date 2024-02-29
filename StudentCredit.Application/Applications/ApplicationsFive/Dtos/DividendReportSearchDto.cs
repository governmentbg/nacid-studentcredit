using StudentCredit.Data.Applications.ApplicationFive.Enums;
using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Applications.ApplicationsFive.Dtos
{
	public class DividendReportSearchDto
	{
		public NomenclatureDto Bank { get; set; }

		public NomenclatureDto Year { get; set; }

		public ApplicationFiveType ApplicationFiveType { get; set; }

		public DateTime? From { get; set; }

		public DateTime? To { get; set; }

		public YearPeriod? Period { get; set; }
	}
}
