using System.ComponentModel;

namespace StudentCredit.Data.Applications.ApplicationFive.Enums
{
	public enum ApplicationFiveType
	{
		[Description("Дължими суми по чл. 39, т. 1")]
		TotalDebtExposure = 1,

		[Description("Дължими суми по чл. 39, т. 2")]
		RepaidCreditObligationsInTheRelevantYear = 2,

		[Description("Дължими суми по чл. 39, т. 3")]
		BankExpensesForTheEnforcementActions = 3
	}
}
