using StudentCredit.Data.Applications.ApplicationFive.Enums;
using StudentCredit.Data.Applications.Common.Models;
using StudentCredit.Data.Banks;
using StudentCredit.Data.Nomenclatures;

namespace StudentCredit.Data.Applications.ApplicationFive
{
    public class ApplicationFive : ApplicationCommit
	{
		// Тип на приложение 5
		public ApplicationFiveType ApplicationFiveType { get; set; }

		// Дата на подаване на Приложение 5
		public DateTime BlankDate { get; set; }

		// Искана сума
		public decimal AmountRequested { get; set; }

		// Корекция на исканата сума
		public decimal? AmountRequestedCorrection { get; set; }

		// Искана сума след корекция
		public decimal? AmountRequestedAfterCorrection { get; set; }

		// Брой кредити
		public int? CreditCount { get; set; }

		// От дата
		public DateTime? From { get; set; }

		// До дата
		public DateTime? To { get; set; }

		public Bank Bank { get; set; }

		public ApplicationFiveAttachedFile ApplicationFiveAttachedFile { get; set; }

		public YearPeriod? Period { get; set; }

		public int? YearId { get ; set; }
		public Year Year { get; set; }
	}
}
