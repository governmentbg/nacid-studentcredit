using StudentCredit.Data.Rdpzsd.Enums;
using StudentCredit.Data.Rdpzsd.Students.Parts.Base;

namespace StudentCredit.Data.Rdpzsd.Students.Parts.PersonDoctoral
{
	public class PersonDoctoralSemester : BasePersonSemester
	{
		public DateTime ProtocolDate { get; set; }
		public string ProtocolNumber { get; set; }
		public YearType YearType { get; set; }
		public string SemesterRelocatedNumber { get; set; }
		public DateTime SemesterRelocatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DefenseDate { get; set; }
    }
}
