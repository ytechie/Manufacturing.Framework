using System;

namespace Manufacturing.Framework.Utility
{
    public interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}
