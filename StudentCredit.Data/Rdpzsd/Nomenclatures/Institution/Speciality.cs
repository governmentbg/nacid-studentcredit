using StudentCredit.Data.Rdpzsd.Base;
using StudentCredit.Data.Rdpzsd.Nomenclatures.Base;

namespace StudentCredit.Data.Rdpzsd.Nomenclatures.Institution
{
	public class Speciality : NomenclatureCode
	{
		public int? ResearchAreaId { get; set; }
		[Skip]
		public ResearchArea ResearchArea { get; set; }

		public int EducationalQualificationId { get; set; }
		[Skip]
		public EducationalQualification EducationalQualification { get; set; }

		public bool IsRegulated { get; set; }
	}
}
