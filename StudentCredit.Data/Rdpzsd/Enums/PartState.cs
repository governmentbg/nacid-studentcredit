using System.ComponentModel;

namespace StudentCredit.Data.Rdpzsd.Enums
{
	[Description("Статус на партовете")]
	public enum PartState
	{
		[Description("Актуално състояние")]
		Actual = 1,

		[Description("Изтрит")]
		Erased = 2
	}
}
