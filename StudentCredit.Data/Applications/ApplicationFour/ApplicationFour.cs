using StudentCredit.Data.Applications.ApplicationOne.Enums;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Applications.Common.Models;
using StudentCredit.Data.Banks;
using StudentCredit.Data.Nomenclatures;

namespace StudentCredit.Data.Applications.ApplicationFour
{
    public class ApplicationFour : ApplicationCommit
    {
        public int ApplicationFourTypeId { get; set; }
        public ApplicationFourType ApplicationFourType { get; set; }

        // Дата на подаване на Приложение 4
        public DateTime BlankDate { get; set; }

        public int? ApplicationOneId { get; set; }
        public ApplicationOne.ApplicationOne ApplicationOne { get; set; }

        public decimal? BankExpenses { get; set; }

        #region BankInfo
        // Търговска банка
        public Bank Bank { get; set; }

		// Булстат
		public string BULSTAT { get; set; }

        // Банкова сметка за превеждане на сумата за заплащане на непогасената част от задължението
        public string BankAccount { get; set; }
		#endregion

		#region StudentInfo

		// Id на обучението в Рдпзсд
		public int? PersonStudentDoctoralId { get; set; }

		// Име на кредитополучателя
		public string StudentFullName { get; set; }

        // ЕГН/ЛНЧ
        public string Uin { get; set; }

        // Висше училище
        public int InstitutionId { get; set; }
        public Institution Institution { get; set; }

        // Специалност
        public int? SpecialityId { get; set; }
        public Speciality Speciality { get; set; }

        // ЕАН
        public string UAN { get; set; }

        // Факултетен №/Заповед за зачисляване на докторант №
        public string FacultyNumber { get; set; }

        // Тип на обучение
		public EducationType EducationType { get; set; }
		#endregion

		#region CreditInfo
		// Номер на кредита
		public string CreditNumber { get; set; }

        // Вид на кредита
        public CreditType CreditType { get; set; }

        // Дата на сключване на договора за кредит
        public DateTime ContractDate { get; set; }

        // Оставащ срок на обучение в месеци
        public int? SchoolRemaining { get; set; }

        // Срок на издължаване в месеци
        public int PaymentPeriod { get; set; }

        // Размер на дължимата лихва
        public decimal Interest { get; set; }

        // Начин на определяне на размера на кредита
        public string Description { get; set; }

        // Размер на непогасената част на задължението:
        public decimal OutstandingDebtAmount { get; set; }

        // Доказателства за размера на непогасената част (извлечение, справка)
        // public List<AttachedFile> OutstandingDebtAmountProof { get; set; }

        // Списък с приложения
        public List<ApplicationFourAttachedFile> Files { get; set; } = new List<ApplicationFourAttachedFile>();
		#endregion

		public ExistingEnum? SpecialityEnum { get; set; }
		public string MigrationSpecialityName { get; set; }
	}
}
