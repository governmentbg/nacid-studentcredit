using StudentCredit.Data.Applications.ApplicationFive.Enums;
using StudentCredit.Data.Common.Enums;

namespace StudentCredit.Application.Applications.ApplicationsFive.Dtos
{
	public class SearchApplicationFiveFilter
	{
		public int? BankId { get; set; }

		public DateTime? From { get; set; }

		public DateTime? To { get; set; }

		public ApplicationFiveType? ApplicationFiveType { get; set; }

		public CommitState? CommitState { get; set; }

		public int Limit { get; set; } = 10;
		public int Offset { get; set; } = 0;
	}
}
