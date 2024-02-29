using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace StudentCredit.Persistence.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence<TDbContext, TDbContextImplementation>(this IServiceCollection services, string connectionString, bool isEnabledSensitiveDataLogging)
            where TDbContext : class
            where TDbContextImplementation : DbContext, TDbContext
        {
            services
                .AddDbContext<TDbContextImplementation>(options =>
                {
                    options.UseNpgsql(connectionString);

                    if (isEnabledSensitiveDataLogging)
                    {
                        options.EnableSensitiveDataLogging();
                    }
                });

            services
                .AddScoped<TDbContext>(provider => provider.GetRequiredService<TDbContextImplementation>());

            return services;
        }
    }
}
