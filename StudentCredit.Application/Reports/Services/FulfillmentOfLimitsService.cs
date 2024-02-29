using Microsoft.EntityFrameworkCore;
using StudentCredit.Application.Reports.Dtos.FulfillmentOfLimits;
using StudentCredit.Application.Reports.Interfaces;
using StudentCredit.Data.SummaryReport;
using StudentCredit.Infrastructure.Helpers.Extensions;
using StudentCredit.Infrastructure.Interfaces.Contexts;

namespace StudentCredit.Application.Reports.Services
{
	public class FulfillmentOfLimitsService : IFulfillmentOfLimitsService
	{
		private readonly IAppDbContext context;

		public FulfillmentOfLimitsService(IAppDbContext context)
		{
			this.context = context;
		}

		public async Task<List<FulfillmentOfLimitsReportDto>> Get()
		{
			var sheetsYear = await this.context.Set<Sheet>()
				.Select(e => e.Year)
				.Distinct()
				.OrderBy(e => e.Name)
				.ToListAsync();

			var yearLimits = await this.context.Set<YearLimit>()
				.ToListAsync();

			var sheets = await this.context.Set<Sheet>()
				.Include(e => e.SheetList)
					.ThenInclude(s => s.SheetList)
				.Include(e => e.Year)
				.Include(e => e.Bank)
				.OrderBy(e => e.Year.Name)
				.ToListAsync();

			var result = new List<FulfillmentOfLimitsReportDto>();

			foreach (var year in sheetsYear)
			{
				var fulfillmentDto = new FulfillmentOfLimitsReportDto
				{
					Year = year.ToNomenclatureDto(),
					YearLimit = yearLimits
						.Where(e => e.YearId == year.Id)
						.Select(e => e.Limit.Value)
						.SingleOrDefault()
				};

				var currentYearSheets = sheets
					.Where(e => e.Year.Id == year.Id)
					.ToList();

				foreach (var currentYearSheet in currentYearSheets)
				{
					var fulfillmentInfoDto = new FulfillmentOfLimitsInfoDto
					{
						BankName = currentYearSheet.Bank.Name,
						BankLimit = currentYearSheet.Limit.Value,
						BankCreditsSize = currentYearSheet.TotalSum.Value + currentYearSheet.RenegotiatedSum.Value,
						CreditsCount = currentYearSheet.CreditCount.Value,
						BankRemainderOfTheApprovedAmount = currentYearSheet.Limit.Value - currentYearSheet.TotalSum.Value + currentYearSheet.RenegotiatedSum.Value,
					};

					if (currentYearSheet.Limit.Value != 0)
					{
						fulfillmentInfoDto.FulfillmentOfLimit = ((currentYearSheet.TotalSum.Value + currentYearSheet.RenegotiatedSum.Value) * 100) / currentYearSheet.Limit.Value;
					}

					fulfillmentDto.FulfillmentOfLimits.Add(fulfillmentInfoDto);
				}

				result.Add(fulfillmentDto);
			}

			return result;
		}
	}
}
