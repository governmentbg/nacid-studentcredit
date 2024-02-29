using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Students.Dtos
{
    public class EducationBasicDto
    {
        public int Id { get; set; }
        public InstitutionNomenclatureDto Institution { get; set; }

        public NomenclatureDto Speciality { get; set; }

        public NomenclatureDto EducationalQualification { get; set; }

        public NomenclatureDto EducationalForm { get; set; }

        public NomenclatureDto Status { get; set; }

        public NomenclatureDto ResearchArea { get; set; }
        public decimal? Duration { get; set; }

        //Дата на дипломиране
        public DateTime? GraduationDate { get; set; }

        //Ако е със статус "Прекъснал"
        public string InterruptionReason { get; set; }

    }
}
