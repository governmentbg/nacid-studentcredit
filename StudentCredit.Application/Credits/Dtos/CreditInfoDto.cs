using StudentCredit.Application.Applications.ApplicationsTwo.Dtos;
using StudentCredit.Data.Applications.ApplicationOne.Enums;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Common.Enums;
using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Credits.Dtos
{
    public class CreditInfoDto
	{
		public int Id { get; set; }
		public CommitState CommitState { get; set; }
		public bool? PaidByApplicationFour { get; set; }
		public NomenclatureDto ApplicationOneType { get; set; }

		public string StudentFullName { get; set; }
		public string Uin { get; set; }
		public NomenclatureDto Institution { get; set; }
		public NomenclatureDto ResearchArea { get; set; }
		public NomenclatureDto Speciality { get; set; }
		public NomenclatureDto EducationalQualification { get; set; }
		public string Uan { get; set; }
		public EducationType EducationType { get; set; }
		public DateTime? BirthDate { get; set; }
		public ExistingEnum? SpecialityEnum { get; set; }
		public string MigrationSpecialityName { get; set; }

		public ExistingEnum? ResearchAreaEnum { get; set; }
		public string MigrationResearchAreaName { get; set; }

		public NomenclatureDto Bank { get; set; }

		public DateTime? ContractDate { get; set; }
		public string CreditNumber { get; set; }
		public CreditType CreditType { get; set; }
		public DateTime? ExpirationDateOfGracePeriod { get; set; }
		public int? PaymentPeriod { get; set; }
		public decimal? CreditSize { get; set; }

		public List<CreditApplicationDto> ApplicationsOne { get; set; } = new List<CreditApplicationDto>();
		public List<RecordEntryDto> ApplicationsTwo { get; set; } = new List<RecordEntryDto>();
		public List<CreditApplicationDto> ApplicationsFour { get; set; } = new List<CreditApplicationDto>();
	}
}
