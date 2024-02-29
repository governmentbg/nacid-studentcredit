using Microsoft.Extensions.DependencyInjection;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.Scan(scan => scan
				.FromCallingAssembly()

				.AddClasses(classes => classes.AssignableTo<ITransientService>())
				.AsMatchingInterface()
				.WithTransientLifetime()

                .AddClasses(classes => classes.AssignableTo<IScopedService>())
                .AsMatchingInterface()
                .WithScopedLifetime()

                .AddClasses(classes => classes.AssignableTo<IScopedService>())
				.AsSelf()
				.WithScopedLifetime()
				);

			return services;
		}
	}
}
