using StudentCredit.Data.Nomenclatures;

namespace StudentCredit.Application.InstitutionSpecialities.Dtos
{
	public class SpecialityInformationDto
	{
		public int Id { get; set; }
		public double Duration { get; set; }
		public string Name { get; set; }
		public EducationFormType Form { get; set; }
		public EducationalQualification Qualification { get; set; }
		public Institution Institution { get; set; }
		public ResearchArea ResearchArea { get; set; }

		public SpecialityInformationDto(int specialityId, string specialityName, double duration, EducationFormType form, EducationalQualification qualification, Institution institution, ResearchArea researchArea)
		{
			this.Id = specialityId;
			this.Name = specialityName;
			this.Duration = duration;
			this.Form = form;
			this.Qualification = qualification;
			this.Institution = institution;
			this.ResearchArea = researchArea;
		}
	}
}
