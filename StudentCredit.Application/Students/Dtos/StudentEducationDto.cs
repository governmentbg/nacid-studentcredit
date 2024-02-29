using StudentCredit.Data.Rdpzsd.Enums;

namespace StudentCredit.Application.Students.Dtos
{
    public class StudentEducationDto : EducationBasicDto
    {
        public CourseType Course { get; set; }
        public string StartPeriod { get; set; }
        public string FinishPeriod { get; set; }
    }
}
