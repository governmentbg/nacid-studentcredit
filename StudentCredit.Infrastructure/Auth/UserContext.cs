using Microsoft.AspNetCore.Http;
using StudentCredit.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudentCredit.Infrastructure.Auth
{
    public class UserContext : IUserContext
    {
        private ClaimsPrincipal _user;

        public int UserId => int.Parse(_user.Claims.Single(c => c.Type.Equals(JwtRegisteredClaimNames.Jti)).Value);
        public string Username => _user.Claims.Single(c => c.Type.Equals("username")).Value;
        public string Role => _user.Claims.Single(c => c.Type == ClaimTypes.Role).Value;
        public int? BankId => int.Parse(_user.Claims.Single(c => c.Type.Equals("bankId")).Value);

		public UserContext(IHttpContextAccessor contextAccessor)
        {
            this._user = contextAccessor.HttpContext?.User;
        }
    }
}
