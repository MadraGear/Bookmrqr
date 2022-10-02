namespace Bookmrqr.Events.Storage
{
    public class Settings : ISettings
    {
        public string EventStoreAddress { get; set; }
        public string EventStorePort { get; set; }
        public string EventStoreUserName { get; set; }
        public string EventStorePassword { get; set; }
        public string ConnectionString { get; set; }
    }
}
