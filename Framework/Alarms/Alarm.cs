using System;

namespace Manufacturing.Framework.Alarms
{
    public class Alarm
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public bool Acknowledged { get; set; }
    }
}
