using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.Common.Dtos;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Application.Reports.Dtos.FulfillmentOfLimits;
using StudentCredit.Application.Reports.Dtos.Reports;
using StudentCredit.Application.Reports.Interfaces;
using StudentCredit.Data.Users.Constants;
using StudentCredit.Infrastructure.Utils;

namespace StudentCredit.Hosting.Controllers.Reports
{
    [ApiController]
	[Route("api/[controller]")]
	public class ReportController : Controller
	{
		private readonly IReportService reportService;
		private readonly RoleService roleService;
		private readonly IFulfillmentOfLimitsService fulfillmentOfLimitsService;

		public ReportController(
			IReportService reportService, 
			RoleService roleService,
			IFulfillmentOfLimitsService fulfillmentOfLimitsService
			)
		{
			this.reportService = reportService;
			this.roleService = roleService;
			this.fulfillmentOfLimitsService = fulfillmentOfLimitsService;
		}

		[HttpGet]
		public async Task<SearchResultItemDto<ReportSearchResultItemDto>> GetReportsFiltered([FromQuery] SearchReportFilter filter)
		{
            this.roleService.ValidateRoleException(UserRoleAliases.ADMINISTRATOR);

            return await reportService.GetReportSearchResultItem(filter);
        }

        [HttpPost("Excel")]
		public FileStreamResult ExportReportsFiltered([FromBody] SearchReportFilter filter)
		{
            this.roleService.ValidateRoleException(UserRoleAliases.ADMINISTRATOR);

			var items = reportService.GetReportSearchResultItem(filter).GetAwaiter().GetResult();

			var excelStream = reportService.ExportReportsExcel(items.Items, filter);

            return new FileStreamResult(excelStream, MimeTypeHelper.GetExtensionWithMime(MimeTypeHelper.OOXML_EXCEL).MimeType) { FileDownloadName = "Reports.xlsx" };
        }

		[HttpGet("fulfillmentOfLimits")]
		public async Task<List<FulfillmentOfLimitsReportDto>> GetFulfillmentLimits()
			=> await this.fulfillmentOfLimitsService.Get();
	}
}