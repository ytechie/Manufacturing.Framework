using System;
using System.Linq;
using ProtoBuf;

namespace Manufacturing.Framework.Dto
{
    /// <summary>
    ///     Represents a record from a datasource
    /// </summary>
    [ProtoContract]
    [ProtoInclude(8, typeof(EventRecord))]
    public class DatasourceRecord
    {
        [ProtoMember(1)]
        public int DatasourceId { get; set; }
        [ProtoMember(2)]
        public DateTime Timestamp { get; set; }
        [ProtoMember(3)]
        public int IntervalSeconds { get; set; } //TODO: make more granular/generic?
        [ProtoMember(5)]
        public byte[] Value { get; set; }

        [ProtoMember(6)]
        public int EncodedDataType
        {
            get
            {
                return (int)DataType;
            }
            set
            {
                DataType = (DataTypeEnum)value;
            }
        }

        public DataTypeEnum DataType { get; set; }

        public enum DataTypeEnum
        {
            Undefined = 0,
            Integer = 1,
            UnsignedFloat = 2,
            Double = 3,
            DateTime = 4,
            String = 5,
            Boolean = 6,
            Decimal = 7
        }

        public virtual bool Equivalent(DatasourceRecord compare)
        {
            if (compare == null)
                return false;
            if (this == compare)
                return true;

            if (DatasourceId != compare.DatasourceId)
                return false;
            if (Timestamp != compare.Timestamp)
                return false;
            if (IntervalSeconds != compare.IntervalSeconds)
                return false;
            if (Value == null ^ compare.Value == null || (Value != null && !Value.SequenceEqual(compare.Value)))
                return false;
            if (EncodedDataType != compare.EncodedDataType)
                return false;

            return true;
        }

    }
}
