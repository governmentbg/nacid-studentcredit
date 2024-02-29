using StudentCredit.Application.Reports.Enums;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Nomenclatures.Enums;

namespace StudentCredit.Application.Reports.Dtos.Reports
{
    public class SearchReportFilter
    {
        public ReportType ReportType { get; set; }
        //When ReportType == ReportType.NewContract
        public NewContractFilterType? NewContractFilterType { get; set; }
        public LearnerType? LearnerType { get; set; }
        public InstitutionOwnershipType? InstitutionOwnershipType { get; set; }

        public int? BankId { get; set; }
        public CreditType? CreditType { get; set; }
        public int? ContractYear { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int Limit { get; set; } = 10;
        public int Offset { get; set; } = 0;
    }
}
