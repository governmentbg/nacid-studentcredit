using StudentCredit.Application.Reports.Enums;

namespace StudentCredit.Application.Reports.Dtos.Reports
{
    public class ReportSearchResultItemDto
    {
        public ReportType ReportType { get; set; }
        //When ReportType == ReportType.NewContract
        public NewContractFilterType NewContractFilterType { get; set; }

        //NewContractFilterType == StudentAndDoctoral
        public bool IsStudents { get; set; }
        public bool IsDoctorals { get; set; }

        //ReportType == Deimbursed
        public bool IsSelfRepaid { get; set; }
        public bool IsStateRepaid { get; set; }

        //NewContractFilterType
        public string BankName { get; set; }
        public string InstitutionName { get; set; }
        public string ResearchAreaName { get; set; }
        public string NationalityName { get; set; }
        public int AgeAtContract { get; set; }
        public int Year { get; set; }

        public int EducationTaxesContractCount { get; set; }
        public decimal? EducationTaxesSize { get; set; }

        public int MaintenanceContractCount { get; set; }
        public decimal? MaintenanceSize { get; set; }


        public int EducationTaxesContractRefusedCount { get; set; }
        public int MaintenanceContractRefusedCount { get; set; }


        public int SelfRepaidCount { get; set; }
        public decimal? SelfRepaidSize { get; set; }

        public int DeathCount { get; set; }
        public decimal? DeathSize { get; set; }

        public int BirthOrFullAdoptationCount { get; set; }
        public decimal? BirthOrFullAdoptationSize { get; set; }

        public int PermanentIncapacityCount { get; set; }
        public decimal? PermanentIncapacitySize { get; set; }

        public int BankClaimCount { get; set; }
        public decimal? BankClaimSize { get; set; }

        public int EarlyDellabilityCount { get; set; }
        public decimal? EarlyDellabilitySize { get; set; }

        public int RenegotiationCount { get; set; }
        public decimal? RenegotiationSize { get; set; }

        public int StateCount { get; set; }
        public decimal? StateSize { get; set; }

        public int PrivateCount { get; set; }
        public decimal? PrivateSize { get; set; }

        public int CurrentTotalCount { get; set; }
        public decimal? CurrentTotalSize { get; set; }

        // For bank report
        public int CurrentTotalRefusedCount { get; set; }
    }
}