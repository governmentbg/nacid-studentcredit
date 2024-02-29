using FileStorageNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using StudentCredit.Application;
using StudentCredit.Application.Infrastructure.AutoMapperProfiles;
using StudentCredit.Hosting.BackgroundServices.RabbitMq.Configurations;
using StudentCredit.Hosting.ConfigurationExtensions;
using StudentCredit.Hosting.DIExtensions;
using StudentCredit.Infrastructure.BackgroundServices;
using StudentCredit.Infrastructure.ConfigurationExtensions;
using StudentCredit.Infrastructure.ConfigurationExtensions.Models;
using StudentCredit.Infrastructure.DIExtensions;
using StudentCredit.Infrastructure.Interfaces.Contexts;
using StudentCredit.Infrastructure.Middlewares;
using StudentCredit.Persistence;
using StudentCredit.Persistence.Extensions;

namespace StudentCredit.Hosting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Services.ConfigureApplicationConfiguration(builder.Environment);

            builder.Services
                .ConfigureHostingConfiguration(configuration);

            builder.Services
                .AddControllers(options =>
                {
                    options.OutputFormatters.Add(new HttpNoContentOutputFormatter());
                    options.Filters.Add(new ProducesAttribute("application/json"));
                })
                .AddJson();

            builder.Services
                .AddAutoMapper(typeof(ApplicationOneProfile));

            builder.Services
                .AddPersistence<IAppDbContext, AppDbContext>(configuration.GetSection("DbConfiguration:ConnectionString").Value, builder.Environment.IsDevelopment())
                .AddPersistence<IAppLogContext, AppLogContext>(configuration.GetSection("DbConfiguration:LogConnectionString").Value, builder.Environment.IsDevelopment())
				.AddPersistence<IRdpzsdDbContext, RdpzsdDbContext>(configuration.GetSection("DbConfiguration:RdpzsdConnectionString").Value, builder.Environment.IsDevelopment())
				.AddFileStorage(configuration.GetSection("DbConfiguration:Descriptors"), configuration.GetSection("DbConfiguration:Encryption"));

            builder.Services
                .AddApplicationServices()
                .AddCommonServices();

            builder.Services
                .ConfigureJwtAuthService(configuration);

            var summaryReportJobConfig = configuration.GetSection("SummaryReportJobConfiguration").Get<SummaryReportJobConfiguration>();
            if (summaryReportJobConfig.JobEnabled)
            {
				builder.Services
				    .ConfigureQuartz(summaryReportJobConfig);
			}

            var emailConfiguration = configuration.GetSection("EmailConfiguration").Get<EmailsConfiguration>();
            if (emailConfiguration.JobEnabled)
            {
                builder.Services
                    .AddHostedService<EmailJob>();
            }

			var rndConsumerConfiguration = configuration.GetSection("RndConsumerConfiguration").Get<RndConsumerConfiguration>();
			if (rndConsumerConfiguration.IsConsumerEnabled)
			{
				builder.Services.AddConsumers();
			}

			var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMiddleware<RedirectionMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseRouting();

            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                {
                    if (context.File.Name == "index.html")
                    {
                        context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                        context.Context.Response.Headers.Add("Expires", "-1");
                    }
                }
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints
               .MapControllers()
               .RequireAuthorization()
           );

            app.Run();
        }
    }
}