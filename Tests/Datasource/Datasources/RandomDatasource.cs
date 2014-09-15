using Manufacturing.DataCollector;
using Manufacturing.DataCollector.Datasources.Simulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manufacturing.Framework.Datasource.Datasources
{
    [TestClass]
    public class RandomDatasourceTests
    {
        [TestMethod]
        public void StartRead_CreatesValue()
        {
            var config = new DataCollectorConfiguration
            {
                RandomDatasourceMin = 10,
                RandomDatasourceMax = 20
            };
            var r = new RandomDatasource(config);

            DataReceivedEventArgs<decimal> args = null;
            r.DataReceived += (s, a) => args = a;
            r.StartRead();

            Assert.IsTrue(args.Value > 10);
            Assert.IsTrue(args.Value < 20);
        }
    }
}
