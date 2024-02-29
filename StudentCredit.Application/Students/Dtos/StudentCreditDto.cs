using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Infrastructure.Helpers.Dtos;

namespace StudentCredit.Application.Students.Dtos
{
    public class StudentCreditDto
    {
        public CreditType? CreditType { get; set; }
        public DateTime? ContractDate { get; set; }
        public string BankName { get; set; }
        public string EducationalQualificationName { get; set; }
        public string SpecialityName { get; set; }
        public NomenclatureDto ApplicationOneType { get; set; }
    }
}
