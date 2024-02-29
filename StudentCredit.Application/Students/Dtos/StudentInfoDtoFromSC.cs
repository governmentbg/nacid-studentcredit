using StudentCredit.Data.Rdpzsd.Enums;
using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Students.Dtos
{
    public class StudentInfoDtoFromSC
    {
        public string StudentFullName { get; set; }
        public NomenclatureDto SecondNationality { get; set; }
        public string Uin { get; set; }
        public string IdNumber { get; set; }
        public string Uan { get; set; }
        public DateTime BirthDate { get; set; }

        public NomenclatureDto Institution { get; set; }
        public NomenclatureDto EducationFormType { get; set; }
        public NomenclatureDto EducationalQualification { get; set; }
        public CourseType? StudentCourse { get; set; }
        public YearType? DoctoralYear { get; set; }
        public string FacultyNumber { get; set; }
        public NomenclatureDto Nationality { get; set; }

        public NomenclatureDto ResearchArea { get; set; }
        public NomenclatureDto Speciality { get; set; }

        public string MigrationResearchAreaName { get; set; }
        public string MigrationSpecialityName { get; set; }
    }
}