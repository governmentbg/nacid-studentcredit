using StudentCredit.Data.Applications.ApplicationFive.Enums;
using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Applications.ApplicationsFive.Dtos
{
	public class DividendReportResultDto
	{
		public decimal TotalSum { get; set; }

		public decimal? AmountRequestedCorrection { get; set; }

		public ApplicationFiveType ApplicationFiveType { get; set; }

		public NomenclatureDto Bank { get; set; }

		public NomenclatureDto Year { get; set; }
	}
}
