using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Students.Dtos
{
	public class StudentInfoDto
	{
		public string StudentFullName { get; set; }
		public NomenclatureDto SecondNationality { get; set; }
		public string Uin { get; set; }
		public string IdNumber { get; set; }
		public string Uan { get; set; }
		public DateTime BirthDate { get; set; }
		public EducationSelectDto PersonStudentInfo { get; set; }
		public EducationSelectDto PersonDoctoralInfo { get; set; }
	}
}
