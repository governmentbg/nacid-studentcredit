using StudentCredit.Data.Rdpzsd.Base;
using StudentCredit.Data.Rdpzsd.Students.Interfaces;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonBasic.Base;

namespace StudentCredit.Data.Rdpzsd.Students.Parts.PersonBasic
{
	public class PersonBasic : BasePersonBasic<PersonBasicInfo>,
		ISinglePart<PersonBasic, PersonLot>
	{
		[Skip]
		public PersonLot Lot { get; set; }
	}
}
