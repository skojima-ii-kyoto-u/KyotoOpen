using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using RoundSF2.Logic;

namespace RoundSF2.View
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _player1;
        public string Player1
        {
            get
            {
                return _player1;
            }
            set
            {
                if(_player1 != value)
                {
                    _player1 = value;
                    RaisePropertyChanged("Player1");
                }
            }
        }

        private string _player2;
        public string Player2
        {
            get
            {
                return _player2;
            }
            set
            {
                if (_player2 != value)
                {
                    _player2 = value;
                    RaisePropertyChanged("Player2");
                }
            }
        }

        private Brush _brushBlack = new SolidColorBrush(Colors.Black);
        private Brush _brushGray = new SolidColorBrush(Colors.Gray);
        private bool select1 = true;
        public Brush Brush1
        {
            get
            {
                if (select1)
                    return _brushBlack;
                else
                    return _brushGray;
            }
        }

        public Brush Brush2
        {
            get
            {
                if (!select1)
                    return _brushBlack;
                else
                    return _brushGray;
            }
        }

        public void Switch()
        {
            select1 = !select1;
            RaisePropertyChanged("Brush1");
            RaisePropertyChanged("Brush2");
        }

        public MainWindowViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string PropName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }
    }
}
