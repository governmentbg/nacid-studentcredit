using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Public.Hosting.Extensions;
using ProxyKit;
using Public.Hosting.Middlewares;
using Public.Hosting.Models;

namespace Public.Hosting 
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Services.ConfigureApplicationConfiguration(builder.Environment);

            // Add services to the container.

            builder.Services
               .AddControllers(options =>
               {
                   options.OutputFormatters.Add(new HttpNoContentOutputFormatter());
                   options.Filters.Add(new ProducesAttribute("application/json"));
               })
               .AddJson();

            builder.Services
              .AddProxy()
              .AddControllers()
              ;


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            var proxyPaths = configuration.GetSection("ProxyPaths").Get<ProxyPath>();
            app.ConfigureProxy(proxyPaths);

            app.UseMiddleware<RedirectionMiddleware>();

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

            app.UseEndpoints(endpoints => endpoints.MapControllers());
            app.Run();
        }
    }
}