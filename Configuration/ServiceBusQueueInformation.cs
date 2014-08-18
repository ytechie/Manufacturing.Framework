namespace Manufacturing.Framework.Configuration
{
    public class ServiceBusQueueInformation
    {
        public string Namespace { get; set; }
        public string SharedAccessKeyName { get; set; }
        public string SharedAccessKey { get; set; }
        public string QueueName { get; set; }

        public string GetConnectionString()
        {
            return string.Format("endpoint=sb://{0}.servicebus.windows.net/;SharedAccessKeyName={1};SharedAccessKey={2}",
                Namespace, SharedAccessKeyName, SharedAccessKey);
        }
    }
}
