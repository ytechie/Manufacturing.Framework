using System;

namespace Manufacturing.Framework.Datasource
{
    public interface IDatasource
    {
        event EventHandler<DataReceivedEventArgs<decimal>> DataReceived;

        void StartRead();
    }
}
