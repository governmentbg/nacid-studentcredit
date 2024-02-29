namespace StudentCredit.Application.DomainValidation.Enums
{
	public enum ApplicationOneErrorCode
	{
		ApplicationOneTypeEmpty = 101,
		BankEmpty = 102,
		BulstatEmpty = 103,
		InvalidStudentName = 104,
		CreditNumberEmpty = 105,
		CancelConditionEmpty = 106,
		PaymentPeriodEmpty = 107,
		CreditSizeEmpty = 108,
		SemesterTaxesEmpty = 109,
		InterestEmpty = 110,
		ExpirationOfGracePeriodEmpty = 111,
		InvalidCyrillic = 112,
		ValueMustBeGreaterThanZero = 113,
		PrincipalAbsorbedEmpty = 114,
		InterestDueEmpty = 115,
		OverallSizeEmpty = 116,
		EarlyDemandOfTheLoanEmpty = 117,
		ForcePaymentDateEmpty = 118,
		ForcePaymentInfoEmpty = 119,
		RecontractionDateEmpty = 120,
		InvalidLength = 121,
		CannotAddNewCreditToInActiveBank = 122
	}
}
