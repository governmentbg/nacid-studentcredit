﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using ProxyKit;
using Public.Hosting.Models;

namespace Public.Hosting.Extensions
{
	public static class ProxyExtension
    {
        public static IApplicationBuilder ConfigureProxy(this IApplicationBuilder app, ProxyPath proxyPath)
        {
            var options = new RewriteOptions()
                    .AddRewrite(@"api/(.*)", "api/$1", skipRemainingRules: true);

            app.UseWhen(
                context => context.Request.Path.Value.StartsWith("/api/public/"),
                builder => builder
                .UseRewriter(options)
                .RunProxy(context => context
                    .ForwardTo(proxyPath.Server)
                    .AddXForwardedHeaders()
                    .Send()
            ));

            return app;
        }
    }
}
