using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Round1Timer
{
    class Timer : INotifyPropertyChanged
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private const int timeSpanMiliSec = 100;

        private DateTime endDateTime, stopDateTime;

        private bool counting = false;

        private int totalSec;

        public Timer()
        {
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(timeSpanMiliSec);

            Count = "--:--";
            Height = 0;
        }

        public void StartTimer(int minute, int second)
        {
            totalSec = minute * 60 + second;
            endDateTime = DateTime.Now.AddMinutes(minute).AddSeconds(second);

            var timeSpan = endDateTime - DateTime.Now;
            Count = timeSpan.ToString(@"mm\:ss");

            dispatcherTimer.Start();
            counting = true;

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(10);
            dt.Tick += (sender, e) =>
            {
                if(Height < 200)
                {
                    Height += 10;
                }
                else
                {
                    Height = 200;
                    dt.Stop();
                }
            };
            dt.Start();
        }

        public void Switch()
        {
            if (counting)
            {
                dispatcherTimer.Stop();
                stopDateTime = DateTime.Now;
                counting = false;
            }
            else
            {
                endDateTime = endDateTime.Add(DateTime.Now.Subtract(stopDateTime));
                dispatcherTimer.Start();
                counting = true;
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var timeSpan = endDateTime - DateTime.Now;

            Count = timeSpan.ToString(@"mm\:ss");

            if (Count == "00:00")
                dispatcherTimer.Stop();
        }

        private string _count;
        public string Count
        {
            get
            {
                return _count;
            }
            set
            {
                if(_count != value)
                {
                    _count = value;
                    RaisePropertyChanged("Count");
                }
            }
        }

        private int _height;
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                if(_height != value)
                {
                    _height = value;
                    RaisePropertyChanged("Height");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string PropName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }
    }
}
