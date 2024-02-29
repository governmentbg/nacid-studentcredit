using StudentCredit.Data.Common.Interfaces;
using StudentCredit.Data.Nomenclatures;

namespace StudentCredit.Data.SummaryReport
{
	public class SheetYear : SheetRowData, IEntity, ISheetList<MonthData>
	{
		public int Id { get; set; }

		public int YearId { get; set; }
		public Year Year { get; set; }

		public int SheetId { get; set; }
		public Sheet Sheet { get; set; }

		public List<MonthData> SheetList { get; set; } = new List<MonthData>();

		public SheetYear() { }

		public SheetYear(int sheetYearId)
		{
			this.YearId = sheetYearId;
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
