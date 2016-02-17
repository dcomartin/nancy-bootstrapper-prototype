namespace Nancy.Core.Configuration
{
    using System;
    using System.Linq;
    using Nancy.Core.Registration;
    using Nancy.Core.Scanning;

    public class TypeRegistrationFactory<TService, TDefaultImplementation> : ITypeRegistrationFactory<TService>
    {
        private readonly Lifetime lifetime;

        private TypeRegistration registration;

        public TypeRegistrationFactory(Lifetime lifetime)
        {
            this.lifetime = lifetime;
        }

        public void Use<TImplementation>() where TImplementation : TService
        {
            this.Use(typeof(TImplementation));
        }

        public void Use(Type implementationType)
        {
            this.registration = new TypeRegistration(typeof(TService), implementationType, this.lifetime);
        }

        public TypeRegistration GetRegistration(ITypeCatalog typeCatalog)
        {
            return this.registration
                ?? ScanForCustomRegistration(typeCatalog, this.lifetime)
                    ?? GetDefaultRegistration(this.lifetime);
        }

        private static TypeRegistration ScanForCustomRegistration(ITypeCatalog typeCatalog, Lifetime lifetime)
        {
            // TODO: Throw on multiple results?
            var customImplementationType = typeCatalog
                .GetTypesAssignableTo<TService>(ScanningStrategies.ExcludeNancy)
                .FirstOrDefault();

            if (customImplementationType != null)
            {
                return new TypeRegistration(typeof(TService), customImplementationType, lifetime);
            }

            return null;
        }

        private static TypeRegistration GetDefaultRegistration(Lifetime lifetime)
        {
            return new TypeRegistration(typeof(TService), typeof(TDefaultImplementation), lifetime);
        }
    }
}
