using FileStorageNetCore;
using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.Applications.ApplicationsOne;
using StudentCredit.Application.Applications.ApplicationsOne.Dtos;
using StudentCredit.Application.Applications.Common.Dtos;
using StudentCredit.Application.Common.Constants;
using StudentCredit.Application.Common.Interfaces;
using StudentCredit.Application.Credits.Dtos;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Common.Enums;
using StudentCredit.Data.Users.Constants;
using StudentCredit.Hosting.Controllers.Common;
using System.Xml.Serialization;

namespace StudentCredit.Hosting.Controllers.Applications
{
	public class ApplicationOneController : BaseApplicationController<ApplicationOneDto>
	{
		private readonly IApplicationOneService service;
        private readonly IGeneratePdfService generatePdfService;
        private readonly RoleService roleService;

		public ApplicationOneController(
			IApplicationOneService service, 
			RoleService roleService,
            IGeneratePdfService generatePdfService

            )
			: base(service, roleService, ApplicationType.ApplicationOne)
		{
			this.service = service;
			this.roleService = roleService;
			this.generatePdfService = generatePdfService;
		}

        [HttpGet("getByState")]
		public async Task<List<CreditSearchResultItemDto>> GetByState([FromQuery] CommitState state)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

			return await this.service.GetByState<CreditSearchResultItemDto>(state);
        }

        [HttpGet("selectCredit")]
		public async Task<ApplicationOneCreditSelectDto> SelectCredit([FromQuery] CreditSelectFilterDto dto)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

			return await this.service.SelectCredit<ApplicationOneCreditSelectDto>(dto);
        }

		[HttpPost("xml")]
		public async Task<ApplicationOneDto> ImportFromXml([FromBody] XmlApplicationImportDto xml)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            using (StringReader sr = new StringReader(xml.DocumentXml))
			{
				var serializer = new XmlSerializer(typeof(ApplicationOneXmlImportDto));
				var importDto = (ApplicationOneXmlImportDto)serializer.Deserialize(sr);

				return await this.service.MapXmlToApplicationOneDto(importDto.Prilojenie1);
			}
		}

        [HttpPost("PDF")]
        public async Task<FileContentResult> ExportPdf([FromBody] ApplicationOneDto applicationOneDto)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            var applicationOnePdfExportDto = this.service.GetPdfExportDto(applicationOneDto);

            var bytes = await this.generatePdfService.GeneratePdf(applicationOnePdfExportDto, FileTemplateAliases.APPLICATION_ONE_EXPORT, true);

            return new FileContentResult(bytes, MimeTypeHelper.GetMimeType(MimeTypeHelper.MIME_APPLICATION_PDF)) { FileDownloadName = "ApplicationOne.pdf" };
        }
    }
}