using System;

namespace Manufacturing.Framework.Configuration.Modules
{
    public interface IDatasourceConfiguration
    {
        TimeSpan GetRandomDatasourceInterval();
        decimal GetRandomDatasourceMin();
        TimeSpan GetRandomDatasourceMax();
    }
}
