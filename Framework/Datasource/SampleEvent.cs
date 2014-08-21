using System;
using Manufacturing.Framework.Dto;
using ProtoBuf;

namespace Manufacturing.Framework.Datasource
{
    /// <summary>
    /// A sample event demonstrating subclassing the <see cref="EventRecord"/> class
    /// to add specific properties, while utilizing the underlying metadata.
    /// </summary>
    [ProtoContract]
    public class SampleEvent : EventRecord
    {
        private const int UnitIdPropertyIndex = 1;

        public SampleEvent()
        {
            EventType = 1;
        }

        public int UnitId
        {
            get
            {
                byte[] bytes;
                if (MetaData.TryGetValue(UnitIdPropertyIndex, out bytes))
                    return BitConverter.ToInt32(bytes, 0);
                else
                    return 0;
            }
            set
            {
                MetaData[UnitIdPropertyIndex] = BitConverter.GetBytes(value);
            }
        }

        //Notice: We don't override Equivalent since it's handled in the base class for MetaData
    }
}