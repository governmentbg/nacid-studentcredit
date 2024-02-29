using StudentCredit.Data.Rdpzsd.Base;
using StudentCredit.Data.Rdpzsd.Nomenclatures;
using StudentCredit.Data.Rdpzsd.Nomenclatures.Institution;
using StudentCredit.Data.Rdpzsd.Nomenclatures.Settlement;

namespace StudentCredit.Data.Rdpzsd.Students.Parts.Base
{
	public abstract class BasePersonStudentDoctoral<TPartInfo, TSemester> : Part<TPartInfo>
	   where TPartInfo : PartInfo
		where TSemester : BasePersonSemester, new()
	{
		public int InstitutionId { get; set; }
		[Skip]
		public Institution Institution { get; set; }

		public int? SubordinateId { get; set; }
		[Skip]
		public Institution Subordinate { get; set; }

		public int InstitutionSpecialityId { get; set; }
		[Skip]
		public InstitutionSpeciality InstitutionSpeciality { get; set; }
		public int StudentStatusId { get; set; }
		[Skip]
		public StudentStatus StudentStatus { get; set; }

		public int? CitizenshipId { get; set; }
		[Skip]
		public Country Citizenship { get; set; }
		public List<TSemester> Semesters { get; set; } = new List<TSemester>();
	}
}
