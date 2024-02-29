using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.Common.Dtos;
using StudentCredit.Application.Applications.ApplicationsTwo;
using StudentCredit.Application.Applications.ApplicationsTwo.Dtos;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Data.Users.Constants;
using StudentCredit.Application.Users.Interfaces;
using StudentCredit.Infrastructure.Utils;

namespace StudentCredit.Hosting.Controllers.Applications
{
	[ApiController]
	[Route("api/[controller]")]
	public class ApplicationTwoController : Controller
	{
		private readonly IApplicationTwoService applicationTwoService;
		private readonly RoleService roleService;
		private readonly IUserService userService;

		public ApplicationTwoController(
			IApplicationTwoService applicationTwoService,
			RoleService roleService,
			IUserService userService
			)
		{
			this.applicationTwoService = applicationTwoService;
			this.roleService = roleService;
			this.userService = userService;
		}

		[HttpGet]
		public async Task<SearchResultItemDto<ApplicationTwoSearchResultItemDto>> GetApplicationsFiltered([FromQuery] SearchApplicationTwoFilter filter)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            if (this.roleService.ValidateRole(UserRoleAliases.BANK_ACTIVE))
			{
				this.userService.ValidateUserBank(filter.BankId.Value);
			}

			return await this.applicationTwoService.GetApplicationsFiltered(filter);
		}

		[HttpGet("{id:int}")]
		public async Task<ApplicationTwoDto> GetById([FromRoute] int id)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            return await this.applicationTwoService.GetById(id);
		}

		[HttpPost]
		public async Task<int> Create([FromBody] ApplicationTwoDto model)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            return await this.applicationTwoService.Create(model);
		}

		[HttpPost("recordEntry")]
		public async Task AddRecordEntry([FromBody] RecordEntryDto model)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            await this.applicationTwoService.AddRecordEntry(model);
		}

		[HttpPost("importExcel")]
		[Consumes("multipart/form-data")]
		public async Task ImportExcel([FromForm] IFormFile file)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            await this.applicationTwoService.ImportFromExcel(file);
		}

		[HttpPut]
		public void Edit([FromBody] ApplicationTwoDto model)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            if (this.roleService.ValidateRole(UserRoleAliases.BANK_ACTIVE))
			{
				this.userService.ValidateUserBank(model.Bank.Id);
			}

			this.applicationTwoService.Edit(model);
		}

		[HttpPost("Excel")]
		public FileStreamResult ExportInExcel([FromBody] ApplicationTwoDto model)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            var excelStream = this.applicationTwoService.ExportInExcel(model, model.RecordEntries, 
				e => new ExcelTableTuple { CellItem = e.CreditNumber, ColumnName = "№ на кредит" },
				e => new ExcelTableTuple { CellItem = e.StudentFullName, ColumnName = "Кредитополучател" },
				e => new ExcelTableTuple { CellItem = e.Uin, ColumnName = "ЕГН" },
				e => new ExcelTableTuple { CellItem = e.TotalSum, ColumnName = "Общ размер" },
				e => new ExcelTableTuple { CellItem = e.PrincipalAbsorbed, ColumnName = "Усвоена главница" },
				e => new ExcelTableTuple { CellItem = e.Interest, ColumnName = "Лихва" },
				e => new ExcelTableTuple { CellItem = e.CapitalizedPrincipal, ColumnName = "Капитализирана главница" },
				e => new ExcelTableTuple { CellItem = e.IsRepaid, ColumnName = "Погасен" },
				e => new ExcelTableTuple { CellItem = e.MonthlyPayment, ColumnName = "Месечна вноска" },
				e => new ExcelTableTuple { CellItem = e.RepaidMonthlyPrincipal, ColumnName = "Погасена месечна вноска - главница" },
				e => new ExcelTableTuple { CellItem = e.RepaidMonthlyInterest, ColumnName = "Погасена месечна вноска - лихва" });

			return new FileStreamResult(excelStream, MimeTypeHelper.GetExtensionWithMime(MimeTypeHelper.OOXML_EXCEL).MimeType) { FileDownloadName = "Applications.xlsx" };
		}
	}
}
