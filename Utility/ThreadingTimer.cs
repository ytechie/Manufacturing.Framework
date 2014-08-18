using System;
using System.Threading;

namespace Manufacturing.Framework.Utility
{
    public delegate void TimerEventHandler(object sender, TimerEventArgs e);

    public class TimerEventArgs : EventArgs
    {
        public TimerEventArgs(object state)
        {
            this.State = state;
        }

        public object State { get; private set; }
    }

    public interface ITimer
    {
        void Change(TimeSpan dueTime, TimeSpan period);
        event TimerEventHandler Tick;
    }

    public class ThreadingTimer : ITimer, IDisposable
    {
        private readonly Timer timer;

        public ThreadingTimer()
        {
            timer = new Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
        }

        public void Change(TimeSpan dueTime, TimeSpan period)
        {
            timer.Change(dueTime, period);
        }

        public void Dispose()
        {
            timer.Dispose();
        }

        private void TimerCallback(object state)
        {
            var tick = Tick;
            if (tick != null)
                tick(this, new TimerEventArgs(state));
        }

        public event TimerEventHandler Tick;
    }
}