using StudentCredit.Data.Common.Interfaces;

namespace StudentCredit.Data.Applications.ApplicationTwo
{
    public class RecordEntry : IEntity
    {
        public int Id { get; set; }

		public int ApplicationTwoId { get; set; }
		public ApplicationTwo ApplicationTwo { get; set; }

		// Номер на кредита
		public string CreditNumber { get; set; }

		// Имена на студента
		public string StudentFullName { get; set; }

		// ЕГН/ЛНЧ
		public string Uin { get; set; }

		// Общ размер на кредита
		public decimal? TotalSum { get; set; }

		// Усвоена главница
		public decimal? PrincipalAbsorbed { get; set; }

		// Лихва
		public decimal? Interest { get; set; }

		// Капитализирана главница
		public decimal? CapitalizedPrincipal { get; set; }

		// Погасен
		public bool? IsRepaid { get; set; }

		// Месечна вноска
		public decimal? MonthlyPayment { get; set; }

		// Погасена месечна вноска - главница
		public decimal? RepaidMonthlyPrincipal { get; set; }

		// Погасена месечна вноска - лихва
		public decimal? RepaidMonthlyInterest { get; set; }
	}
}
