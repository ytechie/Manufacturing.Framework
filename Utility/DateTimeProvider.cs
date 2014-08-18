using System;

namespace Manufacturing.Framework.Utility
{
    public class DateTimeProvider : IDateTime
    {
        public DateTime UtcNow
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
