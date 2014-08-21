using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manufacturing.Framework.Repository.Entities
{
    public class BlobInfo
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Uri { get; set; }
        public long Size { get; set; }
        public DateTimeOffset LastModified { get; set; }

        public static BlobInfo FromBlob(CloudBlockBlob blob)
        {
            return new BlobInfo()
            {
                Name = blob.Name,
                FileName = Path.GetFileName(blob.Name),
                ContentType = blob.Properties.ContentType,
                Uri = blob.Uri.ToString(),
                Size = blob.Properties.Length,
                LastModified = blob.Properties.LastModified.GetValueOrDefault()
            };
        }
    }
}
