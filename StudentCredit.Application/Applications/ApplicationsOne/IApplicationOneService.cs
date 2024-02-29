using StudentCredit.Application.Applications.ApplicationsOne.Dtos;
using StudentCredit.Application.Applications.Common.Interfaces;

namespace StudentCredit.Application.Applications.ApplicationsOne
{
    public interface IApplicationOneService : IApplicationService<ApplicationOneDto>
	{
		Task<ApplicationOneDto> MapXmlToApplicationOneDto(ApplicationOneXml xmlDto);
        ApplicationOnePdfExportDto GetPdfExportDto(ApplicationOneDto applicationOneDto);
    }
}
