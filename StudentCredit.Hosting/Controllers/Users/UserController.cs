using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.Common.Dtos;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Application.Users.Dtos;
using StudentCredit.Application.Users.Interfaces;
using StudentCredit.Data.Users.Constants;
using StudentCredit.Data.Users.Enums;

namespace StudentCredit.Hosting.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly RoleService roleService;

        public UserController(IUserService userService, RoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        [HttpGet]
        public async Task<SearchResultItemDto<UserSearchResultDto>> SearchUsers([FromQuery] UserSearchFilterDto filter, CancellationToken cancellationToken)
        {
            this.roleService.ValidateRoleException(UserRoleAliases.ADMINISTRATOR);

            return await this.userService.SearchUsers(filter, cancellationToken);
        }

        [HttpPost]
        public async Task Create([FromBody] UserDto user, CancellationToken cancellationToken)
        {
            this.roleService.ValidateRoleException(UserRoleAliases.ADMINISTRATOR);

            await this.userService.CreateUser(user, cancellationToken);
        }

        [HttpPut]
        public async Task UpdateUserData([FromBody] UserDto model, CancellationToken cancellationToken)
        {
            this.roleService.ValidateRoleException(UserRoleAliases.ADMINISTRATOR);

            await this.userService.EditUserData(model, cancellationToken);
        }

        [HttpGet("{userId:int}")]
        public async Task<UserDto> GetUserById([FromRoute] int userId, CancellationToken cancellationToken)
        {
            this.roleService.ValidateRoleException(UserRoleAliases.ADMINISTRATOR);

            return await this.userService.GetUserById(userId, cancellationToken);
        }

        [HttpPut("changeStatus")]
        public async Task<UserStatus> ChangeUserActivation([FromBody] int id, CancellationToken cancellationToken)
        {
            this.roleService.ValidateRoleException(UserRoleAliases.ADMINISTRATOR);

            return await this.userService.ChangeUserStatus(id, cancellationToken);
        }

        [HttpPost("NewPassword")]
        public async Task ChangePassword([FromBody] ChangePasswordDto changePasswordDto, CancellationToken cancellationToken)
        {
            await this.userService.ChangeUserPassword(changePasswordDto, cancellationToken);
        }
    }
}
