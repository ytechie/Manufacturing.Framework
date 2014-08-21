using System.Linq;
using Manufacturing.DataCollector;
using Manufacturing.DataCollector.Datasources.Simulation;
using Manufacturing.Framework.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Manufacturing.Framework.DataCollector
{
    [TestClass]
    public class DataCollectorContainerTests
    {
        private IContainer _container;

        [TestInitialize]
        public void Init()
        {
            _container = new Container();
            (new FrameworkContainer()).Register(_container);
            (new DataCollectorContainer()).Register(_container);
        }

        [TestMethod]
        public void EnsureContainerConfigDoesntBomb()
        {
            //Just a simple, empty teste to make sure our initialize is working
            _container.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void GetAllDatasources()
        {
            var datasources = DataCollectorContainer.GetAllDatasources(_container).ToList();
            
            Assert.AreEqual(1, datasources.Count(x => x.GetType() == typeof(RandomDatasource)));
        }

        [TestMethod]
        public void MultipleDatasourcesInstantiated()
        {
            var datasources = DataCollectorContainer.GetAllDatasources(_container).ToList();

            Assert.IsTrue(datasources.Count() >= 3);
        }

        [TestMethod]
        public void GetDatasourceAggregator_ProperlyConfigured()
        {
            var aggregator = _container.GetInstance<DatasourceAggregator>();
        }
    }
}
