using System.Xml.Serialization;

namespace StudentCredit.Application.Applications.ApplicationsOne.Dtos
{
	[XmlRoot(ElementName = "file_type1")]
	[Serializable]
	public class ApplicationOneXmlImportDto
	{
		[XmlElement(ElementName = "pril_1")]
		public ApplicationOneXml Prilojenie1 { get; set; }
	}

	[XmlRoot(ElementName = "pril_1")]
	[Serializable]
	public class ApplicationOneXml
	{
		[XmlElement(ElementName = "blank_type")]
		public string ApplicationOneType { get; set; }

		[XmlElement(ElementName = "bank_bulstat")]
		public string Bulstat { get; set; }

		[XmlElement(ElementName = "bank_contact")]
		public string ContactPerson { get; set; }

		[XmlElement(ElementName = "student")]
		public string EducationType { get; set; }

		[XmlElement(ElementName = "student_name")]
		public StudentName StudentName { get; set; }

		[XmlElement(ElementName = "student_egn")]
		public string Uin { get; set; }

		[XmlElement(ElementName = "student_citizen")]
		public string Nationality { get; set; }

		[XmlElement(ElementName = "student_id")]
		public string IdNumber { get; set; }

		[XmlElement(ElementName = "university")]
		public string Institution { get; set; }

		[XmlElement(ElementName = "student_subject")]
		public string ResearchArea { get; set; }

		[XmlElement(ElementName = "student_spec")]
		public string Speciality { get; set; }

		[XmlElement(ElementName = "student_course", IsNullable = true)]
		public string Course { get; set; }

		[XmlElement(ElementName = "student_number")]
		public string FacultyNumber { get; set; }


		[XmlElement(ElementName = "credit_type", IsNullable = true)]
		public string CreditType { get; set; }

		[XmlElement(ElementName = "credit_num")]
		public string CreditNumber { get; set; }

		[XmlElement(ElementName = "contract_date", IsNullable = true)]
		public DateMap ContractDate { get; set; }

		[XmlElement(ElementName = "cancel_conditions")]
		public string CancelCondition { get; set; }

		[XmlElement(ElementName = "school_remaining", IsNullable = true)]
		public string SchoolRemaining { get; set; }

		[XmlElement(ElementName = "gratis_date", IsNullable = true)]
		public DateMap ExpirationDateOfGratisPeriod { get; set; }

		[XmlElement(ElementName = "plan_info", IsNullable = true)]
		public PlanInfo PlanInfo { get; set; }

		[XmlElement(ElementName = "payment_period", IsNullable = true)]
		public string PaymentPeriod { get; set; }

		[XmlElement(ElementName = "credit_size", IsNullable = true)]
		public CreditSize CreditSize { get; set; }

		[XmlElement(ElementName = "payment_date", IsNullable = true)]
		public DateMap PaymentDate { get; set; }

		[XmlElement(ElementName = "proterm_payment_date", IsNullable = true)]
		public DateMap EarlyPaymentDate { get; set; }

		[XmlElement(ElementName = "recontraction_info")]
		public string RecontractionInfo { get; set; }

		[XmlElement(ElementName = "recontraction_date", IsNullable = true)]
		public DateMap RecontractionDate { get; set; }

		[XmlElement(ElementName = "adjourn", IsNullable = true)]
		public string AdjournType { get; set; }

		[XmlElement(ElementName = "adjourn_date", IsNullable = true)]
		public DateMap AdjournDate { get; set; }

		[XmlElement(ElementName = "adjourn_period", IsNullable = true)]
		public string AdjournPeriod { get; set; }

		[XmlElement(ElementName = "acceleration_date", IsNullable = true)]
		public DateMap EarlyDemandOfTheLoanDate { get; set; }

		[XmlElement(ElementName = "force_payment_info")]
		public string ForcePaymentInfo { get; set; }

		[XmlElement(ElementName = "force_payment_date", IsNullable = true)]
		public DateMap ForcePaymentDate { get; set; }

		[XmlElement(ElementName = "blank_date", IsNullable = true)]
		public DateMap BlankDate { get; set; }

		// New properties

		[XmlElement(ElementName = "principal_absorbed")]
		public decimal? PrincipalAbsorbed { get; set; }

		[XmlElement(ElementName = "interest_due")]
		public decimal? InterestDue { get; set; }

		[XmlElement(ElementName = "overall_size")]
		public decimal? OverallSize { get; set; }
	}

	[XmlRoot(ElementName = "student_name")]
	[Serializable]
	public class StudentName
	{
		[XmlElement(ElementName = "firstname")]
		public string FirstName { get; set; }

		[XmlElement(ElementName = "surname")]
		public string SurName { get; set; }

		[XmlElement(ElementName = "familyname")]
		public string LastName { get; set; }
	}

	[XmlRoot(ElementName = "contract_date")]
	[Serializable]
	public class DateMap
	{
		[XmlElement(ElementName = "d")]
		public int? Day { get; set; }

		[XmlElement(ElementName = "m")]
		public int? Month { get; set; }

		[XmlElement(ElementName = "y")]
		public int? Year { get; set; }
	}

	[XmlRoot(ElementName = "plan_info")]
	[Serializable]
	public class PlanInfo
	{
		[XmlElement(ElementName = "monthly")]
		public string MonthlyPayment { get; set; }

		[XmlElement(ElementName = "descr")]
		public string PaymentDescription { get; set; }
	}

	[XmlRoot(ElementName = "credit_size")]
	[Serializable]
	public class CreditSize
	{
		[XmlElement(ElementName = "size")]
		public string Size { get; set; }

		[XmlElement(ElementName = "sem_tax")]
		public string SemesterTax { get; set; }

		[XmlElement(ElementName = "interest")]
		public string Interest { get; set; }

		[XmlElement(ElementName = "descr")]
		public string Description { get; set; }
	}
}
