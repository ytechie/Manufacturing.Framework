using System.Collections.Generic;
//using ProtoBuf;

namespace Manufacturing.Framework.Dto
{
    //[ProtoContract]
    public class EventRecord : DatasourceRecord
    {
        //[ProtoMember(1)]
        public int EventType { get; protected set; }

        //[ProtoMember(2)]
        public Dictionary<int, byte[]> MetaData { get; set; }

        public EventRecord()
        {
            MetaData = new Dictionary<int, byte[]>();
        }

        public override bool Equivalent(DatasourceRecord compare)
        {
            if (!base.Equivalent(compare))
                return false;

            var c = compare as EventRecord;

            if (c == null)
                return false;

            if (EventType != c.EventType)
                return false;

            if (MetaData == null ^ c.MetaData == null)
                return false;

            if (MetaData != null && c.MetaData != null)
            {
                if (MetaData.Count != c.MetaData.Count)
                    return false;
                foreach (var md in MetaData)
                {
                    if (!c.MetaData.ContainsKey(md.Key))
                        return false;

                    for (var i = 0; i < md.Value.Length; i++)
                    {
                        if (md.Value[i] != c.MetaData[md.Key][i])
                            return false;
                    }
                }
            }

            return true;
        }
    }
}
