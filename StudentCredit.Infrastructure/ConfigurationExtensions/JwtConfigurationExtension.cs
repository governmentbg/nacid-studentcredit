using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StudentCredit.Infrastructure.ConfigurationExtensions.Models;
using System.Text;

namespace StudentCredit.Infrastructure.ConfigurationExtensions
{
    public static class JwtConfigurationExtension
    {
        public static IServiceCollection ConfigureJwtAuthService(this IServiceCollection services, IConfiguration configuration)
        {
			var authConfig = configuration.GetSection("AuthConfiguration").Get<AuthConfiguration>();

			var keyByteArray = Encoding.UTF8.GetBytes(authConfig.SecretKey);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidIssuer = authConfig.Issuer,
                ValidAudience = authConfig.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = tokenValidationParameters;
                    o.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}
