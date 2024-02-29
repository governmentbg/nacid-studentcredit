using StudentCredit.Data.Rdpzsd.Enums;

namespace StudentCredit.Application.Students.Dtos
{
    public class DoctoralEducationDto : EducationBasicDto
    {
        public YearType YearType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
    }
}
