using StudentCredit.Data.Common.Models;

namespace StudentCredit.Data.Nomenclatures
{
	public class Country : Nomenclature
	{
		public string NameEn { get; set; }

		public string Code { get; set; }

		public bool EuCountry { get; set; }
		public bool EeaCountry { get; set; }
	}
}
