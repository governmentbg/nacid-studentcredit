using FileStorageNetCore;
using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.Applications.ApplicationsFour;
using StudentCredit.Application.Applications.ApplicationsFour.Dtos;
using StudentCredit.Application.Applications.Common.Dtos;
using StudentCredit.Application.Common.Constants;
using StudentCredit.Application.Common.Interfaces;
using StudentCredit.Application.Credits;
using StudentCredit.Application.Credits.Dtos;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Data.Applications.ApplicationOne;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Common.Enums;
using StudentCredit.Data.Users.Constants;
using StudentCredit.Hosting.Controllers.Common;

namespace StudentCredit.Hosting.Controllers.Applications
{
    public class ApplicationFourController : BaseApplicationController<ApplicationFourDto>
    {
        private readonly IApplicationFourService service;
        private readonly RoleService roleService;
        private readonly IGeneratePdfService generatePdfService;
		private readonly ICreditService creditService;

		public ApplicationFourController(
            IApplicationFourService service,
            RoleService roleService,
            IGeneratePdfService generatePdfService,
            ICreditService creditService
            )
            : base(service, roleService, ApplicationType.ApplicationFour)
        {
            this.service = service;
            this.roleService = roleService;
            this.generatePdfService = generatePdfService;
			this.creditService = creditService;
		}

        [HttpGet("getByState")]
        public async Task<List<CreditSearchResultItemDto>> GetByState([FromQuery] CommitState state)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            return await this.service.GetByState<CreditSearchResultItemDto>(state);
        }

        [HttpGet("selectCredit")]
		public async Task<CreditSelectDto> SelectCredit([FromQuery] CreditSelectFilterDto dto)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            var credit = await this.service.SelectCredit<CreditSelectDto>(dto);

			await this.service.CheckApplicationFourAlreadyInProcess(credit.CreditNumber, credit.Bank.Id);

			return credit;
		}

        [HttpPost("{applicationFourId:int}/deny")]
        public async Task<ApplicationFourDto> DenyApplicationFour([FromRoute] int applicationFourId, [FromQuery] string changeStateDescription)
        {
            this.roleService.ValidateRoleException(UserRoleAliases.ADMINISTRATOR);

            await this.service.ChangeApplicationState(applicationFourId, CommitState.Denied, changeStateDescription);

            return await this.service.GetById(applicationFourId);
        }

		public override async Task<ApplicationFourDto> Approve([FromRoute] int applicationId)
		{
			this.roleService.ValidateRoleException(UserRoleAliases.ADMINISTRATOR);

			await this.service.ChangeApplicationState(applicationId, CommitState.Approved, null);

            var appFour = await this.service.GetById(applicationId);

			await this.creditService.PayCreditByApplicationFour(appFour.Bank.Id, appFour.CreditType, appFour.CreditNumber, appFour.Uin);

            return appFour;
		}

		[HttpPost("PDF")]
        public async Task<FileContentResult> ExportPdf([FromBody] ApplicationFourDto applicationFourDto)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            var applicationFourPdfExportDto = this.service.GetPdfExportDto(applicationFourDto);

            var bytes = await this.generatePdfService.GeneratePdf(applicationFourPdfExportDto, FileTemplateAliases.APPLICATION_FOUR_EXPORT, true);

            return new FileContentResult(bytes, MimeTypeHelper.GetMimeType(MimeTypeHelper.MIME_APPLICATION_PDF)) { FileDownloadName = "ApplicationFour.pdf" };
        }
    }
}