using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Common.Enums;

namespace StudentCredit.Application.Credits.Dtos
{
    public class SearchCreditFilter
	{
		public int? BankId { get; set; }

		public string Uin { get; set; }

		public string CreditNumber { get; set; }

		public string Uan { get; set; }

		public DateTime? ContractDate { get; set; }

		public CreditType? Type { get; set; }

		public int? InstitutionId { get; set; }

		public string StudentFullName { get; set; }

		public CommitState CommitState { get; set; }

		public int Limit { get; set; } = 10;
		public int Offset { get; set; } = 0;
	}
}
