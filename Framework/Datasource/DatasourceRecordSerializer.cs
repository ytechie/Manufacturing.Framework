using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using Manufacturing.Framework.Dto;
using Microsoft.FluentMessaging;
using log4net;

namespace Manufacturing.Framework.Datasource
{
    public class DatasourceRecordSerializer : IDatasourceRecordSerializer, ISerializer<DatasourceRecord>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Serialize(Stream outputStream, List<DatasourceRecord> records)
        {
            using (var gz = new GZipStream(outputStream, CompressionLevel.Fastest, true))
            {
                ProtoBuf.Serializer.Serialize(gz, records);
            }

            //What if we can't seek? I've never tried this with anything but a MemoryStream
            if (outputStream.CanSeek)
                outputStream.Position = 0;
        }

        public List<DatasourceRecord> Deserialize(Stream sourceStream)
        {
            using (var gz = new GZipStream(sourceStream, CompressionMode.Decompress, false))
            {
                return ProtoBuf.Serializer.Deserialize<List<DatasourceRecord>>(gz);
            }
        }

        public Stream Serialize(IEnumerable<DatasourceRecord> toSerialize)
        {
            var ms = new MemoryStream();
            Serialize(ms, toSerialize.ToList());
            return ms;
        }

        IEnumerable<DatasourceRecord> ISerializer<DatasourceRecord>.Deserialize(Stream sourceStream)
        {
            return Deserialize(sourceStream);
        }
    }
}
