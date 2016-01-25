namespace Nancy.Core.Configuration
{
    public interface IApplicationConfiguration
    {
        IFrameworkConfiguration Framework { get; }
    }

    public interface IApplicationConfiguration<out TContainer> : IApplicationConfiguration
    {
        TContainer Container { get; }
    }
}