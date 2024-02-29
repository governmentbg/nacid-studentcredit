using StudentCredit.Data.Applications.Common.Enums;

namespace StudentCredit.Application.Students.Dtos
{
	public class StudentCardInfoDto
	{
		public CreditType? CreditType { get; set; }

		public DateTime? ContractDate { get; set; }

		public DateTime? ExpirationDateOfGracePeriod { get; set; }

		public DateTime? NewExpirationDateOfGracePeriod { get; set; }

		public decimal? CreditSize { get; set; }

		public decimal? PrincipalAbsorbed { get; set; }

		public decimal? InterestDue { get; set; }

		public string BankName { get; set; }

		public string Type { get; set; }

		public string CreditNumber { get; set; }

		public DateTime? PaymentDate { get; set; }
	}
}
