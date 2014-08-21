using System;
using Manufacturing.DataCollector;
using Manufacturing.DataPusher;
using Manufacturing.Framework.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Manufacturing.Framework.DataPusher
{
    [TestClass]
    public class DataPusherContainerTests
    {
        private IContainer _container;

        [TestInitialize]
        public void Init()
        {
            _container = new Container();
            (new FrameworkContainer()).Register(_container);
            (new DataCollectorContainer()).Register(_container);
            (new DataPusherContainer()).Register(_container);
        }

        [TestMethod]
        public void EnsureContainerConfigDoesntBomb()
        {
            //Just a simple, empty teste to make sure our initialize is working
            _container.AssertConfigurationIsValid();
        }
    }
}
