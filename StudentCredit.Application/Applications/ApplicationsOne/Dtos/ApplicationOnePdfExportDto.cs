namespace StudentCredit.Application.Applications.ApplicationsOne.Dtos
{
    public class ApplicationOnePdfExportDto : ApplicationOneDto
    {
        public string BlankDateString { get; set; }
        public string ContractDateString { get; set; }
        public string CreditTypeString { get; set; }

        public string SpecialityName { get; set; }
    }
}
