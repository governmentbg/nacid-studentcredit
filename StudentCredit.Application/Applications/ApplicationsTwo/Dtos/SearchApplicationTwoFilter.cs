
namespace StudentCredit.Application.Applications.ApplicationsTwo.Dtos
{
	public class SearchApplicationTwoFilter
	{
		public int? BankId { get; set; }

		public DateTime? FromDate { get; set; }

		public DateTime? ToDate { get; set; }

		public int Limit { get; set; } = 10;
		public int Offset { get; set; } = 0;
	}
}
