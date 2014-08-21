using Bootstrap;
using Bootstrap.StructureMap;
using Microsoft.ConventionConfiguration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manufacturing.Framework.Configuration
{
    [TestClass]
    public class ConfigurationLoaderTests
    {
        [TestMethod]
        public void LoadLocalDummyConfigurationWithFrameworkConfiguration_VerifyRegistration()
        {
            var container = new StructureMap.Container();
            ConfigurationLoader.LoadConfigurations(container, ".\\Configuration\\", "{0}Configuration");

            Bootstrapper.With.StructureMap();

            var config = container.GetInstance<DummyConfiguration>();
            Assert.IsNotNull(config);
            Assert.AreEqual(1234, config.Port);
            Assert.AreEqual("DataCollectorQueue", config.MsmqQueueName);
        }
    }


}
