using StudentCredit.Application.Applications.Common.Dtos;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Common.Enums;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Applications.Common.Interfaces
{
	public interface IApplicationService<TDto> : ITransientService
		where TDto : ApplicationCommitDto
	{
		Task<TDto> GetById(int id);

		Task Create(TDto dto);

		Task<List<TReturnDto>> GetByState<TReturnDto>(CommitState commitState);

		Task<List<ApplicationHistoryDto>> GetHistory(int rootId, ApplicationType type);

		Task<TReturnDto> SelectCredit<TReturnDto>(CreditSelectFilterDto dto)
			where TReturnDto : CreditSelectDto;

		Task CreateHistory(ApplicationHistoryDto dto, ApplicationType type);

		Task ChangeApplicationState(int applicationId, CommitState commitState, string changeStateDescription);

		Task<int> Edit(TDto dto);
	}
}
