using Autofac;
using Autofac.Core.Lifetime;
using Microsoft.AspNet.Http;
using Nancy.Core;
using Nancy.Core.Registration;

namespace Nancy.Bootstrappers.Autofac
{
    public class AutofacBootstrapper : Bootstrapper<ContainerBuilder, ILifetimeScope>
    {
        protected sealed override ContainerBuilder CreateBuilder()
        {
            return new ContainerBuilder();
        }

        protected sealed override void Register(ContainerBuilder builder, IContainerRegistry registry)
        {
            builder.Register(registry);
        }

        protected sealed override ILifetimeScope BuildContainer(ContainerBuilder builder)
        {
            return builder.Build();
        }

        protected sealed override IApplication CreateApplication(ILifetimeScope lifetimeScope)
        {
            return new Application(lifetimeScope);
        }

        private sealed class Application : Application<ILifetimeScope>
        {
            public Application(ILifetimeScope lifetimeScope) : base(lifetimeScope)
            {
            }

            protected override ILifetimeScope BeginRequestScope(HttpContext context, ILifetimeScope lifetimeScope)
            {
                return lifetimeScope.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }

            protected override IEngine ComposeEngine(ILifetimeScope lifetimeScope)
            {
                return lifetimeScope.Resolve<IEngine>();
            }
        }
    }
}