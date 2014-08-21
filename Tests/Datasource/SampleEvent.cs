using System;
using System.Collections.Generic;
using System.IO;
using Manufacturing.Framework.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manufacturing.Framework.Datasource
{
    [TestClass]
    public class SampleEventTests
    {
        [TestMethod]
        public void UnitId_GetSet()
        {
            var se = new SampleEvent { UnitId = 5 };
            Assert.AreEqual(5, se.UnitId);
        }

        [TestMethod]
        public void UnitId_GetDefault_0()
        {
            var se = new SampleEvent();
            Assert.AreEqual(0, se.UnitId);
        }

        [TestMethod]
        public void Deserialize_VerifyEquivalent()
        {
            var se = new SampleEvent
            {
                Timestamp = DateTime.Parse("2014-01-20 8:00am"),
                UnitId = 6
            };

            var serializer = new DatasourceRecordSerializer();
            var ms = new MemoryStream();
            serializer.Serialize(ms, new List<DatasourceRecord> { se });
            var records = serializer.Deserialize(ms);
            var deserialized = records[0] as SampleEvent;

            se.Equivalent(deserialized);
        }

        [TestMethod]
        public void EventType_HasOne()
        {
            var se = new SampleEvent();
            Assert.AreEqual(1, se.EventType);
        }
    }
}
