namespace Nancy.Bootstrappers.Autofac
{
    using global::Autofac;
    using global::Autofac.Core.Lifetime;
    using Nancy.Core;
    using Nancy.Core.Http;
    using Nancy.Core.Registration;

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

        protected sealed override void ValidateContainerConfiguration(ILifetimeScope container)
        {
            // Not supported.
        }

        protected sealed override IApplication<ILifetimeScope> CreateApplication(Disposable<ILifetimeScope> lifetimeScope)
        {
            return new Application(lifetimeScope);
        }

        private sealed class Application : Application<ILifetimeScope>
        {
            public Application(Disposable<ILifetimeScope> lifetimeScope) : base(lifetimeScope)
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
