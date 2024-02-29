using StudentCredit.Data.Common.Models;

namespace StudentCredit.Data.Nomenclatures
{
	public class Speciality : Nomenclature
	{
		public int ExternalId { get; set; }
		public int? EducationalQualificationId { get; set; }
		public EducationalQualification EducationalQualification { get; set; }

		public Speciality()
		{

		}

		public Speciality(string name, int externalId, int? educationalQualificationId, bool isActive)
		{
			this.Name = name;
			this.ExternalId = externalId;
			this.EducationalQualificationId = educationalQualificationId;
			this.IsActive = isActive;
		}

		public Speciality(int id, string name, int externalId, int? educationalQualificationId, bool isActive)
		{
			this.Id = id;
			this.Name = name;
			this.ExternalId = externalId;
			this.EducationalQualificationId = educationalQualificationId;
			this.IsActive = isActive;
		}

		public void Update(string name, int externalId, int? educationalQualificationId, bool isActive)
		{
			this.Name = name;
			this.ExternalId = externalId;
			this.EducationalQualificationId = educationalQualificationId;
			this.IsActive = isActive;
		}
	}
}
