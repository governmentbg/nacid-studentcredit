using StudentCredit.Data.Rdpzsd.Base;
using StudentCredit.Data.Rdpzsd.Students.Interfaces;
using StudentCredit.Data.Rdpzsd.Students.Parts.Base;

namespace StudentCredit.Data.Rdpzsd.Students.Parts.PersonStudent
{
	public class PersonStudent : BasePersonStudentDoctoral<PersonStudentInfo, PersonStudentSemester>,
	   IMultiPart<PersonStudent, PersonLot>
	{
		public string FacultyNumber { get; set; }

		public string Qualification { get; set; }

		public int LotId { get; set; }
		[Skip]
		public PersonLot Lot { get; set; }
        public DateTime? GraduationDate { get; set; }
    }
}
