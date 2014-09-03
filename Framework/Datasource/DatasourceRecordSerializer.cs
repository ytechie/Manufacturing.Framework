using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using Manufacturing.Framework.Dto;
using Microsoft.FluentMessaging;
using log4net;
using ProtoBuf.Meta;

namespace Manufacturing.Framework.Datasource
{
    public class DatasourceRecordSerializer : IDatasourceRecordSerializer
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly RuntimeTypeModel _serializer;

        public DatasourceRecordSerializer()
        {
            _serializer = TypeModel.Create();
            _serializer.Add(typeof (List<DatasourceRecord>), true);
            _serializer.CompileInPlace();
        }

        public void Serialize(Stream outputStream, List<DatasourceRecord> records)
        {
            var copy = new List<DatasourceRecord>();
            copy.AddRange(records);
            using (var gz = new GZipStream(outputStream, CompressionLevel.Fastest, true))
            {
                _serializer.Serialize(gz, copy);
            }

            //What if we can't seek? I've never tried this with anything but a MemoryStream
            if (outputStream.CanSeek)
                outputStream.Position = 0;
        }

        public List<DatasourceRecord> Deserialize(Stream sourceStream)
        {
            using (var gz = new GZipStream(sourceStream, CompressionMode.Decompress, false))
            {
                var list = new List<DatasourceRecord>();
                _serializer.Deserialize(gz, list, typeof (List<DatasourceRecord>));
                return list;
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