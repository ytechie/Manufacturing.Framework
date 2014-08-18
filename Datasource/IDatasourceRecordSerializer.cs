using Manufacturing.Framework.Dto;
using Microsoft.FluentMessaging;

namespace Manufacturing.Framework.Datasource
{
    public interface IDatasourceRecordSerializer : ISerializer<DatasourceRecord>
    {
        //void Serialize(Stream ms, List<DatasourceRecord> record);
        //List<DatasourceRecord> Deserialize(Stream sourceStream);
    }
}