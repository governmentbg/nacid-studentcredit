using StudentCredit.Data.Rdpzsd.Base;
using StudentCredit.Data.Rdpzsd.Students.Parts.Base;

namespace StudentCredit.Data.Rdpzsd.Students.Parts.PersonStudent
{
	public class PersonStudentInfo : PartInfo
	{
		[Skip]
		public PersonStudent PersonStudent { get; set; }
	}
}
