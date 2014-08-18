using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manufacturing.Framework.Configuration
{
    public class StorageConfiguration
    {
        public string EndpointProtocol { get; set; }
        public string AccountName { get; set; }
        public string AccountKey { get; set; }

        public string RawDataContainer { get; set; }
        public string GetConnectionString()
        {
            return string.Format(
                "DefaultEndpointsProtocol={0};AccountName={1};AccountKey={2}",
                EndpointProtocol,
                AccountName,
                AccountKey);
        }
    }
}
