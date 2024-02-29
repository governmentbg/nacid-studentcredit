using System.ComponentModel;

namespace StudentCredit.Data.Applications.ApplicationOne.Enums
{
	public enum ApplicationOneStatus
	{
		// ОТКАЗ ЗА СКЛЮЧВАНЕ НА ДОГОВОР ЗА КРЕДИТ
		[Description("Отказан")]
		Denied = 1,

		// СКЛЮЧВАНЕ НА ДОГОВОР ЗА КРЕДИТ
		[Description("Тече гратисен период")]
		NewContract = 2,

		// ПРЕДОГОВАРЯНЕ НА УСЛОВИЯТА
		[Description("Предоговорен")]
		Renegotiated = 3,

		// ИЗТИЧАНЕ НА ГРАТИСНИЯ ПЕРИОД
		[Description("В процес на погасяване")]
		ExpirationOfFreePeriod = 4,

		// НАСТЪПВАНЕ НА ПРЕДСРОЧНА ИЗИСКУЕМОСТ | ПРЕДПРИЕМАНЕ НА ДЕЙСТВИЯ ПО ПРИНУДИТЕЛНО ИЗПЪЛНЕНИЕ
		[Description("Предсрочно изискуем")]
		RequiredInAdvance = 5,

		// ПОГАСЯВАНЕ НА КРЕДИТНОТО ЗАДЪЛЖЕНИЕ
		[Description("Погасен от кредитополучател")]
		RepaidByBorrower = 6,

		// АКТИВИРАНА ДЪРЖАВНАТА ГАРАНЦИЯ
		[Description("Погасен от държавата")]
		RepaidByState = 7
	}
}
