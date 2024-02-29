namespace StudentCredit.Application.Reports.Dtos.FulfillmentOfLimits
{
	public class FulfillmentOfLimitsInfoDto
	{
		public string BankName { get; set; }

		public decimal BankLimit { get; set; }

		public decimal BankCreditsSize { get; set; }

		public decimal BankRemainderOfTheApprovedAmount { get; set; }
		
		public decimal FulfillmentOfLimit { get; set; }

		public int CreditsCount { get; set; }
	}
}
