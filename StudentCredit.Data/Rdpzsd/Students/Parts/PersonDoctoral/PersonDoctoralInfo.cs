using StudentCredit.Data.Rdpzsd.Base;
using StudentCredit.Data.Rdpzsd.Students.Parts.Base;

namespace StudentCredit.Data.Rdpzsd.Students.Parts.PersonDoctoral
{
	public class PersonDoctoralInfo : PartInfo
	{
		[Skip]
		public PersonDoctoral PersonDoctoral { get; set; }
	}
}
