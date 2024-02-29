using StudentCredit.Application.Students.Enums;

namespace StudentCredit.Application.Students.Dtos
{
    public class StudentFilterDto
    {
        public string Identificator { get; set; }
        public SearchIdentificationType IdentificationType { get; set; }

        public int? EducationalQualificationId { get; set; }
        public int? EducationalFormId { get; set; }
    }
}