using System;
using System.IO;
using Manufacturing.Framework.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Manufacturing.Framework.Datasource
{
    [TestClass]
    public class EventTests
    {
        [TestMethod]
        public void Deserialize_VerifyEquivalent()
        {
            var dr = new EventRecord
            {
                Timestamp = DateTime.Parse("2014-01-20 8:00am"),
                MetaData = new Dictionary<int, byte[]> {{1, new byte[]{1,2,3}}}
            };

            var serializer = new DatasourceRecordSerializer();
            var ms = new MemoryStream();
            serializer.Serialize(ms, new List<DatasourceRecord> { dr });
            ms.Position = 0;
            var records = serializer.Deserialize(ms);
            var deserialized = records[0] as EventRecord;

            dr.Equivalent(deserialized);
        }


        [TestMethod]
        public void Metadata_NotNullByDefault()
        {
            var er = new EventRecord();
            Assert.IsNotNull(er.MetaData);
        }

        [TestMethod]
        public void Equivalent_SameMetaData()
        {
            var dr = new EventRecord { MetaData = new Dictionary<int, byte[]> { { 1, new byte[] { 1, 2, 3 } } } };
            var dr2 = new EventRecord { MetaData = new Dictionary<int, byte[]> { { 1, new byte[] { 1, 2, 3 } } } };

            Assert.IsTrue(dr.Equivalent(dr2));
            Assert.IsTrue(dr2.Equivalent(dr));
        }

        [TestMethod]
        public void Equivalent_SameMetaDataInstance()
        {
            var dr = new EventRecord { MetaData = new Dictionary<int, byte[]> { { 1, new byte[] { 1, 2, 3 } } } };
            var dr2 = new EventRecord { MetaData = dr.MetaData };

            Assert.IsTrue(dr.Equivalent(dr2));
            Assert.IsTrue(dr2.Equivalent(dr));
        }

        [TestMethod]
        public void Equivalent_DifferentMetaData()
        {
            var dr = new EventRecord { MetaData = new Dictionary<int, byte[]> { { 1, new byte[] { 1, 2, 3 } } } };
            var dr2 = new EventRecord { MetaData = new Dictionary<int, byte[]> { { 1, new byte[] { 4, 5, 6 } } } };

            Assert.IsFalse(dr.Equivalent(dr2));
            Assert.IsFalse(dr2.Equivalent(dr));
        }

        //[TestMethod]
        //public void Equivalent_SameEventType()
        //{
        //    var se = new EventRecord {EventType = 5};
        //    var se2 = new EventRecord { EventType = 5 };

        //    Assert.IsTrue(se.Equivalent(se2));
        //    Assert.IsTrue(se2.Equivalent(se));
        //}

        //[TestMethod]
        //public void Equivalent_DifferentEventType()
        //{
        //    var se = new EventRecord { EventType = 5 };
        //    var se2 = new EventRecord { EventType = 6 };

        //    Assert.IsFalse(se.Equivalent(se2));
        //    Assert.IsFalse(se2.Equivalent(se));
        //}
    }
}
