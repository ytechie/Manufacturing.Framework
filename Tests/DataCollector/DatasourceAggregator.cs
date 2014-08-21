using System;
using System.Collections.Generic;
using Manufacturing.DataCollector;
using Manufacturing.Framework.Datasource;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Manufacturing.Framework.DataCollector
{
    [TestClass]
    public class DatasourceAggregatorTests
    {
        [TestMethod]
        public void RaiseEventsFromAllChildren()
        {
            var ds1 = new Mock<IDatasource>();
            var ds2 = new Mock<IDatasource>();

            var agg = new DatasourceAggregator(new List<IDatasource> { ds1.Object, ds2.Object });
            var argList = new List<DataReceivedEventArgs<decimal>>();
            agg.DataReceived += (sender, args) => argList.Add(args);

            ds1.Raise(x => x.DataReceived += null, new DataReceivedEventArgs<decimal>(5, 3, DateTime.Now));
            ds2.Raise(x => x.DataReceived += null, new DataReceivedEventArgs<decimal>(6, 4, DateTime.Now));

            Assert.AreEqual(2, argList.Count);
            Assert.AreEqual(5, argList[0].Value);
            Assert.AreEqual(6, argList[1].Value);
            Assert.AreEqual(3, argList[0].DeviceID);
            Assert.AreEqual(4, argList[1].DeviceID);
        }

        [TestMethod]
        public void StartReadCallsChildren()
        {
            var ds1 = new Mock<IDatasource>();
            var ds2 = new Mock<IDatasource>();

            var agg = new DatasourceAggregator(new List<IDatasource> { ds1.Object, ds2.Object });

            agg.StartRead();

            ds1.Verify(x => x.StartRead(), Times.Once());
            ds2.Verify(x => x.StartRead(), Times.Once());
        }
    }
}
