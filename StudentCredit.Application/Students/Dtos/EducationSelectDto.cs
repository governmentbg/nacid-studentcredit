using StudentCredit.Data.Rdpzsd.Enums;
using StudentCredit.Data.Rdpzsd.Nomenclatures;
using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Students.Dtos
{
	public class EducationSelectDto
	{
		public int PersonStudentDoctoralId { get; set; }
		public NomenclatureDto Institution { get; set; }
		public NomenclatureDto ResearchArea { get; set; }
		public NomenclatureDto Speciality { get; set; }
		public NomenclatureDto EducationFormType { get; set; }
		public NomenclatureDto EducationalQualification { get; set; }
		public CourseType? StudentCourse { get; set; }
		public YearType? DoctoralYear { get; set; }
		public string FacultyNumber { get; set; }
		public StudentStatus StudentStatus { get; set; }
		public NomenclatureDto Nationality { get; set; }

	}
}
