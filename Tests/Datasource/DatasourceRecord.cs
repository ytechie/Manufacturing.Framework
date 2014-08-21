using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Manufacturing.Framework.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manufacturing.Framework.Datasource
{
    [TestClass]
    public class DatasourceRecordTests
    {
        [TestMethod]
        public void EncodedDataType_GetSet()
        {
            var dr = new DatasourceRecord { EncodedDataType = 4 };
            Assert.AreEqual(4, dr.EncodedDataType);
        }

        [TestMethod]
        public void Equivalent_SameValues()
        {
            var dr = new DatasourceRecord
            {
                DatasourceId = 5,
                DataType = DatasourceRecord.DataTypeEnum.Integer,
                IntervalSeconds = 6,
                Timestamp = DateTime.Parse("2013-01-01 5:00am")
            };
            dr.SetIntValue(7);

            var dr2 = new DatasourceRecord
            {
                DatasourceId = 5,
                DataType = DatasourceRecord.DataTypeEnum.Integer,
                IntervalSeconds = 6,
                Timestamp = DateTime.Parse("2013-01-01 5:00am")
            };
            dr2.SetIntValue(7);

            Assert.IsTrue(dr.Equivalent(dr2));
            Assert.IsTrue(dr2.Equivalent(dr));
        }

        [TestMethod]
        public void Equivalent_CompareWithNull_NotEquivalent()
        {
            var dr = new DatasourceRecord();
            Assert.IsFalse(dr.Equivalent(null));
        }

        [TestMethod]
        public void Equivalent_SameInstances()
        {
            var dr = new DatasourceRecord();
            Assert.IsTrue(dr.Equivalent(dr));
        }

        [TestMethod]
        public void Equivalent_BothHaveNullValue()
        {
            var dr = new DatasourceRecord();
            var dr2 = new DatasourceRecord();

            Assert.IsTrue(dr.Equivalent(dr2));
            Assert.IsTrue(dr2.Equivalent(dr));
        }

        [TestMethod]
        public void Equivalent_SourceIsNull()
        {
            var dr = new DatasourceRecord();
            var dr2 = new DatasourceRecord();
            dr2.SetDoubleValue(5.6);

            Assert.IsFalse(dr.Equivalent(dr2));
            Assert.IsFalse(dr2.Equivalent(dr));
        }

        [TestMethod]
        public void BatchSizeCheck()
        {
            var serializer = new DatasourceRecordSerializer();

            var records = RandomDatasourceRecordGenerator.GenerateDummyData(5000).ToList();

            var ms = new MemoryStream();
            serializer.Serialize(ms, records);

            Debug.WriteLine("5000 records consumes {0}kb", ms.Length / 1024);
            Debug.WriteLine("Each record consumes {0} bytes", ms.Length / 5000);

            Assert.IsTrue(ms.Length < 200 * 1024);
        }
    }
}
