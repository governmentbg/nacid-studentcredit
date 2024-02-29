using StudentCredit.Data.Applications.ApplicationOne.Enums;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Applications.Common.Models;
using StudentCredit.Data.Banks;
using StudentCredit.Data.Common.Interfaces;
using StudentCredit.Data.Nomenclatures;
using StudentCredit.Data.Rdpzsd.Enums;

namespace StudentCredit.Data.Applications.ApplicationOne
{
    public class ApplicationOne : ApplicationCommit, IEntity, IAuditable
	{
		public int ApplicationOneTypeId { get; set; }
		public ApplicationOneType ApplicationOneType { get; set; }

		// Дата на подаване на Приложение 1
		public DateTime BlankDate { get; set; }

		public ApplicationOneStatus ApplicationOneStatus { get; set; }

		public bool? PaidByApplicationFour { get; set; }
        public DateTime? PaidByApplicationFourDate { get; set; }

        #region ДАННИ ЗА БАНКАТА
        // Име на търговската банка
		public Bank Bank { get; set; }

		// Булстат
		public string BULSTAT { get; set; }

		// Лице за контакт
		public string ContactPerson { get; set; }
		#endregion

		#region ДАННИ ЗА КРЕДИТОПОЛУЧАТЕЛЯ

		// Id на обучението в Рдпзсд
		public int? PersonStudentDoctoralId { get; set; }

		// Студент/Докторант
		public EducationType EducationType { get; set; }

		// Имена на студента
		public string StudentFullName { get; set; }

		// Гражданство
		public int? NationalityId { get; set; }
		public Country Nationality { get; set; }

		// Второ Гражданство
		public int? SecondNationalityId { get; set; }
		public Country SecondNationality { get; set; }

		// ЕГН/ЛНЧ
		public string Uin { get; set; }

		// Номер на документ за самоличност
		public string IdNumber { get; set; }

		// Висше училище
		public int? InstitutionId { get; set; }
		public Institution Institution { get; set; }

		// Проф. направление
		public int? ResearchAreaId { get; set; }
		public ResearchArea ResearchArea { get; set; }

		// Специалност
		public int? SpecialityId { get; set; }
		public Speciality Speciality { get; set; }

		// ОКС/ОНС
		public int? EducationalQualificationId { get; set; }
		public EducationalQualification EducationalQualification { get; set; }

		// Форма на обучение
		public int? EducationFormTypeId { get; set; }
		public EducationFormType EducationFormType { get; set; }

		// Курс на студент
		public CourseType? StudentCourse { get; set; }

		// Година на обучение на докторант
		public YearType? DoctoralYear { get; set; }

		// Факултетен номер/Заповед за зачисляване на докторант номер
		public string FacultyNumber { get; set; }

		// ЕАН
		public string Uan { get; set; }

		// Дата на раждане
		public DateTime? BirthDate { get; set; }

        // Възрастта към датата на теглене на кредит
        public int? AgeAtContract { get; set; }
        #endregion

        #region ДАННИ ЗА СКЛЮЧВАНЕ ИЛИ ОТКАЗ ЗА СКЛЮЧВАНЕ НА ДОГОВОР ЗА КРЕДИТ

        // Вид кредит
        public CreditType? CreditType { get; set; }

		// Дата на сключване на договора/отказване на договора
		public DateTime? ContractDate { get; set; }

		// Номер на кредита
		public string CreditNumber { get; set; }

		// Оставаш срок на обучение в месеци
		public int? SchoolRemaining { get; set; }

		// Причини за отказ
		public string CancelCondition { get; set; }

		// Срок на издължаване в месеци
		public int? PaymentPeriod { get; set; }

		// Максимален размер на кредита
		public decimal? CreditSize { get; set; }

		// Семестриална такса
		public decimal? SemesterTax { get; set; }

		// Лихва
		public decimal? Interest { get; set; }

		// Описание
		public string Description { get; set; }

		#endregion

		#region ДАННИ ЗА ГРАТИСНИЯ ПЕРИОД

		// Дата на изтичане на гратисния период
		public DateTime? ExpirationDateOfGracePeriod { get; set; }

		// Месечна вноска
		public decimal? MonthlyPayment { get; set; }

		// Информация за договорения погасителен план?
		public string PaymentDescription { get; set; }

		// Усвоена главница
		public decimal? PrincipalAbsorbed { get; set; }

		// Дължима лихва
		public decimal? InterestDue { get; set; }

		// Общ размер
		public decimal? OverallSize { get; set; }

		#endregion

		#region ДАННИ ЗА ПОГАСЯВАНЕ НА КРЕДИТА

		// Дата на погасяване на кредита
		public DateTime? PaymentDate { get; set; }

		// Дата на настъпване на предсрочно погасяване
		public DateTime? EarlyPaymentDate { get; set; }

		#endregion

		#region ДАННИ ЗА ПРЕДОГОВАРЯНЕ НА КРЕДИТА

		// Тип на предоговаряне на кредита
		public RecontractionType? RecontractionType { get; set; }
		// Информация за предоговаряне на условията
		public string RecontractionInfo { get; set; }

		// Дата на предоговаряне
		public DateTime? RecontractionDate { get; set; }

		// Тип на острочване
		public int? AdjournTypeId { get; set; }
		public AdjournType AdjournType { get; set; }

		// Дата на настъпване на обстоятелствата за отсрочване
		public DateTime? AdjournDate { get; set; }

		// Срок на отсрочване на кредита
		public int? AdjournPeriod { get; set; }

		// Тип на острочване
		public int? ExtensionOfGracePeriodId { get; set; }
		public ExtensionOfGracePeriod ExtensionOfGracePeriod { get; set; }

		// Нова дата на изтичане на гратисния период
		public DateTime? NewExpirationDateOfGracePeriod { get; set; }

		// Усвоена главница
		public decimal? PrincipalAbsorbedInOldBank { get; set; }

		// Дължима лихва
		public decimal? InterestDueInOldBank { get; set; }

		// Общ размер
		public decimal? OverallSizeInOldBank { get; set; }

		#endregion 

		#region НАСТЪПВАНЕ НА ПРЕДСРОЧНА ИЗИСКУЕМОСТ И ПРЕДПРИЕМАНЕ НА ДЕЙСТВИЯ ПО ПРИНУДИТЕЛНО ИЗПЪЛНЕНИЕ ПО КРЕДИТА

		// Дата на настъпване на предсрочна изискуемост на кредита
		public DateTime? EarlyDemandOfTheLoan { get; set; }

		// Дата на предприемане на принудително събиране
		public DateTime? ForcePaymentDate { get; set; }

		// Информация за  предприети действия по принудително събиране
		public string ForcePaymentInfo { get; set; }

		#endregion

		#region ПОЛЕТА ЗА МИГРАЦИЯ

		public ExistingEnum? SpecialityEnum { get; set; }
		public string MigrationSpecialityName { get; set; }

		public ExistingEnum? ResearchAreaEnum { get; set; }
		public string MigrationResearchAreaName { get; set; }
		
		#endregion
	}
}
