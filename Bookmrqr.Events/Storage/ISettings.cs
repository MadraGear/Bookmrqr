namespace Bookmrqr.Events.Storage
{
    public interface ISettings
    {
        string ConnectionString { get; set; }
        string EventStoreAddress { get; set; }
        string EventStorePort { get; set; }
        string EventStoreUserName { get; set; }
        string EventStorePassword { get; set; }
    }
}
