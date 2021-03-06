﻿namespace Nancy.Core.Tests.Fakes
{
    using Nancy.Core.Registration;

    public class FakeBootstrapper : Bootstrapper<FakeContainer>
    {
        protected override void Register(FakeContainer builder, IContainerRegistry registry)
        {
        }

        protected override void ValidateContainerConfiguration(FakeContainer container)
        {
        }

        protected override IApplication<FakeContainer> CreateApplication(Disposable<FakeContainer> container)
        {
            return null;
        }

        protected override FakeContainer CreateContainer()
        {
            return null;
        }
    }
}
