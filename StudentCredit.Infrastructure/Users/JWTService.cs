using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentCredit.Infrastructure.ConfigurationExtensions.Models;
using StudentCredit.Infrastructure.Users.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentCredit.Infrastructure.Users
{
    public class JWTService : IJWTService
    {
        private readonly AuthConfiguration authConfiguration;

        public JWTService(
            IOptions<AuthConfiguration> options
            )
        {
            this.authConfiguration = options.Value;
        }

        public string CreateToken(int userId, string username, string roleAlias, int? bankId)
        {
            var claims = new List<Claim> {
                    new Claim("username", username),
                    new Claim(JwtRegisteredClaimNames.Jti, userId.ToString()),
                    new Claim(ClaimTypes.Role, roleAlias),
                    new Claim("bankId", bankId.ToString())
				};

            var expires = DateTime.Now.AddHours(authConfiguration.ValidHours);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                authConfiguration.Issuer,
                authConfiguration.Audience,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            string tokenString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return tokenString;
        }
    }
}
