using StudentCredit.Data.Rdpzsd.Base;
using StudentCredit.Data.Rdpzsd.Enums;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonBasic;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonDoctoral;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonStudent;

namespace StudentCredit.Data.Rdpzsd.Students
{
	public class PersonLot : EntityVersion
	{
		public string Uan { get; set; }
		public LotState State { get; set; }

		public int CreateUserId { get; set; }
		public DateTime CreateDate { get; set; }
		public int? CreateInstitutionId { get; set; }

		public PersonBasic PersonBasic { get; set; }
		public List<PersonStudent> PersonStudents { get; set; } = new List<PersonStudent>();
		public List<PersonDoctoral> PersonDoctorals { get; set; } = new List<PersonDoctoral>();
	}
}
