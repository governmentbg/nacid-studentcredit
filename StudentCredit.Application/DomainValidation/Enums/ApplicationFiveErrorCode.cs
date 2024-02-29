namespace StudentCredit.Application.DomainValidation.Enums
{
	public enum ApplicationFiveErrorCode
	{
		BankEmpty = 501,
		CreditCountEmpty = 502,
		YearEmpty = 503,
		PeriodEmpty = 504,
		FromDateEmpty = 505,
		ToDateEmpty = 506,
		AttachedFileEmpty = 507,
		CreditCountMustBePositive = 508,
		AmountRequestedMustBePositive = 509
	}
}
