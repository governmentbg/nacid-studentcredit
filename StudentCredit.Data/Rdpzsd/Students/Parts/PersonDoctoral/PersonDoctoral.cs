using StudentCredit.Data.Rdpzsd.Base;
using StudentCredit.Data.Rdpzsd.Students.Interfaces;
using StudentCredit.Data.Rdpzsd.Students.Parts.Base;

namespace StudentCredit.Data.Rdpzsd.Students.Parts.PersonDoctoral
{
	public class PersonDoctoral : BasePersonStudentDoctoral<PersonDoctoralInfo, PersonDoctoralSemester>,
		IMultiPart<PersonDoctoral, PersonLot>
	{
		public DateTime StartDate { get; set; }

		public int LotId { get; set; }
		[Skip]
		public PersonLot Lot { get; set; }
	}
}
