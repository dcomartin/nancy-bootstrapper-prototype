using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Nancy.Bootstrapper.Prototype
{
    public abstract class Application<TContainer, TScope> : IApplication
        where TContainer : IDisposable
        where TScope : IDisposable
    {
        protected Application(TContainer container)
        {
            Container = container;
        }

        private TContainer Container { get; }

        public async Task HandleRequest(HttpContext context, CancellationToken cancellationToken)
        {
            using (var scope = BeginRequestScope(Container))
            {
                var engine = ComposeEngine(Container, scope);

                await engine.HandleRequest(context, cancellationToken).ConfigureAwait(false);
            }
        }

        public void Dispose()
        {
            Container.Dispose();
        }

        protected abstract TScope BeginRequestScope(TContainer container);

        protected abstract IEngine ComposeEngine(TContainer container, TScope scope);
    }

    public abstract class Application<TContainer> : Application<TContainer, TContainer>
        where TContainer : IDisposable
    {
        protected Application(TContainer container) : base(container)
        {
        }

        protected sealed override IEngine ComposeEngine(TContainer container, TContainer scope)
        {
            return ComposeEngine(scope);
        }

        protected abstract IEngine ComposeEngine(TContainer container);
    }
}