using StudentCredit.Application.Applications.Common.Dtos;
using StudentCredit.Data.Applications.ApplicationOne.Enums;
using StudentCredit.Data.Rdpzsd.Enums;
using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Applications.ApplicationsOne.Dtos
{
	public class ApplicationOneCreditSelectDto : CreditSelectDto
	{
		public NomenclatureDto Nationality { get; set; }
		public NomenclatureDto SecondNationality { get; set; }
		public string IdNumber { get; set; }
		public NomenclatureDto ResearchArea { get; set; }
		public NomenclatureDto EducationFormType { get; set; }
		public NomenclatureDto EducationalQualification { get; set; }
		public CourseType? StudentCourse { get; set; }
		public YearType? DoctoralYear { get; set; }

		public string ContactPerson { get; set; }

		public DateTime? ExpirationDateOfGracePeriod { get; set; }
		public int? SchoolRemaining { get; set; }
		public decimal? CreditSize { get; set; }
		public decimal? SemesterTax { get; set; }

		public decimal? MonthlyPayment { get; set; }
		public string PaymentDescription { get; set; }
    }
}
