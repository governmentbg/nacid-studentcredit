using StudentCredit.Data.Rdpzsd.Enums;

namespace StudentCredit.Data.Rdpzsd.Nomenclatures.Base
{
	public class NomenclatureHierarchy : NomenclatureCode
	{
		public int RootId { get; set; }
		public int? ParentId { get; set; }
		public Level Level { get; set; }
	}
}
