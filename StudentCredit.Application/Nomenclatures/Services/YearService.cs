using Microsoft.EntityFrameworkCore;
using StudentCredit.Application.Common.Extensions;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Application.Nomenclatures.Dtos;
using StudentCredit.Application.Nomenclatures.Interfaces;
using StudentCredit.Data.Nomenclatures;
using StudentCredit.Data.SummaryReport;
using StudentCredit.Data.Users.Constants;
using StudentCredit.Infrastructure.Interfaces;
using StudentCredit.Infrastructure.Interfaces.Contexts;

namespace StudentCredit.Application.Nomenclatures.Services
{
	public class YearService : IYearService
	{
		private readonly IAppDbContext context;
		private readonly IUserContext userContext;
		private readonly RoleService roleService;

		public YearService(
			IAppDbContext context, 
			IUserContext userContext, 
			RoleService roleService
			)
		{
			this.context = context;
			this.userContext = userContext;
			this.roleService = roleService;
		}

		public async Task<IEnumerable<NomenclatureMapperDto<Year>>> GetSummaryReportYears(BaseNomenclatureFilterDto<Year> filter)
		{
			var summaryReportSheets = this.context.Set<Sheet>()
				.AsNoTracking()
				.AsQueryable();

			if (this.roleService.ValidateRole(UserRoleAliases.BANK_ACTIVE))
			{
				summaryReportSheets = summaryReportSheets.Where(e => e.BankId == this.userContext.BankId);
			}

			var summaryReportYearIds = await summaryReportSheets
				.Select(e => e.YearId)
				.Distinct()
				.ToListAsync();

			var query = this.context.Set<Year>()
				.Where(e => summaryReportYearIds.Contains(e.Id))
				.AsQueryable();

			query = filter.WhereBuilder(query);
			query = filter.OrderBuilder(query);

			if (filter.Limit.HasValue && filter.Offset.HasValue)
			{
				query = query.ApplyPagination(filter.Offset.Value, filter.Limit.Value);
			}

			return await query.Select(new NomenclatureMapperDto<Year>().Map()).ToListAsync();
		}
	}
}
