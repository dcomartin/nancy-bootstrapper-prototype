﻿namespace Nancy.AspNet
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Nancy.Bootstrappers.AspNet;
    using Nancy.Core;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNancy(this IApplicationBuilder builder)
        {
            // We need to wrap the default ApplicationServices
            // because IServiceProvider is not implementing IDisposable.
            var provider = builder.ApplicationServices.AsDisposable(shouldDispose: false);

            // We assume that the user has already called `AddNancy`
            // in `ConfigureServices`, otherwise this will fail.
            var bootstrapper = provider.GetRequiredService<IBootstrapper<IDisposableServiceProvider>>();

            var application = bootstrapper.InitializeApplication(provider);

            return builder.UseMiddleware<NancyMiddleware>(application);
        }

        private class NancyMiddleware
        {
            private readonly RequestDelegate next;

            private readonly IApplication application;

            public NancyMiddleware(RequestDelegate next, IApplication application)
            {
                this.next = next;
                this.application = application;
            }

            public Task Invoke(HttpContext context)
            {
                var httpContext = new AspNetHttpContext(context);

                // Add the request services so we can get it out in the bootstrapper.
                httpContext.Items.Add(Constants.AspNetRequestServices, context.RequestServices);

                var cancellationToken = context.RequestAborted;

                return this.application.HandleRequest(httpContext, cancellationToken);
            }
        }
    }
}
