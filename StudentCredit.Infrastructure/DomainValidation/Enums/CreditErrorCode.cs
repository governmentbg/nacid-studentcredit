namespace StudentCredit.Infrastructure.DomainValidation.Enums
{
	public enum CreditErrorCode
	{
		StudentNoActiveEducation = 301,
		CreditExist = 302,
		CreditDoesntExist = 303,
		CreditNumberMissmatch = 304,
		ImportXmlError = 305,
		CreditExistWithRefuseContract = 306,
		CreditExistWithNewContract = 307,
		CannotAddRefuseCreditToExistingCredit = 308,
		MissingNewOrRefusedCredit = 309,
		ApplicationTwoExcelWorkSheetIsNull = 310,
        CreditExistWithRefuseContractCannotCreateAppFour = 311,
        ApplicationFourAlreadyInProcess = 312,
		ApplicationOneNotApproved = 313,
		ApplicationOnePaidByApplicationFour = 314,
		DoctoralNoActiveEducation = 315
    }
}
