using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.Users.Dtos;
using StudentCredit.Application.Users.Interfaces;
using StudentCredit.Infrastructure.Interfaces.Contexts;

namespace StudentCredit.Hosting.Controllers.Users
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class ForgottenPasswordController : ControllerBase
    {
        private readonly IForgottenPasswordService forgottenPasswordService;

        public ForgottenPasswordController(IForgottenPasswordService forgottenPasswordService)
        {
            this.forgottenPasswordService = forgottenPasswordService;
        }

        [HttpPost]
        public async Task SendForgottenPasswordMail([FromBody] EmailForgottenPasswordDto model, CancellationToken cancellationToken)
            => await this.forgottenPasswordService.SendMail(model, cancellationToken);

        [HttpPost("Recovery")]
        public async Task RecoverPassword([FromBody] ForgottenPasswordDto model, CancellationToken cancellationToken)
            => await this.forgottenPasswordService.RecoverPassword(model, cancellationToken);
    }
}