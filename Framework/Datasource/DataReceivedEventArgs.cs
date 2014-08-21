using System;

namespace Manufacturing.Framework.Datasource
{
    public class DataReceivedEventArgs<T> : EventArgs
    {
        public T Value { get; private set; }
        public DateTime Timestamp { get; private set; }

        public int DeviceID { get; set; }
        public DataReceivedEventArgs(T value, int deviceid, DateTime timestamp)
        {
            Value = value;
            Timestamp = timestamp;
            DeviceID = deviceid;
        }
    }
}
