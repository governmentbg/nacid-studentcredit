namespace StudentCredit.Application.Applications.ApplicationsFour.Dtos
{
    public class ApplicationFourPdfExportDto : ApplicationFourDto
    {
        public string BlankDateString { get; set; }
        public string ContractDateString { get; set; }
        public string CreditTypeString { get; set; }

        public string SpecialityName { get; set; }

        // For Files
        public string DebtStatementDocumentFileName { get; set; }
        public string CreditContractAndAnnexesFileName { get; set; }
        public string DocumentInFavorOfTheBankFileName { get; set; }
        public string DocumentPartialRepaymentFileName { get; set; }
        public string DeathCertificateFileName { get; set; }
        public string PermanentIncapacityFileName { get; set; }
        public string BirthCertificatesOrCourtDecisionsForAdoptionsFileName { get; set; }
    }
}