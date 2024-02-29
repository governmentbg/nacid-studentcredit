using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StudentCredit.Infrastructure.DomainValidation;
using StudentCredit.Infrastructure.Interfaces.Registration;
using Scrutor;

namespace StudentCredit.Infrastructure.DIExtensions
{
	public static class CommonDiExtension
	{
		public static IServiceCollection AddCommonServices(this IServiceCollection services)
		{
			services.Scan(scan => scan
				.FromCallingAssembly()

				.AddClasses(classes => classes.AssignableTo<IScopedService>())
				.AsMatchingInterface()
				.WithScopedLifetime());

			services
				.Scan(scan => scan
				.AddType<DomainValidationService>()
				.AsSelf()
				.WithScopedLifetime());

			services
				.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

			return services;
		}
	}
}
