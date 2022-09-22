using System;
using System.Threading.Tasks;

namespace ObjectsFall.Timer
{
    public class CustomTimer 
    {
        #region Fields
        private readonly TimeSpan _startTime;
        private TimeSpan _currentTime;
        private bool _timerRunning;
        #endregion
        #region Properties       
        public TimeSpan CurrentTime
        {
            get { return _currentTime; }
            set { _currentTime = value >= TimeSpan.Zero ? value : TimeSpan.Zero; }
        }
        public TimeSpan Interval { get; set; }
        public bool IsTimerStopped => !_timerRunning;
        #endregion
        public event EventHandler IntervalPassed;

        public CustomTimer(TimeSpan startTime)
        {
            Interval = TimeSpan.FromSeconds(1);
            _startTime = startTime;
            _currentTime = _startTime;
        }
        #region Public Methods
        public void Start()
        {
            if (IsTimerStopped)
            {
                _timerRunning = true;
                RunTimerAsync();
            }
        }
        public void Stop()
        {
            _timerRunning = false;
        }
        public void Reset()
        {
            Stop();
            _currentTime = _startTime;
        }
        #endregion
        #region Private Methods
        private async void RunTimerAsync()
        {
            while (_timerRunning)
            {
                await Task.Delay(Interval).ConfigureAwait(true);

                if (_timerRunning)
                    CountCurrent();
            }
        }
        void CountCurrent()
        {
            CurrentTime = CurrentTime.Add(Interval);
            RaiseIntervalPassedEvent();
        }
        private void RaiseIntervalPassedEvent()
        {
            IntervalPassed?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}