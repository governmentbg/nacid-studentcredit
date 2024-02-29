using StudentCredit.Data.Rdpzsd.Base;
using StudentCredit.Data.Rdpzsd.Students.Parts.Base;

namespace StudentCredit.Data.Rdpzsd.Students.Parts.PersonBasic
{
	public class PersonBasicInfo : PartInfo
	{
		[Skip]
		public PersonBasic PersonBasic { get; set; }
	}
}
