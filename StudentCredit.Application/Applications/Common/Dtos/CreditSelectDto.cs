using StudentCredit.Data.Applications.ApplicationOne.Enums;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Applications.Common.Dtos
{
    public class CreditSelectDto
    {
        public int? ApplicationOneId { get; set; }

        public NomenclatureDto Bank { get; set; }
        public string BULSTAT { get; set; }

		public int? PersonStudentDoctoralId { get; set; }
		public string StudentFullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Uin { get; set; }
        public NomenclatureDto Institution { get; set; }
        public NomenclatureDto Speciality { get; set; }
        public string Uan { get; set; }
        public string FacultyNumber { get; set; }
		public EducationType EducationType { get; set; }

		public string CreditNumber { get; set; }
        public CreditType CreditType { get; set; }
        public DateTime? ContractDate { get; set; }
        public int? PaymentPeriod { get; set; }
        public decimal? Interest { get; set; }
        public string Description { get; set; }

		public ExistingEnum? SpecialityEnum { get; set; }
		public string MigrationSpecialityName { get; set; }

		public ExistingEnum? ResearchAreaEnum { get; set; }
		public string MigrationResearchAreaName { get; set; }
	}
}
