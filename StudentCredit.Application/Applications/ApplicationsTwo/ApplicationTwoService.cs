using Microsoft.EntityFrameworkCore;
using StudentCredit.Application.Common.Dtos;
using StudentCredit.Application.Applications.ApplicationsTwo.Dtos;
using StudentCredit.Infrastructure.Interfaces.Contexts;
using StudentCredit.Data.Applications.ApplicationTwo;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using StudentCredit.Application.Users.Interfaces;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Data.Users.Constants;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using StudentCredit.Data.Banks;
using StudentCredit.Infrastructure.DomainValidation;
using StudentCredit.Infrastructure.DomainValidation.Enums;
using StudentCredit.Infrastructure.Interfaces;
using System.Linq.Expressions;
using StudentCredit.Infrastructure.Utils.Interfaces;
using StudentCredit.Application.DomainValidation.Enums;

namespace StudentCredit.Application.Applications.ApplicationsTwo
{
    public class ApplicationTwoService : IApplicationTwoService
	{
		private readonly IAppDbContext context;
		private readonly IMapper mapper;
		private readonly IUserService userService;
		private readonly RoleService roleService;
		private readonly DomainValidationService validationService;
		private readonly IUserContext userContext;
		private readonly IExcelProcessor excelProcessor;

		public ApplicationTwoService(
			IAppDbContext context,
			IMapper mapper,
			IUserService userService,
			RoleService roleService,
			DomainValidationService validationService,
			IUserContext userContext,
			IExcelProcessor excelProcessor
			)
		{
			this.context = context;
			this.mapper = mapper;
			this.userService = userService;
			this.roleService = roleService;
			this.validationService = validationService;
			this.userContext = userContext;
			this.excelProcessor = excelProcessor;
		}

		public async Task<SearchResultItemDto<ApplicationTwoSearchResultItemDto>> GetApplicationsFiltered(SearchApplicationTwoFilter filter)
		{
			var query = this.context.Set<ApplicationTwo>()
				.AsQueryable();

			if (filter.BankId.HasValue)
			{
				query = query.Where(e => e.BankId == filter.BankId);
			}

			if (filter.FromDate.HasValue)
			{
				query = query.Where(e => e.RecordDate >= filter.FromDate);
			}

			if (filter.ToDate.HasValue)
			{
				query = query.Where(e => e.RecordDate <= filter.ToDate);
			}

			var applications = await query
				.OrderByDescending(c => c.CreationDate)
					.ThenByDescending(c => c.RecordDate)
				.Skip(filter.Offset)
				.Take(filter.Limit)
				.ProjectTo<ApplicationTwoSearchResultItemDto>(this.mapper.ConfigurationProvider)
				.ToListAsync();

			return new SearchResultItemDto<ApplicationTwoSearchResultItemDto>
			{
				Items = applications,
				TotalCount = query.Count()
			};
		}

		public async Task<ApplicationTwoDto> GetById(int id)
		{
			var applicationTwo = await this.context.Set<ApplicationTwo>()
				.AsNoTracking()
				.Where(a => a.Id == id)
				.ProjectTo<ApplicationTwoDto>(this.mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();

			if (this.roleService.ValidateRole(UserRoleAliases.BANK_ACTIVE))
			{
				this.userService.ValidateUserBank(applicationTwo.Bank.Id);
			}

			return applicationTwo;
		}

		public async Task<int> Create(ApplicationTwoDto dto)
		{
			await this.CheckIfApplicationTwoExist(dto.Bank.Id, dto.RecordDate);

			var applicationTwo = this.mapper.Map<ApplicationTwoDto, ApplicationTwo>(dto);

			this.context.Set<ApplicationTwo>().Add(applicationTwo);

			await this.context.SaveChangesAsync();

			return applicationTwo.Id;
		}

		public async Task AddRecordEntry(RecordEntryDto dto)
		{
			var applicationTwo = await this.context.Set<ApplicationTwo>()
				.Include(e => e.RecordEntries)
				.SingleOrDefaultAsync(e => e.Id == dto.ApplicationTwoId);

			if (this.roleService.ValidateRole(UserRoleAliases.BANK_ACTIVE))
			{
				this.userService.ValidateUserBank(applicationTwo.BankId);

			}

			var recordEntry = this.mapper.Map<RecordEntryDto, RecordEntry>(dto);

			applicationTwo.RecordEntries.Add(recordEntry);

			await this.context.SaveChangesAsync();
		}

		public void Edit(ApplicationTwoDto model)
		{
			this.context.Set<ApplicationTwo>()
						.Where(e => e.Id == model.Id)
						.ExecuteUpdate(x => x
						.SetProperty(a => a.CreationDate, model.CreationDate)
						.SetProperty(a => a.RecordDate, model.RecordDate)
						.SetProperty(a => a.TotalSum, model.TotalSum)
						.SetProperty(a => a.CreditCount, model.CreditCount));
		}

		public async Task ImportFromExcel(IFormFile file)
		{
			using (var stream = new MemoryStream())
			{
				file.CopyTo(stream);
				stream.Position = 0;

				using (var package = new ExcelPackage(stream))
				{
					ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

					if (worksheet == null)
					{
						this.validationService.ThrowErrorMessage(CreditErrorCode.ApplicationTwoExcelWorkSheetIsNull);
					}
					else
					{
						var rowCount = worksheet.Dimension.Rows;

						var appTwo = new ApplicationTwo
						{
							RecordEntries = new List<RecordEntry>()
						};

						for (int row = 1; row <= rowCount; row++)
						{
							if (row == 2)
							{
								var bankName = worksheet.Cells[row, 1].Value;
								var bulstat = worksheet.Cells[row, 2].Value;
								var creationDate = (DateTime?)worksheet.Cells[row, 3].Value;
								var recordDate = (DateTime?)worksheet.Cells[row, 4].Value;
								var totalSum = (double)worksheet.Cells[row, 5].Value;
								var creditCount = (double?)worksheet.Cells[row, 6].Value;

								var bank = await this.context.Set<Bank>()
									.SingleOrDefaultAsync(e => e.Id == this.userContext.BankId);

								if (bank.Bulstat != bulstat.ToString())
								{
									this.validationService.ThrowErrorMessage(UserErrorCode.User_NotEnoughPermission);
								}

								appTwo.Bank = bank;
								appTwo.CreationDate = creationDate.Value;
								appTwo.RecordDate = recordDate.Value;
								appTwo.TotalSum = (decimal)totalSum;
								appTwo.CreditCount = (int)creditCount.Value;
							}

							if (row > 3)
							{
								var creditNumber = worksheet.Cells[row, 1].Value;
								var studentNames = worksheet.Cells[row, 2].Value;
								var uin = worksheet.Cells[row, 3].Value;
								var totalSum = (double?)worksheet.Cells[row, 4].Value;
								var principalAbsorbed = (double?)worksheet.Cells[row, 5].Value;
								var interest = (double?)worksheet.Cells[row, 6].Value;
								var capitalizedPrincipal = (double?)worksheet.Cells[row, 7].Value;
								var isRepaid = (double?)worksheet.Cells[row, 8].Value;
								var monthlyPayment = (double?)worksheet.Cells[row, 9].Value;
								var repaidMonthlyPrincipal = (double?)worksheet.Cells[row, 10].Value;
								var repaidMonthlyInterest = (double?)worksheet.Cells[row, 11].Value;

								var recordEntry = new RecordEntry
								{
									CreditNumber = creditNumber.ToString(),
									StudentFullName = studentNames.ToString().Trim(),
									Uin = uin.ToString().Trim(),
									TotalSum = (decimal)totalSum,
									PrincipalAbsorbed = (decimal)principalAbsorbed,
									Interest = (decimal)interest,
									CapitalizedPrincipal = (decimal)capitalizedPrincipal,
									IsRepaid = isRepaid == 1 ? true : false,
									MonthlyPayment = (decimal)monthlyPayment,
									RepaidMonthlyPrincipal = (decimal)repaidMonthlyPrincipal,
									RepaidMonthlyInterest = (decimal)repaidMonthlyInterest
								};

								appTwo.RecordEntries.Add(recordEntry);
							}
						}

						await this.CheckIfApplicationTwoExist(appTwo.Bank.Id, appTwo.RecordDate);

						this.context.Set<ApplicationTwo>().Add(appTwo);
						await this.context.SaveChangesAsync();
					}
				}
			}
		}

		public MemoryStream ExportInExcel(ApplicationTwoDto dto, List<RecordEntryDto> records, params Expression<Func<RecordEntryDto, ExcelTableTuple>>[] expr)
		{
			try
			{
				using (ExcelPackage package = new ExcelPackage())
				{
					var mainHeaders = new List<string>()
				{
					"Банка",
					"Булстат",
					"Дата на подаване на Приложение 2",
					"Отчетна дата",
					"Общ размер на кредитите на банката",
					"Брой сключени договори за кредит"
				};

					ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Приложение 2");

					int col = 1, row = 1;

					foreach (var header in mainHeaders)
					{
						worksheet.Cells[row, col].Value = header;
						worksheet.Cells[row, col].Style.Font.Bold = true;
						col++;
					}

					worksheet.Cells[2, 1].Value = dto.Bank.Name;
					worksheet.Cells[2, 2].Value = dto.Bank.Bulstat;
					worksheet.Cells[2, 3].Value = dto.CreationDate.ToString("dd-MM-yyyy");
					worksheet.Cells[2, 4].Value = dto.RecordDate.ToString("dd-MM-yyyy");
					worksheet.Cells[2, 5].Value = dto.TotalSum.ToString("0.00");
					worksheet.Cells[2, 6].Value = dto.CreditCount;

					this.excelProcessor.FillWorksheetCells(worksheet, dto.RecordEntries, 3, 1, expr);

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

		private async Task CheckIfApplicationTwoExist(int bankId, DateTime recordDate)
		{
			var existingAppTwo = await this.context.Set<ApplicationTwo>()
							.SingleOrDefaultAsync(e => e.Bank.Id == bankId && e.RecordDate.Month == recordDate.Month);

			if (existingAppTwo != null)
			{
				this.context.Set<ApplicationTwo>().Remove(existingAppTwo);
			}
		}
	}
}