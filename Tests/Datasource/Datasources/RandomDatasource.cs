using System;
using System.Collections.Generic;
using Manufacturing.DataCollector;
using Manufacturing.DataCollector.Datasources.Simulation;
using Manufacturing.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Manufacturing.Framework.Datasource.Datasources
{
    [TestClass]
    public class RandomDatasourceTests
    {
        [TestMethod]
        public void StartRead_CreatesValue()
        {
            var timer = new Mock<ITimer>();
            var config = new DataCollectorConfiguration
            {
                RandomDatasourceIntervalSeconds = 30,
                RandomDatasourceMin = 10,
                RandomDatasourceMax = 20
            };
            var r = new RandomDatasource(timer.Object, config);

            DataReceivedEventArgs<decimal> args = null;
            r.DataReceived += (s, a) => args = a;
            r.StartRead();

            Assert.IsTrue(args.Value > 10);
            Assert.IsTrue(args.Value < 20);
        }

        [TestMethod]
        public void TimerFireCreatesValue()
        {
            var timer = new Mock<ITimer>();
            var config = new DataCollectorConfiguration
            {
                RandomDatasourceIntervalSeconds = 30,
                RandomDatasourceMin = 10,
                RandomDatasourceMax = 20
            };
            var r = new RandomDatasource(timer.Object, config);

            var argsList = new List<DataReceivedEventArgs<decimal>>();
            r.DataReceived += (s, a) => argsList.Add(a);

            Assert.AreEqual(0, argsList.Count);
            timer.Raise(x => x.Tick += null, null, null);
            Assert.AreEqual(1, argsList.Count);
            timer.Raise(x => x.Tick += null, null, null);
            Assert.AreEqual(2, argsList.Count);

            Assert.IsTrue(argsList[0].Value > 10);
            Assert.IsTrue(argsList[1].Value < 20);
        }
    }
}
