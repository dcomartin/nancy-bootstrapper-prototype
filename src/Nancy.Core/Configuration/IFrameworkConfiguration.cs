namespace Nancy.Bootstrapper.Prototype.Configuration
{
    public interface IFrameworkConfiguration
    {
        ITypeRegistrationFactory<IEngine> Engine { get; }

        ICollectionTypeRegistrationFactory<ISerializer> Serializers { get; }
    }
}