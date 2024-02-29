using StudentCredit.Data.Rdpzsd.Enums;
using StudentCredit.Data.Rdpzsd.Nomenclatures.Base;

namespace StudentCredit.Data.Rdpzsd.Nomenclatures
{
	public class Period : Nomenclature
	{
		public int Year { get; set; }
		public Semester Semester { get; set; }
	}
}
