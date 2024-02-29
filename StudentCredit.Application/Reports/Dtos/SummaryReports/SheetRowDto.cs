namespace StudentCredit.Application.Reports.Dtos.SummaryReports
{
    public class SheetRowDto
    {
        public decimal? TotalSum { get; set; }

        public decimal? RenegotiatedSum { get; set; }

        public decimal? PrincipalAbsorbed { get; set; }

        public decimal? RemainderToDigest { get; set; }

        public decimal? InterestOverPrincipal { get; set; }

        public decimal? PrincipalPaid { get; set; }

        public decimal? InterestPaid { get; set; }

        public decimal? DebtWrittenOff { get; set; }

        public decimal? SimpleDebtPrincipal { get; set; }

        public decimal? SimpleDebtInterest { get; set; }

        public decimal? WarrantiesActivatedPrincipal { get; set; }

        public decimal? WarrantiesActivatedInterest { get; set; }

        public int? CreditCount { get; set; }
    }
}
