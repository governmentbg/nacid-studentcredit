using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Students.Dtos
{
    public class StudentSelectDto
    {
        public string StudentFullName { get; set; }
        public NomenclatureDto SecondNationality { get; set; }
        public string Uin { get; set; }
        public string IdNumber { get; set; }
        public string Uan { get; set; }
        public bool HasActiveEducation { get; set; }
        public DateTime? BirthDate { get; set; }
        public List<EducationSelectDto> Specialities { get; set; } = new List<EducationSelectDto>();
        public List<EducationSelectDto> Doctorals { get; set; } = new List<EducationSelectDto>();
	}
}
