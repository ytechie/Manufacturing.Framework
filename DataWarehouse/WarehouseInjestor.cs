using System;
using System.IO;
using System.Reflection;
using log4net;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Manufacturing.Framework.DataWarehouse
{
    public class WarehouseInjestor
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Injest(BrokeredMessage message, CloudBlobContainer blobContainer)
        {
            Log.DebugFormat("Injesting message #{0} with size {1}", message.SequenceNumber, message.Size);

            var blob = blobContainer.GetBlockBlobReference(Guid.NewGuid().ToString());
            blob.UploadFromStream(message.GetBody<Stream>());
        }
    }
}
