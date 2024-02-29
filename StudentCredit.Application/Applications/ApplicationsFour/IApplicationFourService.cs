using StudentCredit.Application.Applications.ApplicationsFour.Dtos;
using StudentCredit.Application.Applications.Common.Interfaces;

namespace StudentCredit.Application.Applications.ApplicationsFour
{
    public interface IApplicationFourService : IApplicationService<ApplicationFourDto>
	{
        Task CheckApplicationFourAlreadyInProcess(string creditNumber, int bankId);

        ApplicationFourPdfExportDto GetPdfExportDto(ApplicationFourDto applicationFourDto);
    }
}
