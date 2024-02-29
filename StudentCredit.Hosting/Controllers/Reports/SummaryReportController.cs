using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Application.Reports.Dtos.SummaryReports;
using StudentCredit.Application.Reports.Enums;
using StudentCredit.Application.Reports.Interfaces;
using StudentCredit.Data.Users.Constants;
using StudentCredit.Infrastructure.Utils;

namespace StudentCredit.Hosting.Controllers.Reports
{
    [ApiController]
	[Route("api/[controller]")]
	public class SummaryReportController : Controller
	{
		private readonly RoleService roleService;
		private readonly ISummaryReportService summaryReportService;

		public SummaryReportController(
			RoleService roleService,
			ISummaryReportService summaryReportService
			)
		{
			this.roleService = roleService;
			this.summaryReportService = summaryReportService;
		}

		[HttpGet("summaryReport")]
		public async Task<SheetDto> GetSummaryReport([FromQuery] int yearId)
		{
			this.roleService.ValidateRoleException(UserRoleAliases.ADMINISTRATOR);

			return await this.summaryReportService.GetSummaryReport(yearId);
		}

		[HttpGet("reportByBank")]
		public async Task<SheetDto> GetSummaryReportByBank([FromQuery] int yearId, [FromQuery] int bankId)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

			return await this.summaryReportService.GetSummaryReportByBank(yearId, bankId);
        }

        [HttpPost("importExcel")]
		[Consumes("multipart/form-data")]
		public async Task ImportSummaryReportFromExcel([FromForm] IFormFile file)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

			await this.summaryReportService.ImportSummaryReportFromExcel(file);
        }

        [HttpPut("monthData")]
		public async Task<SheetDto> UpdateMonthData([FromQuery] int sheetId, [FromBody] MonthDataDto dto)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

			return await this.summaryReportService.UpdateMonthData(sheetId, dto);
        }

        [HttpPut("yearLimit")]
		public Task<SheetDto> ChangeYearLimit([FromQuery] int yearId, [FromBody] decimal? yearLimit)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            if (this.roleService.ValidateRole(UserRoleAliases.BANK_ACTIVE))
			{
				return this.summaryReportService.ChangeBankYearLimit(yearId, yearLimit);
			}
			else
			{
				this.roleService.ValidateRoleException(UserRoleAliases.ADMINISTRATOR);

				return this.summaryReportService.ChangeYearLimit(yearId, yearLimit);
			}
		}

		[HttpPost("SummaryReportExcel")]
		public async Task<FileStreamResult> ExportInExcel([FromBody] SummaryReportSearchDto dto)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

			var sheets = await this.summaryReportService.GetAllSheets(dto);

            var excelStream = this.summaryReportService.ExportInExcel(sheets);

			return new FileStreamResult(excelStream, MimeTypeHelper.GetExtensionWithMime(MimeTypeHelper.OOXML_EXCEL).MimeType) { FileDownloadName = "Report.xlsx" };
		}
	}
}
