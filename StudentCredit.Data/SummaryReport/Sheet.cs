using StudentCredit.Data.Banks;
using StudentCredit.Data.Common.Interfaces;
using StudentCredit.Data.Nomenclatures;

namespace StudentCredit.Data.SummaryReport
{
	public class Sheet : SheetRowData, IEntity, ISheetList<SheetYear>
	{
		public int Id { get; set; }

		public int YearId { get; set; }
		public Year Year { get; set; }

		public int BankId { get; set; }
		public Bank Bank { get; set; }

		// Изпълнение на лимита
		public decimal? FulfillmentOfTheLimit { get; set; }

		public List<SheetYear> SheetList { get; set; } = new List<SheetYear>();

		public Sheet() { }

		public Sheet(int yearId, int bankId)
		{
			this.YearId = yearId;
			this.BankId = bankId;

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
			this.Limit = 0;
			this.CreditCount = 0;
		}
	}
}