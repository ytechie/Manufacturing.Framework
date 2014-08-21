using System;
using System.Collections.Generic;
using System.IO;
using Manufacturing.Framework.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manufacturing.Framework.Datasource
{
    [TestClass]
    public class DatasourceRecordSerializerTests
    {
        [TestMethod]
        public void Serialize_Deserialize_NoDataLoss()
        {
            var s = new DatasourceRecordSerializer();
            var ms = new MemoryStream();

            var recs = new List<DatasourceRecord>
            {
                new DatasourceRecord
                {
                    DatasourceId = 1,
                    IntervalSeconds = 1,
                    Timestamp = DateTime.Parse("2013-11-11")
                },
                new DatasourceRecord
                {
                    DatasourceId = 2,
                    IntervalSeconds = 2,
                    Timestamp = DateTime.Parse("2013-11-12")
                }
            };
            recs[0].SetIntValue(1);
            recs[1].SetIntValue(2);

            s.Serialize(ms, recs);
            ms.Position = 0;

            var outRecs = s.Deserialize(ms);

            Assert.AreNotEqual(recs, outRecs);
            Assert.AreEqual(2, outRecs.Count);
            Assert.AreEqual(2, outRecs[1].DatasourceId);
            Assert.AreEqual(2, outRecs[1].IntervalSeconds);
            Assert.AreEqual(DateTime.Parse("2013-11-12"), outRecs[1].Timestamp);
            Assert.AreEqual(2, outRecs[1].GetIntValue());
        }

        [TestMethod]
        public void Serialize_Deserialize_NoDataLoss_DoubleValue()
        {
            var s = new DatasourceRecordSerializer();
            var ms = new MemoryStream();

            var recs = new List<DatasourceRecord>
            {
                new DatasourceRecord
                {
                    DatasourceId = 1,
                    IntervalSeconds = 1,
                    Timestamp = DateTime.Parse("2013-11-11")
                },
                new DatasourceRecord
                {
                    DatasourceId = 2,
                    IntervalSeconds = 2,
                    Timestamp = DateTime.Parse("2013-11-12")
                }
            };
            recs[0].SetDoubleValue(1.05D);
            recs[1].SetDoubleValue(2.05D);

            s.Serialize(ms, recs);
            ms.Position = 0;

            var outRecs = s.Deserialize(ms);

            Assert.AreNotEqual(recs, outRecs);
            Assert.AreEqual(2, outRecs.Count);
            Assert.AreEqual(2, outRecs[1].DatasourceId);
            Assert.AreEqual(2, outRecs[1].IntervalSeconds);
            Assert.AreEqual(DatasourceRecord.DataTypeEnum.Double, outRecs[1].DataType);
            Assert.AreEqual(DateTime.Parse("2013-11-12"), outRecs[1].Timestamp);
            Assert.AreEqual(2.05D, outRecs[1].GetDoubleValue());
        }

    }
}
