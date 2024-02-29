using StudentCredit.Data.Rdpzsd.Base;
using StudentCredit.Data.Rdpzsd.Nomenclatures.Institution;

namespace StudentCredit.Data.Rdpzsd.Students.Parts.Base
{
	public abstract class PartInfo : EntityVersion
	{
		public int? UserId { get; set; }
		public string UserFullname { get; set; }
		public DateTime ActionDate { get; set; }

		public int? InstitutionId { get; set; }
		[Skip]
		public Institution Institution { get; set; }
		public int? SubordinateId { get; set; }
		[Skip]
		public Institution Subordinate { get; set; }
	}
}
