using StudentCredit.Data.Common.Interfaces;

namespace StudentCredit.Data.Nomenclatures
{
	public class InstitutionSpeciality : IEntity
	{
		public int Id { get; set; }
		public int? ViewOrder { get; set; }
		public bool IsActive { get; set; }
		public int ExternalId { get; set; }
		public int InstitutionId { get; set; }
		public Institution Institution { get; set; }
		public int SpecialityId { get; set; }
		public Speciality Speciality { get; set; }
		public int? EducationalFormId { get; set; }
		public EducationFormType EducationalForm { get; set; }
		public double? Duration { get; set; }
		public int? ResearchAreaId { get; set; }
		public ResearchArea ResearchArea { get; set; }
	}
}
