namespace StudentCredit.Application.Applications.ApplicationsTwo.Dtos
{
	public class RecordEntryDto
	{
		public int Id { get; set; }

		public string CreditNumber { get; set; }

		public string StudentFullName { get; set; }

		public string Uin { get; set; }

		public decimal? TotalSum { get; set; }

		public decimal? PrincipalAbsorbed { get; set; }

		public decimal? Interest { get; set; }

		public decimal? CapitalizedPrincipal { get; set; }

		public bool? IsRepaid { get; set; }

		public decimal? MonthlyPayment { get; set; }

		public decimal? RepaidMonthlyPrincipal { get; set; }

		public decimal? RepaidMonthlyInterest { get; set; }

		public int ApplicationTwoId { get; set; }
	}
}
