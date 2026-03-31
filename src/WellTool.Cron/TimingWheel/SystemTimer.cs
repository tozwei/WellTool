using System.Threading;

namespace WellTool.Cron.TimingWheel
{
    public class SystemTimer
    {
        private Timer _timer;
        private long _tickMs;
        private Action _callback;
        
        public SystemTimer(long tickMs, Action callback)
        {
            _tickMs = tickMs;
            _callback = callback;
            _timer = new Timer(Tick, null, _tickMs, _tickMs);
        }
        
        private void Tick(object state)
        {
            _callback();
        }
        
        public void Stop()
        {
            _timer.Dispose();
        }
    }
}