using StudentCredit.Data.Applications.ApplicationOne.Enums;

namespace StudentCredit.Application.Students.Dtos
{
    public class StudentSelectSearchFilter
    {
        public string Uan { get; set; }

        public EducationType EducationType { get; set; }
    }
}
