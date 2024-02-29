using StudentCredit.Data.Rdpzsd.Enums;
using StudentCredit.Data.Rdpzsd.Nomenclatures.Base;

namespace StudentCredit.Data.Rdpzsd.Nomenclatures.Institution
{
	public class Institution : NomenclatureHierarchy
	{
		public int LotNumber { get; set; }
		public InstitutionCommitState? State { get; set; }

		public string Uic { get; set; }
		public string ShortName { get; set; }
		public string ShortNameAlt { get; set; }

		public OrganizationType? OrganizationType { get; set; }
		public OwnershipType? OwnershipType { get; set; }

		public int? SettlementId { get; set; }
		
		public int? MunicipalityId { get; set; }
		
		public int? DistrictId { get; set; }
		
		public bool IsResearchUniversity { get; set; }

		public List<InstitutionSpeciality> InstitutionSpecialities { get; set; } = new List<InstitutionSpeciality>();
	}
}
