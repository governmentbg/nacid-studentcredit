using AutoMapper;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Infrastructure.DomainValidation;
using StudentCredit.Infrastructure.Interfaces.Contexts;
using StudentCredit.Infrastructure.Interfaces;
using StudentCredit.Application.Applications.Common.Services;
using StudentCredit.Application.Applications.ApplicationsFive.Dtos;
using StudentCredit.Data.Applications.ApplicationFive;
using StudentCredit.Data.Common.Enums;
using StudentCredit.Application.Common.Dtos;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StudentCredit.Data.SummaryReport;
using StudentCredit.Data.Applications.ApplicationFive.Enums;
using StudentCredit.Infrastructure.DomainValidation.Enums;
using StudentCredit.Data.Applications.ApplicationFour;
using StudentCredit.Application.Students;

namespace StudentCredit.Application.Applications.ApplicationsFive
{
	public class ApplicationFiveService : ApplicationService<ApplicationFive, ApplicationFiveDto>, IApplicationFiveService
	{
		private readonly IAppDbContext context;
		private readonly DomainValidationService validation;
		private readonly IMapper mapper;

		public ApplicationFiveService(
			IAppDbContext context,
			DomainValidationService validation,
			IUserContext userContext,
			IMapper mapper,
			RoleService roleService,
			IStudentService studentService
			)
			: base(context, userContext, mapper, validation, roleService, studentService)
		{
			this.context = context;
			this.validation = validation;
			this.mapper = mapper;
		}

		public async Task<SearchResultItemDto<ApplicationFiveSearchResultItemDto>> GetFiltered(SearchApplicationFiveFilter filter)
		{
			var query = this.context.Set<ApplicationFive>()
				.Where(c => c.CommitState != CommitState.Archived)
				.AsQueryable();

			if (filter.CommitState.HasValue)
			{
				query = query.Where(c => c.CommitState == filter.CommitState);
			}

			if (filter.BankId.HasValue)
			{
				query = query.Where(c => c.BankId == filter.BankId);
			}

			if (filter.From.HasValue)
			{
				query = query.Where(c => c.BlankDate >= filter.From);
			}

			if (filter.To.HasValue)
			{
				query = query.Where(c => c.BlankDate <= filter.To);
			}

			if (filter.ApplicationFiveType.HasValue)
			{
				query = query.Where(c => c.ApplicationFiveType == filter.ApplicationFiveType);
			}

			var applications = await query
				.OrderByDescending(c => c.Id)
				.Skip(filter.Offset)
				.Take(filter.Limit)
				.ProjectTo<ApplicationFiveSearchResultItemDto>(this.mapper.ConfigurationProvider)
				.ToListAsync();

			return new SearchResultItemDto<ApplicationFiveSearchResultItemDto>
			{
				Items = applications,
				TotalCount = query.Count()
			};
		}

		public async Task<DividendReportResultDto> GenerateReport(DividendReportSearchDto dto)
		{
			var totalSum = 0m;
			decimal? amountRequestedCorrection = null;

			if (dto.ApplicationFiveType == ApplicationFiveType.TotalDebtExposure)
			{
				(totalSum, amountRequestedCorrection) = await this.GenerateTotalDebtExposureReport(dto);
			}
			else if (dto.ApplicationFiveType == ApplicationFiveType.RepaidCreditObligationsInTheRelevantYear)
			{
				totalSum = await this.GenerateRepaidCreditObligationsInTheRelevantYearReport(dto);
			}
			else if (dto.ApplicationFiveType == ApplicationFiveType.BankExpensesForTheEnforcementActions)
			{
				totalSum = this.GenerateBankExpensesForTheEnforcementActionsReport(dto);
			}

			var result = new DividendReportResultDto()
			{ 
				TotalSum = Math.Round(totalSum, 2),
				AmountRequestedCorrection = amountRequestedCorrection,
				Bank = dto.Bank,
				ApplicationFiveType = dto.ApplicationFiveType,
				Year = dto.Year
			};

			return result;
		}

		private async Task<(decimal, decimal?)> GenerateTotalDebtExposureReport(DividendReportSearchDto dto)
		{
			decimal totalSum = 0m;

			var sheets = await this.context.Set<Sheet>()
				.Where(e => e.BankId == dto.Bank.Id)
				.Include(e => e.SheetList)
				.OrderBy(e => e.YearId)
				.ToListAsync();

			if (sheets == null)
			{
				this.validation.ThrowErrorMessage(ReportErrorCode.NoDataForBank);
			}

			foreach (var sheet in sheets)
			{
				totalSum += sheet.PrincipalAbsorbed ?? 0;
				totalSum += sheet.InterestOverPrincipal ?? 0;
				totalSum -= sheet.PrincipalPaid ?? 0;
				totalSum -= sheet.SimpleDebtPrincipal ?? 0;
				totalSum -= sheet.WarrantiesActivatedPrincipal ?? 0;
			}

			var amountRequestedCorrection = await this.context.Set<ApplicationFive>()
				.Where(e => e.BankId == dto.Bank.Id && e.ApplicationFiveType == ApplicationFiveType.TotalDebtExposure && e.CommitState == CommitState.Approved)
				.OrderByDescending(e => e.BlankDate)
				.Select(e => e.AmountRequestedCorrection)
				.FirstOrDefaultAsync();

			return (totalSum / 100, amountRequestedCorrection);
		}

		private async Task<decimal> GenerateRepaidCreditObligationsInTheRelevantYearReport(DividendReportSearchDto dto)
		{
			var totalSum = 0m;

			var sheets = await this.context.Set<Sheet>()
				.Where(e => e.BankId == dto.Bank.Id)
				.Include(e => e.SheetList)
					.ThenInclude(s => s.SheetList)
				.OrderBy(e => e.YearId)
				.ToListAsync();

			if (sheets == null)
			{
				this.validation.ThrowErrorMessage(ReportErrorCode.NoDataForBank);
			}

			foreach (var sheet in sheets)
			{
				var sheetYear = sheet.SheetList.Where(e => e.YearId == dto.Year.Id).SingleOrDefault();

				if (sheetYear == null)
				{
					continue;
				}

				if (dto.Period == YearPeriod.First)
				{
					totalSum += this.SumRepaidCreditObligations(1, 6, sheetYear);
				}
				else if (dto.Period == YearPeriod.Second)
				{
					totalSum += this.SumRepaidCreditObligations(7, 12, sheetYear);
				}
			}

			return (totalSum * 2.5m) / 100;
		}

		private decimal GenerateBankExpensesForTheEnforcementActionsReport(DividendReportSearchDto dto)
		{
			var applicationsFour = this.context.Set<ApplicationFour>()
				.Where(e => e.BankId == dto.Bank.Id && e.CommitState == CommitState.Approved)
				.AsQueryable();

			if (dto.From.HasValue)
			{
				applicationsFour = applicationsFour.Where(e => e.BlankDate >= dto.From);
			}

			if (dto.To.HasValue)
			{
				applicationsFour = applicationsFour.Where(e => e.BlankDate <= dto.To);

			}

			return applicationsFour
				.Sum(e => e.BankExpenses ?? 0);
		}

		private decimal SumRepaidCreditObligations(int firstMonth, int lastMonth, SheetYear sheetYear)
		{
			var totalSum = 0m;

			foreach (var month in sheetYear.SheetList.Where(e => e.Month >= firstMonth && e.Month <= lastMonth))
			{
				if (month.PrincipalPaid.HasValue)
				{
					totalSum += month.PrincipalPaid.Value;
				}

				if (month.InterestPaid.HasValue)
				{
					totalSum += month.InterestPaid.Value;
				}
			}

			return totalSum;
		}
	}
}
