using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using StudentCredit.Application.Common.Constants;
using StudentCredit.Application.Common.Extensions;
using StudentCredit.Application.DomainValidation.Enums;
using StudentCredit.Application.Reports.Dtos.Reports;
using StudentCredit.Application.Reports.Enums;
using StudentCredit.Application.Reports.Interfaces;
using StudentCredit.Data.Applications.ApplicationFour;
using StudentCredit.Data.Applications.ApplicationOne;
using StudentCredit.Data.Applications.ApplicationOne.Enums;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Common.Enums;
using StudentCredit.Data.Nomenclatures.Constants;
using StudentCredit.Data.Nomenclatures.Enums;
using StudentCredit.Infrastructure.DomainValidation;
using StudentCredit.Infrastructure.DomainValidation.Enums;
using StudentCredit.Infrastructure.Helpers.Extensions;
using StudentCredit.Infrastructure.Interfaces.Contexts;

namespace StudentCredit.Application.Reports.Services
{
    public class ReportService : IReportService
    {
        private readonly IAppDbContext context;
        private readonly DomainValidationService validation;

        public ReportService(
            IAppDbContext context,
            DomainValidationService validation
            )
        {
            this.context = context;
            this.validation = validation;
        }

        public async Task<SearchResultItemReportDto<ReportSearchResultItemDto>> GetReportSearchResultItem(SearchReportFilter filter)
        {
            var query = context.Set<ApplicationOne>()
                .Where(e => e.CommitState == CommitState.Approved)
                .Include(e => e.EducationalQualification)
                .Include(e => e.Bank)
                .Include(e => e.Institution)
                .Include(e => e.ResearchArea)
                .AsQueryable();

            if (!filter.StartDate.HasValue)
            {
                filter.StartDate = new DateTime(2010, 1, 1);
            }

            if (!filter.EndDate.HasValue)
            {
                filter.EndDate = DateTime.Now;
            }

            if (filter.StartDate > filter.EndDate)
            {
                validation.ThrowErrorMessage(ReportErrorCode.StartDateCannotBeGreaterThanEndDate);
            }

            var result = new SearchResultItemReportDto<ReportSearchResultItemDto>();

            int totalCreditCount = 0;
            decimal? totalCreditSize = 0.0m;
            int totalCreditsRefusedCount = 0;

            var groupedReports = new List<ReportSearchResultItemDto>();

            if (filter.ReportType == ReportType.NewContract)
            {
                var filteredApps = query
                        .Where(s => s.ApplicationOneType.Alias == ApplicationOneTypeAlias.NEW_CONTRACT
                        && s.ContractDate >= filter.StartDate
                        && s.ContractDate <= filter.EndDate)
                        .AsQueryable();

                if (filter.NewContractFilterType == NewContractFilterType.StudentAndDoctoral)
                {
                    var studentApps = filteredApps
                        .Where(s => s.EducationalQualification.Alias != "doctor")
                        .AsQueryable();

                    var doctoralApps = filteredApps
                        .Where(s => s.EducationalQualification.Alias == "doctor")
                        .AsQueryable();

                    if (studentApps.Any())
                    {
                        var studentReport = new ReportSearchResultItemDto
                        {
                            ReportType = ReportType.NewContract,
                            NewContractFilterType = NewContractFilterType.StudentAndDoctoral,
                            IsStudents = true,
                            EducationTaxesContractCount = studentApps.Where(s => s.CreditType == CreditType.EducationTaxes).Count(),
                            MaintenanceContractCount = studentApps.Where(s => s.CreditType == CreditType.Maintenance).Count(),
                            EducationTaxesSize = studentApps.Where(s => s.CreditType == CreditType.EducationTaxes).Sum(s => s.CreditSize),
                            MaintenanceSize = studentApps.Where(s => s.CreditType == CreditType.Maintenance).Sum(s => s.CreditSize)
                        };

                        groupedReports.Add(studentReport);
                    }

                    if (doctoralApps.Any())
                    {
                        var doctoralReport = new ReportSearchResultItemDto
                        {
                            ReportType = ReportType.NewContract,
                            NewContractFilterType = NewContractFilterType.StudentAndDoctoral,
                            IsDoctorals = true,
                            EducationTaxesContractCount = doctoralApps.Where(s => s.CreditType == CreditType.EducationTaxes).Count(),
                            MaintenanceContractCount = doctoralApps.Where(s => s.CreditType == CreditType.Maintenance).Count(),
                            EducationTaxesSize = doctoralApps.Where(s => s.CreditType == CreditType.EducationTaxes).Sum(s => s.CreditSize),
                            MaintenanceSize = doctoralApps.Where(s => s.CreditType == CreditType.Maintenance).Sum(s => s.CreditSize)
                        };

                        groupedReports.Add(doctoralReport);
                    }

                    groupedReports.ToList();

                    groupedReports.ForEach(s =>
                    {
                        s.CurrentTotalCount = GetSumCount(SumCountType.ContractCount, s);
                        s.CurrentTotalSize = GetSumSize(SumSizeType.SizeValue, s);
                    });

                    totalCreditCount = GetSumCount(SumCountType.ContractCount, groupedReports);
                    totalCreditSize = GetSumSize(SumSizeType.SizeValue, groupedReports);
                }

                else if (filter.NewContractFilterType == NewContractFilterType.Bank)
                {
                    var refusedApps = query
                        .Where(s => s.ApplicationOneType.Alias == ApplicationOneTypeAlias.REFUSE_CONTRACT
                        && s.ContractDate >= filter.StartDate
                        && s.ContractDate <= filter.EndDate)
                        .AsQueryable();

                    var bankIds = filteredApps.Select(s => s.BankId).ToList();

                    //Get apps from banks that only have refused contracts
                    var onlyRefused = refusedApps.Where(app => !bankIds.Any(id => app.BankId == id));

                    var bankReports = filteredApps
                        .GroupBy(e => e.Bank)
                        .Select(c => new ReportSearchResultItemDto
                        {
                            ReportType = ReportType.NewContract,
                            NewContractFilterType = NewContractFilterType.Bank,
                            BankName = c.First().Bank.Name,
                            EducationTaxesContractCount = c.Where(s => s.CreditType == CreditType.EducationTaxes).Count(),
                            MaintenanceContractCount = c.Where(s => s.CreditType == CreditType.Maintenance).Count(),
                            EducationTaxesSize = c.Where(s => s.CreditType == CreditType.EducationTaxes).Sum(s => s.CreditSize),
                            MaintenanceSize = c.Where(s => s.CreditType == CreditType.Maintenance).Sum(s => s.CreditSize),
                            EducationTaxesContractRefusedCount = refusedApps.Where(s => s.BankId == c.First().BankId && s.CreditType == CreditType.EducationTaxes).Count(),
                            MaintenanceContractRefusedCount = refusedApps.Where(s => s.BankId == c.First().BankId && s.CreditType == CreditType.Maintenance).Count(),
                        })
                        .ToList();

                    groupedReports.AddRange(bankReports); 

                    if (onlyRefused.Any())
                    {
                        var onlyRefusedReports = onlyRefused
                           .GroupBy(e => e.Bank)
                           .Select(c => new ReportSearchResultItemDto
                           {
                               ReportType = ReportType.NewContract,
                               NewContractFilterType = NewContractFilterType.Bank,
                               BankName = c.First().Bank.Name,
                               EducationTaxesContractCount = 0,
                               MaintenanceContractCount = 0,
                               EducationTaxesSize = 0,
                               MaintenanceSize = 0,
                               EducationTaxesContractRefusedCount = onlyRefused.Where(s => s.CreditType == CreditType.EducationTaxes).Count(),
                               MaintenanceContractRefusedCount = onlyRefused.Where(s => s.CreditType == CreditType.Maintenance).Count()
                           })
                           .ToList();

                        groupedReports.AddRange(onlyRefusedReports);
                    }

                    groupedReports.ForEach(s =>
                    {
                        s.CurrentTotalCount = GetSumCount(SumCountType.ContractCount, s);
                        s.CurrentTotalSize = GetSumSize(SumSizeType.SizeValue, s);
                        s.CurrentTotalRefusedCount = GetSumCount(SumCountType.RefusedCount, s);
                    });

                    totalCreditCount = GetSumCount(SumCountType.ContractCount, groupedReports);
                    totalCreditSize = GetSumSize(SumSizeType.SizeValue, groupedReports);
                    totalCreditsRefusedCount = GetSumCount(SumCountType.RefusedCount, groupedReports);
                }

                else if (filter.NewContractFilterType == NewContractFilterType.Institution)
                {
                    if (filter.InstitutionOwnershipType == InstitutionOwnershipType.State)
                    {
                        filteredApps = filteredApps.Where(s => s.Institution.InstitutionOwnershipType == InstitutionOwnershipType.State);
                    }

                    if (filter.InstitutionOwnershipType == InstitutionOwnershipType.Private)
                    {
                        filteredApps = filteredApps.Where(s => s.Institution.InstitutionOwnershipType == InstitutionOwnershipType.Private);
                    }

                    groupedReports = filteredApps
                        .GroupBy(e => e.Institution)
                        .Select(c => new ReportSearchResultItemDto
                        {
                            ReportType = ReportType.NewContract,
                            NewContractFilterType = NewContractFilterType.Institution,
                            InstitutionName = c.First().Institution.Name,
                            EducationTaxesContractCount = c.Where(s => s.CreditType == CreditType.EducationTaxes).Count(),
                            MaintenanceContractCount = c.Where(s => s.CreditType == CreditType.Maintenance).Count(),
                            EducationTaxesSize = c.Where(s => s.CreditType == CreditType.EducationTaxes).Sum(s => s.CreditSize),
                            MaintenanceSize = c.Where(s => s.CreditType == CreditType.Maintenance).Sum(s => s.CreditSize)
                        })
                        .OrderBy(e => !string.IsNullOrEmpty(e.InstitutionName))
                        .ThenBy(e => e.InstitutionName)
                        .ToList();

                    groupedReports.ForEach(s =>
                    {
                        s.CurrentTotalCount = GetSumCount(SumCountType.ContractCount, s);
                        s.CurrentTotalSize = GetSumSize(SumSizeType.SizeValue, s);
                    });

                    totalCreditCount = GetSumCount(SumCountType.ContractCount, groupedReports);
                    totalCreditSize = GetSumSize(SumSizeType.SizeValue, groupedReports);
                }

                else if (filter.NewContractFilterType == NewContractFilterType.InstitutionTypeByYear)
                {
                    groupedReports = filteredApps
                      .GroupBy(e => e.ContractDate.Value.Year)
                      .Select(c => new ReportSearchResultItemDto
                      {
                          ReportType = ReportType.NewContract,
                          NewContractFilterType = NewContractFilterType.InstitutionTypeByYear,
                          Year = c.First().ContractDate.Value.Year,
                          StateCount = c.Where(s => s.Institution.InstitutionOwnershipType == InstitutionOwnershipType.State).Count(),
                          PrivateCount = c.Where(s => s.Institution.InstitutionOwnershipType == InstitutionOwnershipType.Private).Count(),
                          StateSize = c.Where(s => s.Institution.InstitutionOwnershipType == InstitutionOwnershipType.State).Sum(s => s.CreditSize),
                          PrivateSize = c.Where(s => s.Institution.InstitutionOwnershipType == InstitutionOwnershipType.Private).Sum(s => s.CreditSize)
                      })
                      .OrderBy(e => e.Year)
                      .ToList();

                    groupedReports.ForEach(s =>
                    {
                        s.CurrentTotalCount = GetSumCount(SumCountType.ContractByYearCount, s);
                        s.CurrentTotalSize = GetSumSize(SumSizeType.SizeByYearValue, s);
                    });

                    totalCreditCount = GetSumCount(SumCountType.ContractByYearCount, groupedReports);
                    totalCreditSize = GetSumSize(SumSizeType.SizeByYearValue, groupedReports);
                }

                else if (filter.NewContractFilterType == NewContractFilterType.ResearchArea)
                {
                    if (filter.LearnerType == LearnerType.Student)
                    {
                        filteredApps = filteredApps.Where(s => s.EducationalQualification.Alias != "doctor");
                    }

                    else if (filter.LearnerType == LearnerType.Doctoral)
                    {
                        filteredApps = filteredApps.Where(s => s.EducationalQualification.Alias == "doctor");
                    }

                    groupedReports = filteredApps
                       .GroupBy(e => e.ResearchArea)
                       .Select(c => new ReportSearchResultItemDto
                       {
                           ReportType = ReportType.NewContract,
                           NewContractFilterType = NewContractFilterType.ResearchArea,
                           ResearchAreaName = c.First().ResearchArea.Name,
                           EducationTaxesContractCount = c.Where(s => s.CreditType == CreditType.EducationTaxes).Count(),
                           MaintenanceContractCount = c.Where(s => s.CreditType == CreditType.Maintenance).Count(),
                           EducationTaxesSize = c.Where(s => s.CreditType == CreditType.EducationTaxes).Sum(s => s.CreditSize),
                           MaintenanceSize = c.Where(s => s.CreditType == CreditType.Maintenance).Sum(s => s.CreditSize)
                       })
                       .OrderBy(e => !string.IsNullOrEmpty(e.ResearchAreaName))
                       .ThenBy(e => e.ResearchAreaName)
                       .ToList();

                    groupedReports.ForEach(s =>
                    {
                        s.CurrentTotalCount = GetSumCount(SumCountType.ContractCount, s);
                        s.CurrentTotalSize = GetSumSize(SumSizeType.SizeValue, s);
                    });

                    totalCreditCount = GetSumCount(SumCountType.ContractCount, groupedReports);
                    totalCreditSize = GetSumSize(SumSizeType.SizeValue, groupedReports);
                }

                else if (filter.NewContractFilterType == NewContractFilterType.Nationality)
                {
                    if (filter.LearnerType == LearnerType.Student)
                    {
                        filteredApps = filteredApps.Where(s => s.EducationalQualification.Alias != "doctor");
                    }

                    else if (filter.LearnerType == LearnerType.Doctoral)
                    {
                        filteredApps = filteredApps.Where(s => s.EducationalQualification.Alias == "doctor");
                    }

                    groupedReports = filteredApps
                       .GroupBy(e => e.Nationality)
                       .Select(c => new ReportSearchResultItemDto
                       {
                           ReportType = ReportType.NewContract,
                           NewContractFilterType = NewContractFilterType.Nationality,
                           NationalityName = c.First().Nationality.Name,
                           EducationTaxesContractCount = c.Where(s => s.CreditType == CreditType.EducationTaxes).Count(),
                           MaintenanceContractCount = c.Where(s => s.CreditType == CreditType.Maintenance).Count(),
                           EducationTaxesSize = c.Where(s => s.CreditType == CreditType.EducationTaxes).Sum(s => s.CreditSize),
                           MaintenanceSize = c.Where(s => s.CreditType == CreditType.Maintenance).Sum(s => s.CreditSize)
                       })
                       .OrderBy(e => !string.IsNullOrEmpty(e.NationalityName))
                       .ThenBy(e => e.NationalityName)
                       .ToList();

                    groupedReports.ForEach(s =>
                    {
                        s.CurrentTotalCount = GetSumCount(SumCountType.ContractCount, s);
                        s.CurrentTotalSize = GetSumSize(SumSizeType.SizeValue, s);
                    });

                    totalCreditCount = GetSumCount(SumCountType.ContractCount, groupedReports);
                    totalCreditSize = GetSumSize(SumSizeType.SizeValue, groupedReports);
                }

                else if (filter.NewContractFilterType == NewContractFilterType.Age)
                {
                    //Get only bulgarian students/doctorals
                    filteredApps = filteredApps.Where(s => s.Nationality.Code == CountryCodes.BULGARIA);

                    if (filter.LearnerType == LearnerType.Student)
                    {
                        filteredApps = filteredApps.Where(s => s.EducationalQualification.Alias != "doctor");
                    }

                    else if (filter.LearnerType == LearnerType.Doctoral)
                    {
                        filteredApps = filteredApps.Where(s => s.EducationalQualification.Alias == "doctor");
                    }

                    var appsWithoutAge = filteredApps
                      .Where(e => e.AgeAtContract == 0 || e.AgeAtContract == null)
                      .GroupBy(e => e.AgeAtContract ?? 0)
                      .Select(c => new ReportSearchResultItemDto
                      {
                          ReportType = ReportType.NewContract,
                          NewContractFilterType = NewContractFilterType.Age,
                          AgeAtContract = 0,
                          EducationTaxesContractCount = c.Where(s => s.CreditType == CreditType.EducationTaxes).Count(),
                          MaintenanceContractCount = c.Where(s => s.CreditType == CreditType.Maintenance).Count(),
                          EducationTaxesSize = c.Where(s => s.CreditType == CreditType.EducationTaxes).Sum(s => s.CreditSize),
                          MaintenanceSize = c.Where(s => s.CreditType == CreditType.Maintenance).Sum(s => s.CreditSize)
                      })
                      .ToList();

                    groupedReports.AddRange(appsWithoutAge);

                    var appsWithAge = filteredApps
                       .Where(e => e.AgeAtContract != 0 && e.AgeAtContract != null)
                       .GroupBy(e => e.AgeAtContract)
                       .Select(c => new ReportSearchResultItemDto
                       {
                           ReportType = ReportType.NewContract,
                           NewContractFilterType = NewContractFilterType.Age,
                           AgeAtContract = c.First().AgeAtContract.Value,
                           EducationTaxesContractCount = c.Where(s => s.CreditType == CreditType.EducationTaxes).Count(),
                           MaintenanceContractCount = c.Where(s => s.CreditType == CreditType.Maintenance).Count(),
                           EducationTaxesSize = c.Where(s => s.CreditType == CreditType.EducationTaxes).Sum(s => s.CreditSize),
                           MaintenanceSize = c.Where(s => s.CreditType == CreditType.Maintenance).Sum(s => s.CreditSize)
                       })
                       .OrderBy(e => e.AgeAtContract)
                       .ToList();

                    groupedReports.AddRange(appsWithAge);

                    groupedReports.ForEach(s =>
                    {
                        s.CurrentTotalCount = GetSumCount(SumCountType.ContractCount, s);
                        s.CurrentTotalSize = GetSumSize(SumSizeType.SizeValue, s);
                    });

                    totalCreditCount = GetSumCount(SumCountType.ContractCount, groupedReports);
                    totalCreditSize = GetSumSize(SumSizeType.SizeValue, groupedReports);
                }
            }

            else if (filter.ReportType == ReportType.Deimbursed)
            {
                //Check if either PaymentDate or EarlyPaymentDate is between filter dates boundaries
                var filteredApps = query
                       .Where(s => s.ApplicationOneType.Alias == ApplicationOneTypeAlias.REPAYMENT_CREDIT
                            && (s.PaymentDate >= filter.StartDate && s.PaymentDate <= filter.EndDate
                            || s.EarlyPaymentDate >= filter.StartDate && s.EarlyPaymentDate <= filter.EndDate))
                       .AsQueryable();

                var selfRepaids = filteredApps
                    .Where(s => s.ApplicationOneStatus == ApplicationOneStatus.RepaidByBorrower)
                    .AsQueryable();

                var stateRepaids = context.Set<ApplicationFour>()
                    .Include(s=> s.ApplicationOne)
                    .Where(e => e.ApplicationOne.PaidByApplicationFour.Value
                        && e.ApplicationOne.PaidByApplicationFourDate >= filter.StartDate 
                        && e.ApplicationOne.PaidByApplicationFourDate <= filter.EndDate
                        && e.CommitState == CommitState.Approved)
                    .AsQueryable();

                if (selfRepaids.Any())
                {
                    var selfRepaidReport = new ReportSearchResultItemDto
                    {
                        ReportType = ReportType.Deimbursed,
                        IsSelfRepaid = true,
                        SelfRepaidCount = selfRepaids.Count(),
                        SelfRepaidSize = selfRepaids.Sum(s => s.CreditSize)
                    };

                    groupedReports.Add(selfRepaidReport);
                }

                if (stateRepaids.Any())
                {
                    var stateRepaidReport = new ReportSearchResultItemDto
                    {
                        ReportType = ReportType.Deimbursed,
                        IsStateRepaid = true,
                        DeathCount = stateRepaids.Where(s => s.ApplicationFourType.Alias == ApplicationFourTypeAlias.DEATH).Count(),
                        DeathSize = stateRepaids.Where(s => s.ApplicationFourType.Alias == ApplicationFourTypeAlias.DEATH).Sum(s => s.OutstandingDebtAmount),
                        BirthOrFullAdoptationCount = stateRepaids.Where(s => s.ApplicationFourType.Alias == ApplicationFourTypeAlias.BIRTH_OR_FULL_ADOPTATION).Count(),
                        BirthOrFullAdoptationSize = stateRepaids.Where(s => s.ApplicationFourType.Alias == ApplicationFourTypeAlias.BIRTH_OR_FULL_ADOPTATION).Sum(s => s.OutstandingDebtAmount),
                        PermanentIncapacityCount = stateRepaids.Where(s => s.ApplicationFourType.Alias == ApplicationFourTypeAlias.PERMANENT_INCAPACITY).Count(),
                        PermanentIncapacitySize = stateRepaids.Where(s => s.ApplicationFourType.Alias == ApplicationFourTypeAlias.PERMANENT_INCAPACITY).Sum(s => s.OutstandingDebtAmount),
                        BankClaimCount = stateRepaids.Where(s => s.ApplicationFourType.Alias == ApplicationFourTypeAlias.BANK_CLAIM).Count(),
                        BankClaimSize = stateRepaids.Where(s => s.ApplicationFourType.Alias == ApplicationFourTypeAlias.BANK_CLAIM).Sum(s => s.OutstandingDebtAmount),
                    };

                    groupedReports.Add(stateRepaidReport);
                }

                groupedReports.ToList();

                totalCreditCount = GetSumCount(SumCountType.DeimbursedCount, groupedReports);
                totalCreditSize = GetSumSize(SumSizeType.DeimbursedValue, groupedReports);
            }

            else if (filter.ReportType == ReportType.EarlyDellability)
            {
                var filteredApps = query
                       .Where(s => s.ApplicationOneType.Alias == ApplicationOneTypeAlias.EARLY_DELLABILITY
                        && s.EarlyDemandOfTheLoan >= filter.StartDate
                        && s.EarlyDemandOfTheLoan <= filter.EndDate)
                       .AsQueryable();

                if (filteredApps.Any())
                {
                    var earlyDellabilityReport = new ReportSearchResultItemDto
                    {
                        ReportType = ReportType.EarlyDellability,
                        EarlyDellabilityCount = filteredApps.Count(),
                        EarlyDellabilitySize = filteredApps.Sum(s => s.CreditSize)
                    };

                    groupedReports.Add(earlyDellabilityReport);
                }

                groupedReports.ToList();
            }

            else if (filter.ReportType == ReportType.Renegotiation)
            {
                var filteredApps = query
                       .Where(s => s.ApplicationOneType.Alias == ApplicationOneTypeAlias.RENEGOTIATION
                        && s.RecontractionDate >= filter.StartDate
                        && s.RecontractionDate <= filter.EndDate)
                       .AsQueryable();

                if (filteredApps.Any())
                {
                    var renegotiationReport = new ReportSearchResultItemDto
                    {
                        ReportType = ReportType.Renegotiation,
                        RenegotiationCount = filteredApps.Count(),
                        RenegotiationSize = filteredApps.Sum(s => s.CreditSize)
                    };

                    groupedReports.Add(renegotiationReport);
                }

                groupedReports.ToList();
            }

            result.Items = groupedReports;
            result.TotalCount = groupedReports.Count;
            result.TotalCreditsCount = totalCreditCount;
            result.TotalCreditsSize = totalCreditSize;
            result.TotalCreditsRefusedCount = totalCreditsRefusedCount;

            return result;
        }


        public MemoryStream ExportReportsExcel(List<ReportSearchResultItemDto> items, SearchReportFilter filter)
        {
            var unknown = "Неизвестно";
            var currency = " лв.";

            filter.Offset = 0;

            try
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    var filterHeader = "Филтриране спрямо";
                    var additionalHeader = "";
                    var additionalHeaderColumnValue = "";

                    var firstTableHeader = "";
                    var secondTableHeader = "";
                    var bankTableRefusedHeader = "Брой откази";

                    switch (filter.NewContractFilterType)
                    {
                        case NewContractFilterType.StudentAndDoctoral:
                            firstTableHeader = "Кредитополучатели";
                            secondTableHeader = "Вид на кредит";
                            break;
                        case NewContractFilterType.Bank:
                            firstTableHeader = "Банка";
                            secondTableHeader = "Вид на кредит";
                            break;
                        case NewContractFilterType.Institution:
                            additionalHeader = "Вид на ВУ";
                            additionalHeaderColumnValue = filter.InstitutionOwnershipType?.GetEnumDescription() ?? "Всички";
                            firstTableHeader = "ВУ/НО";
                            secondTableHeader = "Вид на кредит";
                            break;
                        case NewContractFilterType.InstitutionTypeByYear:
                            firstTableHeader = "Година";
                            secondTableHeader = "Вид на ВУ/НО";
                            break;
                        case NewContractFilterType.ResearchArea:
                            additionalHeader = "Вид на обучаеми";
                            additionalHeaderColumnValue = filter.LearnerType?.GetEnumDescription() ?? "Всички";
                            firstTableHeader = "ПН";
                            secondTableHeader = "Вид на кредит";
                            break;
                        case NewContractFilterType.Nationality:
                            additionalHeader = "Вид на обучаеми";
                            additionalHeaderColumnValue = filter.LearnerType?.GetEnumDescription() ?? "Всички";
                            firstTableHeader = "Гражданство";
                            secondTableHeader = "Вид на кредит";
                            break;
                        case NewContractFilterType.Age:
                            additionalHeader = "Вид на обучаеми";
                            additionalHeaderColumnValue = filter.LearnerType?.GetEnumDescription() ?? "Всички";
                            firstTableHeader = "Възраст (само български граждани)";
                            secondTableHeader = "Вид на кредит";
                            break;
                        default:
                            break;
                    };

                    var filterHeaders = new List<string>()
                    {
                        "Тип на справката",
                        "Начало на период",
                        "Край на период"
                    };

                    if (filter.ReportType == ReportType.NewContract)
                    {
                        filterHeaders.Add(filterHeader);
                        filterHeaders.Add(additionalHeader);
                    }

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Справки");

                    //5 = Max number between [filter fields count, table headers count]
                    bool[] isFormatedMaxCols = new bool[5];

                    int row = 1, col = 1, range = 4;

                    foreach (var header in filterHeaders)
                    {
                        worksheet.Cells[row, col].Value = header;
                        SetCellStyle(worksheet, row, col, true, null, null);
                        col++;
                    }

                    row++;

                    worksheet.Cells[row, 1].Value = filter.ReportType.GetEnumDescription();
                    worksheet.Cells[row, 2].Value = filter.StartDate?.ToString("dd-MM-yyyy");
                    worksheet.Cells[row, 3].Value = filter.EndDate?.ToString("dd-MM-yyyy");
                    worksheet.Cells[row, 4].Value = filter.NewContractFilterType?.GetEnumDescription();
                    worksheet.Cells[row, 5].Value = additionalHeaderColumnValue;

                    row += 2;
                    col = 1;


                    if (filter.ReportType == ReportType.NewContract)
                    {
                        var tableHeaders = new List<string>()
                        {
                            firstTableHeader,
                            secondTableHeader,
                            "Брой на сключени договори",
                            "Размер на кредитите"
                        };

                        if (filter.NewContractFilterType == NewContractFilterType.Bank)
                        {
                            tableHeaders.Add(bankTableRefusedHeader);
                        }

                        foreach (var header in tableHeaders)
                        {
                            worksheet.Cells[row, col].Value = header;
                            SetCellStyle(worksheet, row, col, true, null, null);
                            col++;
                        }

                        row++;
                        col = 1;

                        foreach (var item in items)
                        {
                            var description = "";

                            switch (filter.NewContractFilterType)
                            {
                                case NewContractFilterType.StudentAndDoctoral:
                                    if (item.IsStudents)
                                    {
                                        description = "Студенти";
                                    }

                                    if (item.IsDoctorals)
                                    {
                                        description = "Докторанти";
                                    }

                                    ExcelExportFillNewContractRow(worksheet, description, row, col, "За обучение", "За издръжка", null,
                                            item.EducationTaxesContractCount, item.EducationTaxesSize,
                                            item.MaintenanceContractCount, item.MaintenanceSize,
                                            null, null);
                                    break;

                                case NewContractFilterType.Bank:
                                    description = item.BankName;
                                    ExcelExportFillNewContractRow(worksheet, description, row, col, "За обучение", "За издръжка", "Брой откази",
                                            item.EducationTaxesContractCount, item.EducationTaxesSize,
                                            item.MaintenanceContractCount, item.MaintenanceSize,
                                            item.EducationTaxesContractRefusedCount, item.MaintenanceContractRefusedCount);

                                    range = 5;
                                    break;

                                case NewContractFilterType.Institution:
                                    description = item.InstitutionName != null ? item.InstitutionName : unknown;
                                    ExcelExportFillNewContractRow(worksheet, description, row, col, "За обучение", "За издръжка", null,
                                            item.EducationTaxesContractCount, item.EducationTaxesSize,
                                            item.MaintenanceContractCount, item.MaintenanceSize,
                                            null, null);
                                    break;

                                case NewContractFilterType.InstitutionTypeByYear:
                                    description = item.Year.ToString();
                                    ExcelExportFillNewContractRow(worksheet, description, row, col, "Държавни", "Частни", null,
                                            item.StateCount, item.StateSize,
                                            item.PrivateCount, item.PrivateSize,
                                            null, null);
                                    break;

                                case NewContractFilterType.ResearchArea:
                                    description = item.ResearchAreaName != null ? item.ResearchAreaName : unknown;
                                    ExcelExportFillNewContractRow(worksheet, description, row, col, "За обучение", "За издръжка", null,
                                            item.EducationTaxesContractCount, item.EducationTaxesSize,
                                            item.MaintenanceContractCount, item.MaintenanceSize,
                                            null, null);
                                    break;

                                case NewContractFilterType.Nationality:
                                    description = item.NationalityName != null ? item.NationalityName : unknown;
                                    ExcelExportFillNewContractRow(worksheet, description, row, col, "За обучение", "За издръжка", null,
                                            item.EducationTaxesContractCount, item.EducationTaxesSize,
                                            item.MaintenanceContractCount, item.MaintenanceSize,
                                            null, null);
                                    break;

                                case NewContractFilterType.Age:
                                    description = (item.AgeAtContract != 0) ? item.AgeAtContract.ToString() +"-годишни" : unknown;
                                    ExcelExportFillNewContractRow(worksheet, description, row, col, "За обучение", "За издръжка", null,
                                            item.EducationTaxesContractCount, item.EducationTaxesSize,
                                            item.MaintenanceContractCount, item.MaintenanceSize,
                                            null, null);
                                    break;
                                default: 
                                    break;
                            }

                            SetCellsTopBorder(worksheet, row, range);
                            row += 3;
                        }

                        int sumValueFirst, sumValueRefused;
                        decimal? sumValueSecond;

                        if (filter.NewContractFilterType == NewContractFilterType.InstitutionTypeByYear)
                        {
                            sumValueFirst = items.Sum(s => s.StateCount + s.PrivateCount);
                            sumValueSecond = items.Sum(s => s.StateSize + s.PrivateSize);
                        }
                        else
                        {
                            sumValueFirst = items.Sum(s => s.EducationTaxesContractCount + s.MaintenanceContractCount);
                            sumValueSecond = items.Sum(s => s.EducationTaxesSize + s.MaintenanceSize);
                        }

                        col = 1;
                        worksheet.Cells[row, col, row, col + 1].Merge = true;
                        worksheet.Cells[row, col].Value = "ОБЩО ВСИЧКИ:";
                        SetCellStyle(worksheet, row, col, true, ExcelHorizontalAlignment.Right, null);

                        col += 2;
                        worksheet.Cells[row, col].Value = sumValueFirst.NumberFormatting();
                        SetCellStyle(worksheet, row, col, true, ExcelHorizontalAlignment.Right, null);


                        col++;
                        worksheet.Cells[row, col].Value = sumValueSecond.Value.RoundDecimalWithTwoPlaces() + currency;
                        SetCellStyle(worksheet, row, col, true, ExcelHorizontalAlignment.Right, null);

                        if (filter.NewContractFilterType == NewContractFilterType.Bank)
                        {
                            sumValueRefused = items.Sum(s => s.EducationTaxesContractRefusedCount + s.MaintenanceContractRefusedCount);

                            col++;
                            worksheet.Cells[row, col].Value = sumValueRefused.NumberFormatting();
                            SetCellStyle(worksheet, row, col, true, ExcelHorizontalAlignment.Right, null);
                        }

                        SetCellsTopBorder(worksheet, row, range);
                    }

                    if (filter.ReportType == ReportType.Deimbursed)
                    {
                        var tableHeaders = new List<string>()
                        {
                            "Статус на кредита",
                            "Основание",
                            "Брой",
                            "Размер на кредитите"
                        };

                        foreach (var header in tableHeaders)
                        {
                            worksheet.Cells[row, col].Value = header;
                            SetCellStyle(worksheet, row, col, true, null, null);
                            col++;
                        }

                        row++;
                        col = 1;

                        int sumCount = 0;
                        decimal? sumSize = 0;

                        foreach (var item in items)
                        {
                            if (item.IsSelfRepaid)
                            {
                                ExcelExportFillDeimbursedRow(worksheet, row, col, false, "Погасен от кредитополучателя",
                                    null, item.SelfRepaidCount, item.SelfRepaidSize,
                                    null, null, null,
                                    null, null, null,
                                    null, null, null);

                                sumCount += item.SelfRepaidCount;
                                sumSize += item.SelfRepaidSize;

                                row++;
                            }
                            else
                            {

                                ExcelExportFillDeimbursedRow(worksheet, row, col, true, "Погасен от държавата",
                                    "Поради смърт", item.DeathCount, item.DeathSize,
                                    "Раждане на второ дете", item.BirthOrFullAdoptationCount, item.BirthOrFullAdoptationSize,
                                    "Трайна неработоспособност", item.PermanentIncapacityCount, item.PermanentIncapacitySize,
                                    "Предсрочна изискуемост", item.BankClaimCount, item.BankClaimSize);

                                sumCount += item.DeathCount + item.BirthOrFullAdoptationCount + item.PermanentIncapacityCount + item.BankClaimCount;
                                sumSize += item.DeathSize + item.BirthOrFullAdoptationSize + item.PermanentIncapacitySize + item.BankClaimSize;

                                row += 4;
                            }

                            SetCellsTopBorder(worksheet, row, range);
                        }

                        col = 1;

                        worksheet.Cells[row, col, row, col + 1].Merge = true;
                        worksheet.Cells[row, col].Value = "ОБЩО ВСИЧКИ:";
                        SetCellStyle(worksheet, row, col, true, ExcelHorizontalAlignment.Right, null);

                        col += 2;
                        worksheet.Cells[row, col].Value = sumCount.NumberFormatting();
                        SetCellStyle(worksheet, row, col, true, null, null);


                        col++;
                        worksheet.Cells[row, col].Value = sumSize.Value.RoundDecimalWithTwoPlaces() + currency;
                        SetCellStyle(worksheet, row, col, true, null, null);

                        SetCellsTopBorder(worksheet, row, range);
                    }

                    if (filter.ReportType == ReportType.EarlyDellability || filter.ReportType == ReportType.Renegotiation)
                    {
                        var tableHeaders = new List<string>()
                        {
                            "Статус на кредита",
                            "Брой",
                            "Размер на кредитите"
                        };

                        foreach (var header in tableHeaders)
                        {
                            worksheet.Cells[row, col].Value = header;
                            SetCellStyle(worksheet, row, col, true, null, null);
                            col++;
                        }

                        row++;
                        col = 1;

                        foreach (var item in items)
                        {
                            if (filter.ReportType == ReportType.EarlyDellability)
                            {
                                ExcelExportFillEarlyOrRenegotiationRow(worksheet, "Предсрочно изискуем",
                                    row, col, item.EarlyDellabilityCount, item.EarlyDellabilitySize);
                            }
                            else
                            {
                                ExcelExportFillEarlyOrRenegotiationRow(worksheet, "Предговорени условия",
                                    row, col, item.RenegotiationCount, item.RenegotiationSize);
                            }
                        }
                    }


                    for (int i = 0; i <= 4; i++)
                    {
                        if (!isFormatedMaxCols[i])
                        {
                            worksheet.Column(i + 1).AutoFit();
                        }
                    }

                    var stream = new MemoryStream(package.GetAsByteArray());

                    return stream;
                }
            }
            catch (Exception)
            {
                this.validation.ThrowErrorMessage(SystemErrorCode.System_ExportProblem);

                throw;
            }
        }

        private int GetSumCount(SumCountType type, ReportSearchResultItemDto item)
        {
            var sum = 0;
            switch (type)
            {
                case SumCountType.ContractCount:
                    sum = item.EducationTaxesContractCount + item.MaintenanceContractCount;
                    break;
                case SumCountType.DeimbursedCount:
                    sum = item.SelfRepaidCount + item.DeathCount + item.BirthOrFullAdoptationCount + item.PermanentIncapacityCount + item.BankClaimCount;
                    break;
                case SumCountType.RefusedCount:
                    sum = item.EducationTaxesContractRefusedCount + item.MaintenanceContractRefusedCount;
                    break;
                case SumCountType.ContractByYearCount:
                    sum = item.StateCount + item.PrivateCount;
                    break;
                default:
                    break;
            }
            return sum;
        }
        private int GetSumCount(SumCountType type, List<ReportSearchResultItemDto> items)
        {
            var sum = 0;
            switch(type)
            {
                case SumCountType.ContractCount:
                    sum = items.Sum(s => s.EducationTaxesContractCount + s.MaintenanceContractCount);
                    break;
                case SumCountType.DeimbursedCount:
                    sum = items.Sum(s => s.SelfRepaidCount + s.DeathCount + s.BirthOrFullAdoptationCount + s.PermanentIncapacityCount + s.BankClaimCount);
                    break;
                case SumCountType.RefusedCount:
                    sum = items.Sum(s => s.EducationTaxesContractRefusedCount + s.MaintenanceContractRefusedCount);
                    break;
                case SumCountType.ContractByYearCount:
                    sum = items.Sum(s => s.StateCount + s.PrivateCount);
                    break;
                default:
                    break;
            }
            return sum;
        }

        private decimal? GetSumSize(SumSizeType type, ReportSearchResultItemDto item)
        {
            decimal? sum = 0.0m;
            switch (type)
            {
                case SumSizeType.SizeValue:
                    sum = item.EducationTaxesSize.Value + item.MaintenanceSize.Value;
                    break;
                case SumSizeType.SizeByYearValue:
                    sum = item.StateSize.Value + item.PrivateSize.Value;
                    break;
                case SumSizeType.DeimbursedValue:
                    sum = item.SelfRepaidSize ?? 0 + item.DeathSize ?? 0 + item.BirthOrFullAdoptationSize ?? 0 + item.PermanentIncapacitySize ?? 0 + item.BankClaimSize ?? 0;
                    break;
                default:
                    break;
            }
            return sum;
        }
        private decimal? GetSumSize(SumSizeType type, List<ReportSearchResultItemDto> items)
        {
            decimal? sum = 0.0m;
            switch (type)
            {
                case SumSizeType.SizeValue:
                    sum = items.Sum(s => s.EducationTaxesSize.Value + s.MaintenanceSize.Value);
                    break;
                case SumSizeType.SizeByYearValue:
                    sum = items.Sum(s => s.StateSize.Value + s.PrivateSize.Value);
                    break;
                case SumSizeType.DeimbursedValue:
                    sum = items.Sum(s => s.SelfRepaidSize ?? 0 + s.DeathSize ?? 0 + s.BirthOrFullAdoptationSize ?? 0 + s.PermanentIncapacitySize ?? 0 + s.BankClaimSize ?? 0);
                    break;
                default:
                    break;
            }
            return sum;
        }

        private void ExcelExportFillNewContractRow(ExcelWorksheet worksheet, string description,
            int row, int col,
            string type1, string type2, string type3,
            int type1Count, decimal? type1Size,
            int type2Count, decimal? type2Size,
            int? refusedEducationCount, int? refusedMaintenanceCount)
        {
            var currency = " лв.";


            worksheet.Cells[row, col, row + 2, col].Merge = true;
            worksheet.Cells[row, col].Value = description;
            SetCellStyle(worksheet, row, col, false, null, ExcelVerticalAlignment.Top);

            col++;

            worksheet.Cells[row, col].Value = type1;
            worksheet.Cells[row + 1, col].Value = type2;
            worksheet.Cells[row + 2, col].Value = "ОБЩО:";
            SetCellStyle(worksheet, row + 2, col, true, ExcelHorizontalAlignment.Right, null);

            col++;

            worksheet.Cells[row, col].Value = type1Count.NumberFormatting();
            worksheet.Cells[row + 1, col].Value = type2Count.NumberFormatting();
            worksheet.Cells[row + 2, col].Value = (type1Count + type2Count).NumberFormatting();
            SetCellStyle(worksheet, row + 2, col, true, ExcelHorizontalAlignment.Right, null);

            col++;

            worksheet.Cells[row, col].Value = type1Size.Value.RoundDecimalWithTwoPlaces() + currency;
            worksheet.Cells[row + 1, col].Value = type2Size.Value.RoundDecimalWithTwoPlaces() + currency;
            worksheet.Cells[row + 2, col].Value = (type1Size + type2Size).Value.RoundDecimalWithTwoPlaces() + currency;
            SetCellStyle(worksheet, row + 2, col, true, ExcelHorizontalAlignment.Right, null);

            if (!String.IsNullOrEmpty(type3))
            {
                col++;
                worksheet.Cells[row, col].Value = refusedEducationCount.Value.NumberFormatting();
                worksheet.Cells[row + 1, col].Value = refusedMaintenanceCount.Value.NumberFormatting();
                worksheet.Cells[row + 2, col].Value = (refusedEducationCount.Value + refusedMaintenanceCount.Value).NumberFormatting();
                SetCellStyle(worksheet, row + 2, col, true, ExcelHorizontalAlignment.Right, null);
            }
        }

        private void ExcelExportFillDeimbursedRow(ExcelWorksheet worksheet, int row, int col,
            bool isStateRepaid, string description,
            string reason1, int count1, decimal? size1,
            string reason2, int? count2, decimal? size2,
            string reason3, int? count3, decimal? size3,
            string reason4, int? count4, decimal? size4)
        {
            var currency = " лв.";

            worksheet.Cells[row, col].Value = description;
            worksheet.Cells[row, col + 1].Value = reason1;
            worksheet.Cells[row, col + 2].Value = count1.NumberFormatting();
            worksheet.Cells[row, col + 3].Value = size1.Value.RoundDecimalWithTwoPlaces() + currency;

            if (isStateRepaid)
            {
                worksheet.Cells[row, col, row + 3, col].Merge = true;
                SetCellStyle(worksheet, row, col, false, null, ExcelVerticalAlignment.Top);

                row++; col = 1;
                worksheet.Cells[row, col + 1].Value = reason2;
                worksheet.Cells[row, col + 2].Value = count2.Value.NumberFormatting();
                worksheet.Cells[row, col + 3].Value = size2.Value.RoundDecimalWithTwoPlaces() + currency;

                row++; col = 1;
                worksheet.Cells[row, col + 1].Value = reason3;
                worksheet.Cells[row, col + 2].Value = count3.Value.NumberFormatting();
                worksheet.Cells[row, col + 3].Value = size3.Value.RoundDecimalWithTwoPlaces() + currency;

                row++; col = 1;
                worksheet.Cells[row, col + 1].Value = reason4;
                worksheet.Cells[row, col + 2].Value = count4.Value.NumberFormatting();
                worksheet.Cells[row, col + 3].Value = size4.Value.RoundDecimalWithTwoPlaces() + currency;
            }
        }

        private void ExcelExportFillEarlyOrRenegotiationRow(ExcelWorksheet worksheet, string description,
            int row, int col,
            int count, decimal? size)
        {
            var currency = " лв.";

            worksheet.Cells[row,col].Value = description;
            worksheet.Cells[row, col + 1].Value = count.NumberFormatting();
            worksheet.Cells[row, col + 2].Value = size.Value.RoundDecimalWithTwoPlaces() + currency;
        }

        private void SetCellStyle(ExcelWorksheet worksheet, int row, int col, bool isBold,
            ExcelHorizontalAlignment? excelHorizontalAlignment, ExcelVerticalAlignment? excelVerticalAlignment)
        {
            if (isBold)
            {
                worksheet.Cells[row, col].Style.Font.Bold = true;
            }

            if (excelHorizontalAlignment.HasValue)
            {
                worksheet.Cells[row, col].Style.HorizontalAlignment = excelHorizontalAlignment.Value;
            }

            if (excelVerticalAlignment.HasValue)
            {
                worksheet.Cells[row, col].Style.VerticalAlignment = excelVerticalAlignment.Value;
            }
        }

        private void SetCellsTopBorder(ExcelWorksheet worksheet, int row, int range)
        {
            string endLetter = "D";
            if (range == 5)
            {
                endLetter = "E";
            }
            string modelRange = "A"+ row.ToString() + ":" + endLetter + row.ToString();
            var modelTable = worksheet.Cells[modelRange];

            modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        }
    }
}