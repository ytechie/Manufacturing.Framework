using System;
using System.Collections.Generic;
using Manufacturing.Framework.Datasource;
using Manufacturing.Framework.Dto;
using Manufacturing.Framework.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Manufacturing.DataPusher;

namespace Manufacturing.Framework.DataPusher
{
    [TestClass]
    public class ServiceBusQueueDataPusherTests
    {
        [TestInitialize]
        public void Init()
        {
            LoggingUtils.InitLocalLogging();
        }

        /// <summary>
        ///     This is really an integration test. Ignore by default.
        /// </summary>
       // [TestMethod]
       //[Ignore]
       // public void AdhocDataPush()
       // {
       //     var blobConfig = new DataPusherConfiguration()
       //     {
       //          PushBatchSize = 6,
       //           ServiceBusConnectionString = "Endpoint=sb://manufacturing.servicebus.windows.net/;SharedAccessKeyName=ClientPush;SharedAccessKey=RUCIbZu2VQlYrsscSi/4eHDUhfCWmrbgKbLbY7DxJ3Q=",
       //            PushIntervalSeconds= 3,
       //              ServiceBusQueueName = "cloudreceive"

       //     };
           

       // //    var dummyData = GenerateDummyData();

       // //    s.PushRecords(dummyData, "cloudreceive");
       //     var serializer = new DatasourceRecordSerializer();

       //     var s = new ServiceBusQueueDataPusher(blobConfig, serializer);
               

       //     var dummyData = GenerateDummyData();

       //     s.PushRecords(dummyData);
     
       // }

        public static IEnumerable<DatasourceRecord> GenerateDummyData()
        {
            return RandomDatasourceRecordGenerator.GenerateDummyData(6000);
        }
    }
}
