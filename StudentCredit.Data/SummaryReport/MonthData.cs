using StudentCredit.Data.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudentCredit.Data.SummaryReport
{
	public class MonthData : SheetRowData, IEntity
	{
		public int Id { get ; set; }

		public int SheetYearId { get; set; }
		public SheetYear SheetYear { get; set; }

		[Range(1, 12)]
		public int Month { get ; set; }

		public MonthData()
		{
		}

		public MonthData(int month)
		{
			this.Month = month;
			this.TotalSum = 0;
			this.RenegotiatedSum = 0;
			this.PrincipalAbsorbed = 0;
			this.RemainderToDigest = 0;
			this.InterestOverPrincipal = 0;
			this.PrincipalPaid = 0;
			this.InterestPaid = 0;
			this.DebtWrittenOff = 0;
			this.SimpleDebtPrincipal = 0;
			this.SimpleDebtInterest = 0;
			this.WarrantiesActivatedPrincipal = 0;
			this.WarrantiesActivatedInterest = 0;
			this.CreditCount = 0;
		}
	}
}