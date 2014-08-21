using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manufacturing.Framework.Configuration
{
    [TestClass]
    public class ServiceBusQueueInformationTests
    {
        [TestMethod]
        public void GetConnectionString()
        {
            var s = new ServiceBusQueueInformation
            {
                Namespace = "a",
                SharedAccessKeyName = "b",
                SharedAccessKey = "c"
            };

            Assert.AreEqual("endpoint=sb://a.servicebus.windows.net/;SharedAccessKeyName=b;SharedAccessKey=c", s.GetConnectionString());
        }
    }
}
