using StudentCredit.Data.Applications.ApplicationOne.Enums;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Common.Enums;
using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Credits.Dtos
{
    public class CreditSearchResultItemDto
	{
		public int Id { get; set; }

		public DateTime ContractDate { get; set; }

		public string CreditNumber { get; set; }

		public CreditType CreditType { get; set; }

		public NomenclatureDto Bank { get; set; }

		public string StudentFullName { get; set; }

		public string Uin { get; set; }

		public string Uan { get; set; }

		public EducationType EducationType { get; set; }

		public NomenclatureDto EducationalQualification { get; set; }

		public string Institution { get; set; }

		public DateTime? BirthDate { get; set; }

		public CommitState CommitState { get; set; }

		public int? RootId { get; set; }

		public bool? PaidByApplicationFour { get; set; }

		public NomenclatureDto ApplicationType { get; set; }

		public int? ApplicationOneId { get; set; }
	}
}
