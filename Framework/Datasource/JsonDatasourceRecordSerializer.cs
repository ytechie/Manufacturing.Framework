using System.Collections.Generic;
using System.IO;
using System.Linq;
using Manufacturing.Framework.Dto;
using Microsoft.FluentMessaging;
using Newtonsoft.Json;

namespace Manufacturing.Framework.Datasource
{
    public class JsonDatasourceRecordSerializer : IDatasourceRecordSerializer
    {
        public Stream Serialize(IEnumerable<DatasourceRecord> toSerialize)
        {
            return Serialize(toSerialize.ToList());
        }

        public IEnumerable<DatasourceRecord> Deserialize(Stream sourceStream)
        {
            var sr = new StreamReader(sourceStream);
            var json = sr.ReadToEnd();
            return JsonConvert.DeserializeObject<List<DatasourceRecord>>(json);

        }

        public Stream Serialize(List<DatasourceRecord> toSerialize)
        {
            var json = JsonConvert.SerializeObject(toSerialize);
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            sw.Write(json);
            sw.Flush();

            return ms;
        }
    }
}