using System;
using System.Collections.Generic;
using System.Linq;
using Manufacturing.DataCollector;
using Manufacturing.Framework.Datasource;
using Manufacturing.Framework.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manufacturing.Framework.DataCollector
{
    [TestClass]
    public class MsmqRecordRepositoryTests
    {
        [TestMethod]
        [TestCategory("MSMQ")]
        public void ProcessRecords_RecordsWithoutBatching_ProcessedProperly()
        {
            var config = new DataCollectorConfiguration { MsmqQueueName = "unittest-1-" + DateTime.Now.ToString("u") };
            var q = new MsmqRecordRepository(new DatasourceRecordSerializer(), config);

            var dummyData = PushDummyRecords(q, 5);
            var processed = new List<IEnumerable<DatasourceRecord>>();

            q.ProcessRecords(processed.Add, 999);

            Assert.AreEqual(1, processed.Count);
            Assert.AreEqual(5, processed[0].Count());
            //Sometimes we do unspeakable things in unit tests
            Assert.IsTrue(dummyData[0].Equivalent(processed[0].ToArray()[0]));
            Assert.IsTrue(dummyData[1].Equivalent(processed[0].ToArray()[1]));
            Assert.IsTrue(dummyData[2].Equivalent(processed[0].ToArray()[2]));
            Assert.IsTrue(dummyData[3].Equivalent(processed[0].ToArray()[3]));
            Assert.IsTrue(dummyData[4].Equivalent(processed[0].ToArray()[4]));
        }

        [TestMethod]
        [TestCategory("MSMQ")]
        public void ProcessRecords_BatchRecordsEndAtBatch_ProcessedProperly()
        {
            var config = new DataCollectorConfiguration { MsmqQueueName = "unittest-2-" + DateTime.Now.ToString("u") };
            var q = new MsmqRecordRepository(new DatasourceRecordSerializer(), config);

            var dummyData = PushDummyRecords(q, 4);
            var processed = new List<IEnumerable<DatasourceRecord>>();

            q.ProcessRecords(processed.Add, 2);

            Assert.AreEqual(2, processed.Count);

            Assert.AreEqual(2, processed[0].Count());
            Assert.IsTrue(dummyData[0].Equivalent(processed[0].ToArray()[0]));
            Assert.IsTrue(dummyData[1].Equivalent(processed[0].ToArray()[1]));

            Assert.AreEqual(2, processed[1].Count());
            Assert.IsTrue(dummyData[2].Equivalent(processed[1].ToArray()[0]));
            Assert.IsTrue(dummyData[3].Equivalent(processed[1].ToArray()[1]));
        }

        [TestMethod]
        [TestCategory("MSMQ")]
        public void ProcessRecords_BatchRecordsEndMidBatch_ProcessedProperly()
        {
            var config = new DataCollectorConfiguration { MsmqQueueName = "unittest-2-" + DateTime.Now.ToString("u") };
            var q = new MsmqRecordRepository(new DatasourceRecordSerializer(), config);

            var dummyData = PushDummyRecords(q, 5);
            var processed = new List<IEnumerable<DatasourceRecord>>();

            q.ProcessRecords(processed.Add, 2);

            Assert.AreEqual(3, processed.Count);

            Assert.AreEqual(2, processed[0].Count());
            Assert.IsTrue(dummyData[0].Equivalent(processed[0].ToArray()[0]));
            Assert.IsTrue(dummyData[1].Equivalent(processed[0].ToArray()[1]));

            Assert.AreEqual(2, processed[1].Count());
            Assert.IsTrue(dummyData[2].Equivalent(processed[1].ToArray()[0]));
            Assert.IsTrue(dummyData[3].Equivalent(processed[1].ToArray()[1]));

            Assert.AreEqual(1, processed[2].Count());
            Assert.IsTrue(dummyData[4].Equivalent(processed[2].ToArray()[0]));
        }

        [TestMethod]
        [TestCategory("MSMQ")]
        public void ProcessRecords_NoRecords_DontBomb()
        {
            var config = new DataCollectorConfiguration {MsmqQueueName = "unittest-3-" + DateTime.Now.ToString("u")};
            var q = new MsmqRecordRepository(new DatasourceRecordSerializer(), config);

            var processed = new List<IEnumerable<DatasourceRecord>>();

            q.ProcessRecords(processed.Add, 999);

            Assert.AreEqual(0, processed.Count);
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestCategory("MSMQ")]
        public void ProcessRecords_InvalidBatchSize_RaiseException()
        {
            var config = new DataCollectorConfiguration { MsmqQueueName = "unittest-3-" + DateTime.Now.ToString("u") };
            var q = new MsmqRecordRepository(new DatasourceRecordSerializer(), config);

            var processed = new List<IEnumerable<DatasourceRecord>>();

            q.ProcessRecords(processed.Add, 0);
        }

        private static List<DatasourceRecord> PushDummyRecords(MsmqRecordRepository queue, int numberOfRecords)
        {
            var dummyData = RandomDatasourceRecordGenerator.GenerateDummyData(numberOfRecords).ToList();

            foreach (var record in dummyData)
            {
                queue.Push(record);
            }

            return dummyData;
        }
    }
}
