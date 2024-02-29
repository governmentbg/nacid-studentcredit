using StudentCredit.Data.Rdpzsd.Enums;

namespace StudentCredit.Application.InstitutionSpecialities.Dtos
{
	public class InstitutionDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsActive { get; set; }
		public int? InstitutionType { get; set; }
		public List<InstitutionSpecialityDto> InstitutionSpecialities { get; set; } = new List<InstitutionSpecialityDto>();
		public int RootId { get; set; }
		public int? ParentId { get; set; }
		public Level Level { get; set; }
		public string Uic { get; set; }
	}
}
