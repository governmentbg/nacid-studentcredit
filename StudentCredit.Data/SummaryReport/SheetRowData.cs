namespace StudentCredit.Data.SummaryReport
{
	public class SheetRowData
	{
		// Договорен размер на кредитите
		public decimal? TotalSum { get; set; }

		// Предоговорени (+) увеличение (-) намаление
		public decimal? RenegotiatedSum { get; set; }

		// Усвоени за такси за обучение или за издръжка средства (главници)
		public decimal? PrincipalAbsorbed { get; set; }

		// Остатък за усвояване
		public decimal? RemainderToDigest { get; set; }

		// Начислена по време на гратисния период лихва върху усвоената част от кредита, която се капитализира годишно
		public decimal? InterestOverPrincipal { get; set; }

		// Извършени плащания - Главници
		public decimal? PrincipalPaid { get; set; }

		// Извършени плащания - Лихви
		public decimal? InterestPaid { get; set; }

		// Отписан дълг - главници
		public decimal? DebtWrittenOff { get; set; }

		// Опростен дълг - Главници
		public decimal? SimpleDebtPrincipal { get; set;}

		// Опростен дълг - Лихви
		public decimal? SimpleDebtInterest { get; set; }

		// Активирани гаранции - Главници
		public decimal? WarrantiesActivatedPrincipal { get; set; }

		// Активирани гаранции - Лихви
		public decimal? WarrantiesActivatedInterest { get; set; }

		// Лимит
		public decimal? Limit { get; set; }

		// Брой кредити
		public int? CreditCount { get; set; }
	}
}
