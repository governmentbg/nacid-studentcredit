using StudentCredit.Data.Rdpzsd.Base;
using StudentCredit.Data.Rdpzsd.Enums;
using StudentCredit.Data.Rdpzsd.Nomenclatures;
using StudentCredit.Data.Rdpzsd.Students.Parts.Base;

namespace StudentCredit.Data.Rdpzsd.Students.Parts.PersonStudent
{
	public class PersonStudentSemester : BasePersonSemester
	{
		public int PeriodId { get; set; }
		[Skip]
		public Period Period { get; set; }
		public CourseType Course { get; set; }
	}
}
