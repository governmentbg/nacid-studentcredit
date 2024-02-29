using StudentCredit.Data.Rdpzsd.Base;
using StudentCredit.Data.Rdpzsd.Nomenclatures.Settlement;
using StudentCredit.Data.Rdpzsd.Students.Parts.Base;

namespace StudentCredit.Data.Rdpzsd.Students.Parts.PersonBasic.Base
{
	public abstract class BasePersonBasic<TPartInfo> : Part<TPartInfo>
		where TPartInfo : PartInfo
	{
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string OtherNames { get; set; }
		public string FullName { get; set; }
		public string Uin { get; set; }
        public string ForeignerNumber { get; set; }
        public string IdnNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int CitizenshipId { get; set; }
		[Skip]
		public Country Citizenship { get; set; }
		public int? SecondCitizenshipId { get; set; }
		[Skip]
		public Country SecondCitizenship { get; set; }
	}
}
