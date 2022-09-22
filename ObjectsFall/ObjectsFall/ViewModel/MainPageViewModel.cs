using ObjectsFall.Database;
using ObjectsFall.Model;
using ObjectsFall.Timer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ObjectsFall.ViewModel
{
    public class MainPageViewModel : BaseModel
    {
        #region Fields
        private CustomTimer _timer;
        private bool _isTimeEnd;
        private double _timerSecond;
        private int _total;
        private int _score;
        private uint _moveSpeed;
        private int _randomTimeBetween;
        private string _timerValue;
        private List<GameRecord> _gameRecords;
        private string _playerName;

        #endregion
        public double TimerSecond
        {
            get { return _timerSecond; }
            set
            {
                _timerSecond = value;
                OnPropertyChanged("TimerSecond");
            }
        }
        public uint MoveSpeed
        {
            get { return _moveSpeed; }
            set
            {
                _moveSpeed = value;
                OnPropertyChanged("MoveSpeed");
            }
        }
        public int Total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged("Total");
            }
        }
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }
        public string TimerValue
        {
            get { return _timerValue; }
            set
            {
                _timerValue = value;
                OnPropertyChanged("TimerValue");
            }
        }
        public bool IsTimeEnd
        {
            get { return _isTimeEnd; }
            set
            {
                _isTimeEnd = value;
                OnPropertyChanged("IsTimeEnd");
            }
        }
        public List<GameRecord> GameRecords
        {
            get { return _gameRecords; }
            set
            {
                _gameRecords = value;
                OnPropertyChanged("GameRecords");
            }
        }
        private GameRecordRepository  gameRecordRepository  { get; set; }
        public string PlayerName
        {
            get { return _playerName; }
            set
            {
                _playerName = value;
                OnPropertyChanged("PlayerName");
            }
        }
        public CustomTimer Timer
        {
            get { return _timer; }
            set
            {
                _timer = value;
                OnPropertyChanged("Timer");
            }
        }
        public int RandomTimeBetween
        {
            get { return _randomTimeBetween; }
            set
            {
                _randomTimeBetween = value;
                OnPropertyChanged("RandomTimeBetween");
            }
        }
        #region Commands
        public Command<GameRecord> DeleteRecordCommand { get; set; }
        public Command<Image> PresentTappedCommand { get; set; }
        #endregion
        public MainPageViewModel()
        {
            PresentTappedCommand = new Command<Image>(selectedPresent => PresentTappedClicked(selectedPresent));
            DeleteRecordCommand = new Command<GameRecord>(selectedGamedRecord => DeleteRecordClicked(selectedGamedRecord));
            gameRecordRepository = new GameRecordRepository();
            GameRecords =  gameRecordRepository.GameRecords();
            TimeSpan timeSpan = new TimeSpan(1);
            Timer = new CustomTimer(timeSpan);
            MoveSpeed = 3000;
            RandomTimeBetween = 2000;
        }

        #region methods
        private void DeleteRecordClicked(GameRecord selectedGamedRecord)
        {
            gameRecordRepository.DeleteGameRecord(selectedGamedRecord);
            GameRecords = gameRecordRepository.GameRecords();
        }

        private void PresentTappedClicked(Image selectedPresent)
        {
            Score += 1;
            selectedPresent.IsVisible = false;
        }

        public void StartTimer()
        {
            Total = 0;
            Score = 0;
            Timer.Start();
            Timer.IntervalPassed += Timer_IntervalPassed;
        }

        private void Timer_IntervalPassed(object sender, EventArgs e)
        {
            TimerValue = string.Format("{0:mm\\:ss}", Timer.CurrentTime);
            TimerSecond = Math.Round(Timer.CurrentTime.TotalSeconds, 2);
            if (TimerSecond == 30)
                StopTimer();
        }
        private void StopTimer()
        {
            Timer.Reset();
            IsTimeEnd = true;

            gameRecordRepository.SaveGameRecord(new GameRecord() 
            { 
                Name = PlayerName, 
                Score = this.Score, 
                Total = this.Total 
            });
           
            GameRecords = gameRecordRepository.GameRecords();
        }
        #endregion
    }
}
