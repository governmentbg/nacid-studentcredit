using StudentCredit.Application.Common.Dtos;
using StudentCredit.Application.Users.Dtos;
using StudentCredit.Data.Users.Enums;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Users.Interfaces
{
    public interface IUserService : ITransientService
	{
        Task<SearchResultItemDto<UserSearchResultDto>> SearchUsers(UserSearchFilterDto filter, CancellationToken cancellationToken);
        Task CreateUser(UserDto model, CancellationToken cancellationToken);
        Task<UserDto> GetUserById(int userId, CancellationToken cancellationToken);
        Task EditUserData(UserDto dto, CancellationToken cancellationToken);
        Task ChangeUserPassword(ChangePasswordDto dto, CancellationToken cancellationToken);
        Task<UserStatus> ChangeUserStatus(int id, CancellationToken cancellationToken);
        void ValidateUserBank(int bankId);

	}
}
