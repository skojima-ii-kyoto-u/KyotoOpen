using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Round3E.Logic;

namespace Round3E.View
{
    public class PlayerViewModel : INotifyPropertyChanged
    {
        private Player player;
        public Player Player
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
                RaisePropertyChanged("OX1");
                RaisePropertyChanged("OX2");
                RaisePropertyChanged("TeamP");
                RaisePropertyChanged("TextColor");
            }
        }
        

        public string OX1
        {
            get
            {
                return string.Format(
                    "{0}○{1}×",
                    player.O1, player.X1);
            }
            set
            {
                RaisePropertyChanged("OX1");
            }
        }

        public string OX2
        {
            get
            {
                return string.Format(
                    "{0}○{1}×",
                    player.O2, player.X2);
            }
            set
            {
                RaisePropertyChanged("OX2");
            }
        }

        public string TeamP
        {
            get
            {
                return string.Format(
                    "{0}",
                    player.Point);
            }
            set
            {
                RaisePropertyChanged("TeamP");
            }
        }

        private Brush solidbrushOrange = new SolidColorBrush(Colors.Orange);
        private Brush solidbrushGray = new SolidColorBrush(Colors.Gray);
        private Brush solidbrushBlue = new SolidColorBrush(Colors.DeepSkyBlue);
        private Brush solidbrushWhite = new SolidColorBrush(Colors.White);
        public Brush TextColor
        {
            get
            {
                if (!player.IsAnswerable)
                    return solidbrushGray;
                else if (player.SleepCount > 0)
                    return solidbrushBlue;
                else
                    return solidbrushWhite;
            }
        }

        private int num = 0;
        public Brush TeamColor
        {
            get
            {
                switch (num)
                {
                    case 0:
                        return solidbrushWhite;
                    case 1:
                        return new SolidColorBrush(Colors.DarkRed);
                    case 2:
                        return new SolidColorBrush(Colors.OrangeRed);
                    case 3:
                        return new SolidColorBrush(Colors.Yellow);
                    case 4:
                        return new SolidColorBrush(Colors.Green);
                    case 5:
                        return new SolidColorBrush(Colors.Blue);
                    case 6:
                        return new SolidColorBrush(Colors.Violet);
                    default:
                        return solidbrushWhite;
                }
            }
        }

        public PlayerViewModel(Player p, int pnum)
        {
            player = p;
            num = pnum;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string PropName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }
    }
}
