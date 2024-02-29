using StudentCredit.Data.Rdpzsd.Base;
using StudentCredit.Data.Rdpzsd.Nomenclatures;

namespace StudentCredit.Data.Rdpzsd.Students.Parts.Base
{
	public class BasePersonSemester : EntityVersion
	{
		public int PartId { get; set; }
		public int StudentStatusId { get; set; }
		[Skip]
		public StudentStatus StudentStatus { get; set; }
		public int StudentEventId { get; set; }
        [Skip]
        public StudentEvent StudentEvent { get; set; }
        public int? RelocatedFromPartId { get; set; }

		public string Note { get; set; }

		public bool HasScholarship { get; set; }
		public bool UseHostel { get; set; }
		public bool UseHolidayBase { get; set; }
		public bool ParticipatedIntPrograms { get; set; }
	}
}
