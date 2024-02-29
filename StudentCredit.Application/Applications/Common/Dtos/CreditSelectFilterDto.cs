using StudentCredit.Data.Applications.Common.Enums;

namespace StudentCredit.Application.Applications.Common.Dtos
{
    public class CreditSelectFilterDto
    {
        public string Uin { get; set; }

        public CreditType CreditType { get; set; }

        public string CreditNumber { get; set; }

        public int BankId { get; set; }

        public int ApplicationOneId { get; set; }
    }
}
