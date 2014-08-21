using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manufacturing.Framework.Logging
{
    [TestClass]
    public class LoggingUtilsTests
    {
        [TestMethod]
        public void CheckLogging()
        {
            LoggingUtils.InitializeLogging();
        }
    }
}
