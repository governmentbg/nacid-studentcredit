using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.Users.Dtos;
using StudentCredit.Application.Users.Interfaces;

namespace StudentCredit.Hosting.Controllers.Users
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        public async Task<UserLoginInfoDto> LoginUser([FromBody] UserCredentialsDto model, CancellationToken cancellationToken)
            => await this.loginService.Login(model, cancellationToken);
    }
}
