using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using StudentCredit.Application.Common.Extensions;
using StudentCredit.Application.DomainValidation.Enums;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Application.Reports.Dtos.SummaryReports;
using StudentCredit.Application.Reports.Enums;
using StudentCredit.Application.Reports.Interfaces;
using StudentCredit.Data.Banks;
using StudentCredit.Data.Logs.Enums;
using StudentCredit.Data.Nomenclatures;
using StudentCredit.Data.SummaryReport;
using StudentCredit.Data.Users.Constants;
using StudentCredit.Infrastructure.DomainValidation;
using StudentCredit.Infrastructure.DomainValidation.Enums;
using StudentCredit.Infrastructure.Interfaces;
using StudentCredit.Infrastructure.Interfaces.Contexts;
using StudentCredit.Infrastructure.Logs;
using System.Drawing;

namespace StudentCredit.Application.Reports.Services
{
    public class SummaryReportService : ISummaryReportService
	{
		private readonly IAppDbContext context;
		private readonly IMapper mapper;
		private readonly IUserContext userContext;
		private readonly DomainValidationService validationService;
		private readonly ILoggingService loggingService;
		private readonly RoleService roleService;
		private readonly Dictionary<int, string> summaryReportFormulas = new()
		{
			{ 1, "\"a\"" },
			{ 2, "\"b\"" },
			{ 3, "\"c\"" },
			{ 4, "\"d = a + b - c\"" },
			{ 5, "\"e\"" },
			{ 6, "\"f\"" },
			{ 7, "\"g\"" },
			{ 8, "\"h\"" },
			{ 9, "\"i\"" },
			{ 10, "\"j\"" },
			{ 11, "\"k\"" },
			{ 12, "\"l\"" },
			{ 13, "\"m\"" },
			{ 14, "\"n=(a+b)/m,%\"" },
			{ 15, "\"o\"" }
		};

		public SummaryReportService(
			IAppDbContext context,
			IMapper mapper,
			IUserContext userContext,
			DomainValidationService validationService,
			ILoggingService loggingService,
			RoleService roleService
			)
		{
			this.context = context;
			this.mapper = mapper;
			this.userContext = userContext;
			this.validationService = validationService;
			this.loggingService = loggingService;
			this.roleService = roleService;
		}

		public async Task<List<SheetDto>> GetAllSheets(SummaryReportSearchDto dto)
		{
			var isBankUser = this.roleService.ValidateRole(UserRoleAliases.BANK_ACTIVE);

			var sheets = new List<SheetDto>();

			var query = this.context.Set<Sheet>()
				.AsQueryable();

			if (isBankUser || dto.SummaryReportType == SummaryReportType.Bank)
			{
				query = query.Where(e => e.BankId == dto.BankId);
			}

			var sheetsYear = await query
				.Select(e => e.YearId)
				.Distinct()
				.ToListAsync();

			foreach (var sheetYear in sheetsYear)
			{
				if (isBankUser || dto.SummaryReportType == SummaryReportType.Bank)
				{
					var sheet = await this.GetSummaryReportByBank(sheetYear, dto.BankId.Value);
					sheets.Add(sheet);
				}
				else
				{
					var sheet = await this.GetSummaryReport(sheetYear);
					sheets.Add(sheet);
				}
			}

			return sheets
				.OrderBy(e => e.Year.Name)
				.ToList();
		}

		public async Task<SheetDto> GetSummaryReport(int yearId)
		{
			var sheetToReturn = new Sheet();

			var sheets = await this.context.Set<Sheet>()
				.AsNoTracking()
				.Where(e => e.YearId == yearId)
				.Include(e => e.Year)
				.Include(e => e.SheetList)
					.ThenInclude(x => x.Year)
				.Include(e => e.SheetList)
					.ThenInclude(x => x.SheetList)
				.OrderBy(e => e.YearId)
				.ToListAsync();

			if (sheets.Any())
			{
				sheetToReturn = sheets[0];

				foreach (var sheet in sheets.Skip(1))
				{
					this.SumSheetRowModels(sheetToReturn, sheet);

					foreach (var (sheetYearToReturn, sheetYear) in sheetToReturn.SheetList.OrderBy(e => e.YearId).Zip(sheet.SheetList.OrderBy(e => e.YearId)))
					{
						this.SumSheetRowModels(sheetYearToReturn, sheetYear);

						foreach (var (monthDataToReturn, monthData) in sheetYearToReturn.SheetList.OrderBy(e => e.Month).Zip(sheetYear.SheetList.OrderBy(e => e.Month)))
						{
							this.SumSheetRowModels(monthDataToReturn, monthData);
						}
					}
				}

				this.SumFulfillmentOfTheLimit(sheetToReturn);
			}

			return this.mapper.Map<SheetDto>(sheetToReturn);
		}

		public async Task<SheetDto> GetSummaryReportByBank(int yearId, int bankId)
		{
			var sheet = await context.Set<Sheet>()
				.Where(e => e.YearId == yearId && e.BankId == bankId)
				.Include(e => e.SheetList)
					.ThenInclude(c => c.SheetList)
				.Include(e => e.SheetList)
					.ThenInclude(c => c.Year)
				.Include(e => e.Year)
				.SingleOrDefaultAsync();

			if (sheet == null)
			{
				this.validationService.ThrowErrorMessage(ReportErrorCode.NoDataForBank);
			}

			this.SumFulfillmentOfTheLimit(sheet);

			return this.mapper.Map<SheetDto>(sheet);
		}

		public async Task ImportSummaryReportFromExcel(IFormFile file)
		{
			using (var stream = new MemoryStream())
			{
				file.CopyTo(stream);
				stream.Position = 0;

				using (var package = new ExcelPackage(stream))
				{
					var worksheets = package.Workbook.Worksheets.ToList();

					var bulstat = worksheets[0].Cells[1, 2].Value;

					foreach (var worksheet in worksheets)
					{
						var rowCount = worksheet.Dimension.Rows;

						var sheet = new Sheet
						{
							SheetList = new List<SheetYear>()
						};

						var currentSheetYear = new SheetYear();

						for (int row = 4; row <= rowCount; row++)
						{
							var firstColumn = worksheet.Cells[row, 1].Value ?? null;

							if (firstColumn != null)
							{
								var parsedFirstColumn = firstColumn.ToString();

								if (parsedFirstColumn.Trim().ToLower() == "общо")
								{
									sheet.TotalSum = ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 2].Value.ToString()) ?? 0;
									sheet.RenegotiatedSum = ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 3].Value.ToString()) ?? 0;
									sheet.PrincipalAbsorbed = ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 4].Value.ToString()) ?? 0;
									sheet.RemainderToDigest = ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 5].Value.ToString()) ?? 0;
									sheet.InterestOverPrincipal = ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 6].Value.ToString()) ?? 0;
									sheet.PrincipalPaid = ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 7].Value.ToString()) ?? 0;
									sheet.InterestPaid = ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 8].Value.ToString()) ?? 0;
									sheet.DebtWrittenOff = ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 9].Value.ToString()) ?? 0;
									sheet.SimpleDebtPrincipal = ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 10].Value.ToString()) ?? 0;
									sheet.SimpleDebtInterest = ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 11].Value.ToString()) ?? 0;
									sheet.WarrantiesActivatedPrincipal = ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 12].Value.ToString()) ?? 0;
									sheet.WarrantiesActivatedInterest = ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 13].Value.ToString()) ?? 0;
									sheet.Limit = ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 14].Value.ToString()) ?? 0;
									sheet.FulfillmentOfTheLimit = ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 15].Value.ToString()) ?? 0;
									sheet.CreditCount = ParserExtensions.TryParseStringToNullableInt(worksheet.Cells[row, 16].Value.ToString()) ?? 0;

									sheet.Year = await GetYearByName(worksheet.Name);
								}
								else if (parsedFirstColumn.Trim().ToLower() != "общо" && parsedFirstColumn.Length > 2)
								{
									currentSheetYear = new SheetYear
									{
										SheetList = new List<MonthData>(),
										TotalSum = worksheet.Cells[row, 2].Value != null ? ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 2].Value.ToString()) : 0,
										RenegotiatedSum = worksheet.Cells[row, 3].Value != null ? ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 3].Value.ToString()) : 0,
										PrincipalAbsorbed = worksheet.Cells[row, 4].Value != null ? ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 4].Value.ToString()) : 0,
										RemainderToDigest = worksheet.Cells[row, 5].Value != null ? ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 5].Value.ToString()) : 0,
										InterestOverPrincipal = worksheet.Cells[row, 6].Value != null ? ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 6].Value.ToString()) : 0,
										PrincipalPaid = worksheet.Cells[row, 7].Value != null ? ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 7].Value.ToString()) : 0,
										InterestPaid = worksheet.Cells[row, 8].Value != null ? ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 8].Value.ToString()) : 0,
										DebtWrittenOff = worksheet.Cells[row, 9].Value != null ? ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 9].Value.ToString()) : 0,
										SimpleDebtPrincipal = worksheet.Cells[row, 10].Value != null ? ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 10].Value.ToString()) : 0,
										SimpleDebtInterest = worksheet.Cells[row, 11].Value != null ? ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 11].Value.ToString()) : 0,
										WarrantiesActivatedPrincipal = worksheet.Cells[row, 12].Value != null ? ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 12].Value.ToString()) : 0,
										WarrantiesActivatedInterest = worksheet.Cells[row, 13].Value != null ? ParserExtensions.TryParseStringToNullableDecimal(worksheet.Cells[row, 13].Value.ToString()) : 0,
										CreditCount = worksheet.Cells[row, 16].Value != null ? ParserExtensions.TryParseStringToNullableInt(worksheet.Cells[row, 16].Value.ToString()) : 0,
										Year = await GetYearByName(parsedFirstColumn)
									};

									sheet.SheetList.Add(currentSheetYear);
								}
								else if (parsedFirstColumn.Trim().ToLower() != "общо" && parsedFirstColumn.Length > 0 && parsedFirstColumn.Length < 3)
								{
									var monthData = new MonthData
									{
										Month = int.Parse(parsedFirstColumn),
										TotalSum = worksheet.Cells[row, 2].Value != null ? ParserExtensions.TryParseStringToDecimal(worksheet.Cells[row, 2].Value.ToString()) : 0,
										RenegotiatedSum = worksheet.Cells[row, 3].Value != null ? ParserExtensions.TryParseStringToDecimal(worksheet.Cells[row, 3].Value.ToString()) : 0,
										PrincipalAbsorbed = worksheet.Cells[row, 4].Value != null ? ParserExtensions.TryParseStringToDecimal(worksheet.Cells[row, 4].Value.ToString()) : 0,
										RemainderToDigest = worksheet.Cells[row, 5].Value != null ? ParserExtensions.TryParseStringToDecimal(worksheet.Cells[row, 5].Value.ToString()) : 0,
										InterestOverPrincipal = worksheet.Cells[row, 6].Value != null ? ParserExtensions.TryParseStringToDecimal(worksheet.Cells[row, 6].Value.ToString()) : 0,
										PrincipalPaid = worksheet.Cells[row, 7].Value != null ? ParserExtensions.TryParseStringToDecimal(worksheet.Cells[row, 7].Value.ToString()) : 0,
										InterestPaid = worksheet.Cells[row, 8].Value != null ? ParserExtensions.TryParseStringToDecimal(worksheet.Cells[row, 8].Value.ToString()) : 0,
										DebtWrittenOff = worksheet.Cells[row, 9].Value != null ? ParserExtensions.TryParseStringToDecimal(worksheet.Cells[row, 9].Value.ToString()) : 0,
										SimpleDebtPrincipal = worksheet.Cells[row, 10].Value != null ? ParserExtensions.TryParseStringToDecimal(worksheet.Cells[row, 10].Value.ToString()) : 0,
										SimpleDebtInterest = worksheet.Cells[row, 11].Value != null ? ParserExtensions.TryParseStringToDecimal(worksheet.Cells[row, 11].Value.ToString()) : 0,
										WarrantiesActivatedPrincipal = worksheet.Cells[row, 12].Value != null ? ParserExtensions.TryParseStringToDecimal(worksheet.Cells[row, 12].Value.ToString()) : 0,
										WarrantiesActivatedInterest = worksheet.Cells[row, 13].Value != null ? ParserExtensions.TryParseStringToDecimal(worksheet.Cells[row, 13].Value.ToString()) : 0,
										CreditCount = worksheet.Cells[row, 16].Value != null ? ParserExtensions.TryParseStringToNullableInt(worksheet.Cells[row, 16].Value.ToString()) : 0
									};

									currentSheetYear.SheetList.Add(monthData);
								}
							}
						}

						sheet.Bank = context.Set<Bank>()
							.SingleOrDefault(e => e.Bulstat == bulstat.ToString());

						context.Set<Sheet>().Add(sheet);
					}

					await context.SaveChangesAsync();
				}
			}
		}

		public async Task<SheetDto> UpdateMonthData(int sheetId, MonthDataDto dto)
		{
			var sheet = await this.context.Set<Sheet>()
				.Where(e => e.Id == sheetId)
				.Include(e => e.SheetList)
					.ThenInclude(x => x.SheetList)
				.SingleOrDefaultAsync();

			if (sheet.BankId != this.userContext.BankId)
			{
				this.validationService.ThrowErrorMessage(UserErrorCode.User_NotEnoughPermission);
			}

			var sheetYear = sheet.SheetList.SingleOrDefault(e => e.Id == dto.SheetYearId);

			var monthData = sheetYear.SheetList.SingleOrDefault(e => e.Id == dto.Id);
			monthData = this.mapper.Map(dto, monthData);

			this.SumSheetList(sheetYear, sheetYear.SheetList);

			this.SumSheetList(sheet, sheet.SheetList);

			this.SumFulfillmentOfTheLimit(sheet);

			await this.context.SaveChangesAsync();

			return await this.GetSummaryReportByBank(sheet.YearId, this.userContext.BankId.Value);
		}

		public async Task<SheetDto> ChangeBankYearLimit(int yearId, decimal? yearLimit)
		{
			var sheet = await this.context.Set<Sheet>()
			   .Where(e => e.YearId == yearId && e.BankId == this.userContext.BankId)
			   .SingleOrDefaultAsync();

			sheet.Limit = yearLimit;
			this.SumFulfillmentOfTheLimit(sheet);

			await this.context.SaveChangesAsync();

			return await this.GetSummaryReportByBank(yearId, this.userContext.BankId.Value);
		}

		public async Task<SheetDto> ChangeYearLimit(int yearId, decimal? yearLimit)
		{
			var limit = await this.context.Set<YearLimit>()
				.SingleOrDefaultAsync(e => e.YearId == yearId);

			if (limit != null)
			{
				limit.Limit = yearLimit;
			}
			else
			{
				var newLimit = new YearLimit(yearLimit, yearId);
				this.context.Set<YearLimit>().Add(newLimit);
			}

			await this.context.SaveChangesAsync();

			return await this.GetSummaryReport(yearId);
		}

		public async Task CreateSheets()
		{
			try
			{
				var year = await this.GetYearByName(DateTime.Now.Year.ToString());

				var bankIds = await this.context.Set<Bank>()
					.Where(e => e.IsActive == true)
					.Select(e => e.Id)
					.ToListAsync();

				foreach (var bankId in bankIds)
				{
					var sheetExist = await this.context.Set<Sheet>()
						.AnyAsync(e => e.YearId == year.Id && e.BankId == bankId);

					if (!sheetExist)
					{
						var sheet = new Sheet(year.Id, bankId);

						this.context.Set<Sheet>().Add(sheet);
					}
				}

				await this.context.SaveChangesAsync();

				await this.loggingService.LogMessageAsync($"Successfully created sheet!");
			}
			catch (Exception ex)
			{
				await this.loggingService.LogExceptionAsync(ex, LogType.SummaryReportJobExceptionLog);
			}
		}

		public async Task CreateSheetYears()
		{
			try
			{
				var year = await this.GetYearByName(DateTime.Now.Year.ToString());

				var sheets = await this.context.Set<Sheet>()
					.Include(e => e.SheetList)
					.ToListAsync();

				foreach (var sheet in sheets)
				{
					var existSheetYear = sheet.SheetList.Any(e => e.YearId == year.Id);

					if (!existSheetYear)
					{
						sheet.SheetList.Add(new SheetYear(year.Id));
					}
				}

				await this.context.SaveChangesAsync();

				await this.loggingService.LogMessageAsync($"Successfully created sheetYears!");
			}
			catch (Exception ex)
			{
				await this.loggingService.LogExceptionAsync(ex, LogType.SummaryReportJobExceptionLog);
			}
		}

		public async Task CreateSheetYearsMonth()
		{
			try
			{
				var year = await this.GetYearByName(DateTime.Now.Year.ToString());

				var sheets = await this.context.Set<Sheet>()
					.Include(e => e.SheetList)
						.ThenInclude(x => x.SheetList)
					.ToListAsync();

				foreach (var sheet in sheets)
				{
					var sheetYear = sheet.SheetList.SingleOrDefault(e => e.YearId == year.Id);

					var existMonthData = sheetYear.SheetList.Any(e => e.Month == DateTime.Now.Month);

					if (!existMonthData)
					{
						sheetYear.SheetList.Add(new MonthData(DateTime.Now.Month));
					}
				}

				await this.context.SaveChangesAsync();

				await this.loggingService.LogMessageAsync($"Successfully created sheet years month!");
			}
			catch (Exception ex)
			{
				await this.loggingService.LogExceptionAsync(ex, LogType.SummaryReportJobExceptionLog);

			}
		}

		public MemoryStream ExportInExcel(List<SheetDto> sheets)
		{
			try
			{
				using (ExcelPackage package = new ExcelPackage())
				{
					foreach (var sheet in sheets)
					{
						ExcelWorksheet worksheet = package.Workbook.Worksheets.Add($"{sheet.Year.Name}");

						#region RowAndColumnSizes
						worksheet.Column(1).Width = 6.59;
						worksheet.Column(2).Width = 16.82;
						worksheet.Column(3).Width = 15.91;
						worksheet.Column(4).Width = 17.64;
						worksheet.Column(5).Width = 16.36;
						worksheet.Column(6).Width = 16.82;
						worksheet.Column(7).Width = 16.18;
						worksheet.Column(8).Width = 16.09;
						worksheet.Column(9).Width = 14.82;
						worksheet.Column(10).Width = 14.55;
						worksheet.Column(11).Width = 13.64;
						worksheet.Column(12).Width = 15.09;
						worksheet.Column(13).Width = 14.82;
						worksheet.Column(14).Width = 16.64;
						worksheet.Column(15).Width = 16.55;
						worksheet.Column(16).Width = 13.18;
						worksheet.Row(3).Height = 26.3;
						worksheet.Row(4).Height = 90;
						#endregion

						#region Headers
						worksheet.Cells[3, 2, 4, 16].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
						worksheet.Cells[3, 2, 4, 16].Style.Fill.PatternType = ExcelFillStyle.Solid;
						worksheet.Cells[3, 2, 4, 16].Style.WrapText = true;
						worksheet.Cells[4, 2, 4, 5].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);
						worksheet.Cells[3, 6, 3, 7].Style.Fill.BackgroundColor.SetColor(Color.DarkGray);
						worksheet.Cells[4, 7, 4, 8].Style.Fill.BackgroundColor.SetColor(Color.DarkGray);
						worksheet.Cells[4, 10, 4, 11].Style.Fill.BackgroundColor.SetColor(Color.GhostWhite);
						worksheet.Cells[4, 12, 4, 13].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);
						worksheet.Cells[3, 15, 3, 16].Style.Fill.BackgroundColor.SetColor(Color.Tan);

						worksheet.Cells["B2:P2"].Merge = true;
						worksheet.Cells[2, 2].Value = $"Справка за броя, размера, състоянието и движението на  предоставените кредити по реда на ЗКСД за отчентия период (от 01.01.{sheet.Year.Name} г. до {DateTime.Today:dd.MM.yyyy} г. включително)";

						worksheet.Cells["B3:E3"].Merge = true;
						worksheet.Cells[3, 2].Value = "Кредити (главници) по ЗКСД";
						worksheet.Cells[3, 2].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);

						worksheet.Cells[4, 2].Value = $"Договорен размер на кредитите, сключени през {sheet.Year.Name} г.";

						worksheet.Cells[4, 3].Value = "Предоговорени (+) увеличение (-) намаление";

						worksheet.Cells[4, 4].Value = "Усвоени за такси за обучение или за издръжка средства (главници)";

						worksheet.Cells[4, 5].Value = "Остатък за усвояване";

						worksheet.Cells["F3:F4"].Merge = true;
						worksheet.Cells[3, 6].Value = "Начислена по време на гратисния период лихва върху усвоената част от кредита, която се капитализира годишно";

						worksheet.Cells["G3:H3"].Merge = true;
						worksheet.Cells[3, 7].Value = "Извършени плащания";

						worksheet.Cells[4, 7].Value = "Главници";

						worksheet.Cells[4, 8].Value = "Лихви";

						worksheet.Cells["I3:I4"].Merge = true;
						worksheet.Cells[3, 9].Value = "Отписан дълг - главници";
						worksheet.Cells[3, 9].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);

						worksheet.Cells["J3:K3"].Merge = true;
						worksheet.Cells[3, 10].Value = "Опростен дълг";
						worksheet.Cells[3, 10].Style.Fill.BackgroundColor.SetColor(Color.GhostWhite);

						worksheet.Cells[4, 10].Value = "Главници";

						worksheet.Cells[4, 11].Value = "Лихви";

						worksheet.Cells["L3:M3"].Merge = true;
						worksheet.Cells[3, 12].Value = "Активирани гаранции";
						worksheet.Cells[3, 12].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);

						worksheet.Cells[4, 12].Value = "Главници";

						worksheet.Cells[4, 13].Value = "Лихви";

						worksheet.Cells["N3:N4"].Merge = true;
						worksheet.Cells[3, 14].Value = "Лимит";
						worksheet.Cells[3, 14].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);

						worksheet.Cells["O3:O4"].Merge = true;
						worksheet.Cells[3, 15].Value = "Изпълнение на лимита";

						worksheet.Cells["P3:P4"].Merge = true;
						worksheet.Cells[3, 16].Value = "Брой кредити";
						#endregion

						#region Formulas
						for (int i = 1; i < 16; i++)
						{
							worksheet.Cells[5, i + 1].Value = this.summaryReportFormulas[i];
						}
						#endregion

						#region Sheet
						worksheet.Cells[6, 1].Value = "ОБЩО";
						worksheet.Cells[6, 2].Value = sheet.TotalSum != null ? sheet.TotalSum.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
						worksheet.Cells[6, 3].Value = sheet.RenegotiatedSum != null ? sheet.RenegotiatedSum.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
						worksheet.Cells[6, 4].Value = sheet.PrincipalAbsorbed != null ? sheet.PrincipalAbsorbed.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
						worksheet.Cells[6, 5].Value = sheet.RemainderToDigest != null ? sheet.RemainderToDigest.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
						worksheet.Cells[6, 6].Value = sheet.InterestOverPrincipal != null ? sheet.InterestOverPrincipal.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
						worksheet.Cells[6, 7].Value = sheet.PrincipalPaid != null ? sheet.PrincipalPaid.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
						worksheet.Cells[6, 8].Value = sheet.InterestPaid != null ? sheet.InterestPaid.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
						worksheet.Cells[6, 9].Value = sheet.DebtWrittenOff != null ? sheet.DebtWrittenOff.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
						worksheet.Cells[6, 10].Value = sheet.SimpleDebtPrincipal != null ? sheet.SimpleDebtPrincipal.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
						worksheet.Cells[6, 11].Value = sheet.SimpleDebtInterest != null ? sheet.SimpleDebtInterest.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
						worksheet.Cells[6, 12].Value = sheet.WarrantiesActivatedPrincipal != null ? sheet.WarrantiesActivatedPrincipal.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
						worksheet.Cells[6, 13].Value = sheet.WarrantiesActivatedInterest != null ? sheet.WarrantiesActivatedInterest.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
						worksheet.Cells[6, 14].Value = sheet.Limit != null ? sheet.Limit.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
						worksheet.Cells[6, 15].Value = sheet.FulfillmentOfTheLimit != null ? sheet.FulfillmentOfTheLimit.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
						worksheet.Cells[6, 16].Value = sheet.CreditCount;
						#endregion

						int row = 7;

						#region SheetYears
						foreach (var sheetYear in sheet.SheetList.OrderBy(e => e.Year.Id))
						{
							worksheet.Cells[row, 1].Value = sheetYear.Year.Name;
							worksheet.Cells[row, 2].Value = sheetYear.TotalSum != null ? sheetYear.TotalSum.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
							worksheet.Cells[row, 3].Value = sheetYear.RenegotiatedSum != null ? sheetYear.RenegotiatedSum.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
							worksheet.Cells[row, 4].Value = sheetYear.PrincipalAbsorbed != null ? sheetYear.PrincipalAbsorbed.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
							worksheet.Cells[row, 5].Value = sheetYear.RemainderToDigest != null ? sheetYear.RemainderToDigest.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
							worksheet.Cells[row, 6].Value = sheetYear.InterestOverPrincipal != null ? sheetYear.InterestOverPrincipal.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
							worksheet.Cells[row, 7].Value = sheetYear.PrincipalPaid != null ? sheetYear.PrincipalPaid.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
							worksheet.Cells[row, 8].Value = sheetYear.InterestPaid != null ? sheetYear.InterestPaid.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
							worksheet.Cells[row, 9].Value = sheetYear.DebtWrittenOff != null ? sheetYear.DebtWrittenOff.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
							worksheet.Cells[row, 10].Value = sheetYear.SimpleDebtPrincipal != null ? sheetYear.SimpleDebtPrincipal.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
							worksheet.Cells[row, 11].Value = sheetYear.SimpleDebtInterest != null ? sheetYear.SimpleDebtInterest.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
							worksheet.Cells[row, 12].Value = sheetYear.WarrantiesActivatedPrincipal != null ? sheetYear.WarrantiesActivatedPrincipal.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
							worksheet.Cells[row, 13].Value = sheetYear.WarrantiesActivatedInterest != null ? sheetYear.WarrantiesActivatedInterest.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
							worksheet.Cells[row, 16].Value = sheetYear.CreditCount ?? 0;

							worksheet.Cells[row, 1, row, 16].Style.Fill.PatternType = ExcelFillStyle.Solid;
							var color = ColorTranslator.FromHtml("#FFDFD991");
							worksheet.Cells[row, 1, row, 16].Style.Fill.BackgroundColor.SetColor(color);
							worksheet.Cells[row, 1, row, 16].Style.Font.Bold = true;
							worksheet.Cells[row, 1, row, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

							var outlineLevel = 1;

							row++;

							foreach (var monthData in sheetYear.SheetList.OrderBy(e => e.Month))
							{
								worksheet.Cells[row, 1].Value = monthData.Month;
								worksheet.Cells[row, 2].Value = monthData.TotalSum != null ? monthData.TotalSum.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
								worksheet.Cells[row, 3].Value = monthData.RenegotiatedSum != null ? monthData.RenegotiatedSum.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
								worksheet.Cells[row, 4].Value = monthData.PrincipalAbsorbed != null ? monthData.PrincipalAbsorbed.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
								worksheet.Cells[row, 5].Value = monthData.RemainderToDigest != null ? monthData.RemainderToDigest.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
								worksheet.Cells[row, 6].Value = monthData.InterestOverPrincipal != null ? monthData.InterestOverPrincipal.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
								worksheet.Cells[row, 7].Value = monthData.PrincipalPaid != null ? monthData.PrincipalPaid.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
								worksheet.Cells[row, 8].Value = monthData.InterestPaid != null ? monthData.InterestPaid.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
								worksheet.Cells[row, 9].Value = monthData.DebtWrittenOff != null ? monthData.DebtWrittenOff.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
								worksheet.Cells[row, 10].Value = monthData.SimpleDebtPrincipal != null ? monthData.SimpleDebtPrincipal.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
								worksheet.Cells[row, 11].Value = monthData.SimpleDebtInterest != null ? monthData.SimpleDebtInterest.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
								worksheet.Cells[row, 12].Value = monthData.WarrantiesActivatedPrincipal != null ? monthData.WarrantiesActivatedPrincipal.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
								worksheet.Cells[row, 13].Value = monthData.WarrantiesActivatedInterest != null ? monthData.WarrantiesActivatedInterest.Value.RoundDecimalWithTwoPlaces() : 0m.RoundDecimalWithTwoPlaces();
								worksheet.Cells[row, 16].Value = monthData.CreditCount ?? 0;

								worksheet.Cells[row, 1, row, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
								worksheet.Row(row).OutlineLevel = outlineLevel;
								worksheet.Row(row).Collapsed = true;
								row++;
							}

							outlineLevel++;
						}
						#endregion

						#region Styles
						for (int forRow = 6; forRow <= worksheet.Dimension.End.Row; forRow++)
						{
							for (int col = 2; col < 14; col++)
							{
								worksheet.Cells[forRow, col].Value += " лв.";
							}
						}

						worksheet.Cells[2, 1, 6, 16].Style.Font.Bold = true;
						worksheet.Cells[2, 1, 6, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

						worksheet.Cells[1, 1, worksheet.Dimension.End.Row, 16].Style.Border.Top.Style = ExcelBorderStyle.Thin;
						worksheet.Cells[1, 1, worksheet.Dimension.End.Row, 16].Style.Border.Left.Style = ExcelBorderStyle.Thin;
						worksheet.Cells[1, 1, worksheet.Dimension.End.Row, 16].Style.Border.Right.Style = ExcelBorderStyle.Thin;
						worksheet.Cells[1, 1, worksheet.Dimension.End.Row, 16].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
						worksheet.Cells[1, 1, worksheet.Dimension.End.Row, 16].Style.Font.Name = "Colibri";
						worksheet.Cells[1, 1, worksheet.Dimension.End.Row, 16].Style.Font.Size = 10;
						#endregion
					}

					var stream = new MemoryStream(package.GetAsByteArray());

					return stream;
				}
			}
			catch (Exception)
			{
				this.validationService.ThrowErrorMessage(SystemErrorCode.System_ExportProblem);

				throw;
			}
		}

		private async Task<Year> GetYearByName(string name)
		{
			return await context.Set<Year>()
						.SingleOrDefaultAsync(e => e.Name.Trim() == name.Trim());
		}

		private void SumSheetList<TModel, T>(TModel model, List<T> list)
			where TModel : SheetRowData, ISheetList<T>
			where T : SheetRowData
		{
			model.TotalSum = list.Sum(e => e.TotalSum ?? 0);
			model.RenegotiatedSum = list.Sum(e => e.RenegotiatedSum ?? 0);
			model.PrincipalAbsorbed = list.Sum(e => e.PrincipalAbsorbed ?? 0);
			model.RemainderToDigest = list.Sum(e => e.RemainderToDigest ?? 0);
			model.InterestOverPrincipal = list.Sum(e => e.InterestOverPrincipal ?? 0);
			model.PrincipalPaid = list.Sum(e => e.PrincipalPaid ?? 0);
			model.InterestPaid = list.Sum(e => e.InterestPaid ?? 0);
			model.DebtWrittenOff = list.Sum(e => e.DebtWrittenOff ?? 0);
			model.SimpleDebtPrincipal = list.Sum(e => e.SimpleDebtPrincipal ?? 0);
			model.SimpleDebtInterest = list.Sum(e => e.SimpleDebtInterest ?? 0);
			model.WarrantiesActivatedPrincipal = list.Sum(e => e.WarrantiesActivatedPrincipal ?? 0);
			model.WarrantiesActivatedInterest = list.Sum(e => e.WarrantiesActivatedInterest ?? 0);
			model.CreditCount = list.Sum(e => e.CreditCount ?? 0);
		}

		private void SumSheetRowModels<TModel>(TModel modelToReturn, TModel model)
			where TModel : SheetRowData
		{
			modelToReturn.TotalSum += model.TotalSum ?? 0;
			modelToReturn.RenegotiatedSum += model.RenegotiatedSum ?? 0;
			modelToReturn.PrincipalAbsorbed += model.PrincipalAbsorbed ?? 0;
			modelToReturn.RemainderToDigest += model.RemainderToDigest ?? 0;
			modelToReturn.InterestOverPrincipal += model.InterestOverPrincipal ?? 0;
			modelToReturn.PrincipalPaid += model.PrincipalPaid ?? 0;
			modelToReturn.InterestPaid += model.InterestPaid ?? 0;
			modelToReturn.DebtWrittenOff += model.DebtWrittenOff ?? 0;
			modelToReturn.SimpleDebtPrincipal += model.SimpleDebtPrincipal ?? 0;
			modelToReturn.SimpleDebtInterest += model.SimpleDebtInterest ?? 0;
			modelToReturn.WarrantiesActivatedPrincipal += model.WarrantiesActivatedPrincipal ?? 0;
			modelToReturn.WarrantiesActivatedInterest += model.WarrantiesActivatedInterest ?? 0;
			modelToReturn.Limit += model.Limit ?? 0;

			if (modelToReturn.CreditCount == null)
			{
				modelToReturn.CreditCount = 0;
			}
			modelToReturn.CreditCount += model.CreditCount ?? 0;
		}

		private void SumFulfillmentOfTheLimit(Sheet sheet)
		{
			sheet.FulfillmentOfTheLimit = ((sheet.TotalSum + sheet.RenegotiatedSum) * 100) / sheet.Limit;
		}
	}
}
