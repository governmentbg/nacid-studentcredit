using StudentCredit.Data.Common.Models;
using StudentCredit.Data.Nomenclatures.Enums;
using StudentCredit.Data.Rdpzsd.Enums;

namespace StudentCredit.Data.Nomenclatures
{
	public class Institution : Nomenclature
	{
		public int? InstitutionTypeId { get; set; }
		public int ExternalId { get; set; }
		public List<InstitutionSpeciality> InstitutionSpecialities { get; set; } = new List<InstitutionSpeciality>();
		public int RootId { get; set; }
		public int? ParentId { get; set; }
		public Level Level { get; set; }
		public InstitutionOwnershipType? InstitutionOwnershipType { get; set; }
		public string Uic { get; set; }

		public Institution()
		{

		}

		public Institution(string name, bool isActive, int? institutionTypeId, int externalId, int rootId, int? parentId, Level level, string uic)
		{
			this.Id = externalId;
			this.ExternalId = externalId;
			this.Name = name;
			this.IsActive = isActive;
			this.InstitutionTypeId = institutionTypeId;
			this.RootId = rootId;
			this.ParentId = parentId;
			this.Level = level;
			this.Uic = uic;
		}

		public void Update(string name, bool isActive, int? institutionTypeId, int externalId, int rootId, int? parentId, Level level, string uic)
		{
			this.ExternalId = externalId;
			this.Name = name;
			this.IsActive = isActive;
			this.InstitutionTypeId = institutionTypeId;
			this.RootId = rootId;
			this.ParentId = parentId;
			this.Level = level;
			this.Uic = uic;
		}
	}
}
