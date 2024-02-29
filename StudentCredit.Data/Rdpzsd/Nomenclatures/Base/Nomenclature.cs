using StudentCredit.Data.Rdpzsd.Base;

namespace StudentCredit.Data.Rdpzsd.Nomenclatures.Base
{
	public class Nomenclature : EntityVersion
	{
		public string Name { get; set; }
		public string NameAlt { get; set; }
		public string Alias { get; set; }

		public bool IsActive { get; set; } = true;

		public int? ViewOrder { get; set; }
	}
}
