﻿namespace Nancy.Bootstrappers.AspNet
{
    using System;

    internal static class ServiceProviderExtensions
    {
        public static IDisposableServiceProvider AsDisposable(this IServiceProvider provider, bool shouldDispose)
        {
            return new DisposableServiceProvider(provider, shouldDispose);
        }

        private class DisposableServiceProvider : IDisposableServiceProvider
        {
            private readonly IServiceProvider provider;

            private readonly bool shouldDispose;

            public DisposableServiceProvider(IServiceProvider provider, bool shouldDispose)
            {
                this.provider = provider;
                this.shouldDispose = shouldDispose;
            }

            public object GetService(Type serviceType)
            {
                return this.provider.GetService(serviceType);
            }

            public void Dispose()
            {
                if (this.shouldDispose)
                {
                    (this.provider as IDisposable)?.Dispose();
                }
            }
        }
    }
}
