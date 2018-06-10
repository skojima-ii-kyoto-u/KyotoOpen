using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Round3A.Logic;

namespace Round3A.View
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

        public string Function
        {
            get
            {
                switch (player.FuncNum)
                {
                    case 0:
                        return "";
                    case 1:
                        return "4(A-a)+B";
                    case 2:
                        return "A+4(B-b)";
                    case 3:
                        return "AB+4";
                    case 4:
                        return "(A+B-2)^2";
                    case 5:
                        return "(AB)^(3-a-b)";
                    case 6:
                        return "(A+B)!/(1+a+b)";
                    default:
                        return "";
                }
            }
        }
        public void RaiseFunctionChanged()
        {
            RaisePropertyChanged("Function");
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


        public PlayerViewModel(Player p)
        {
            player = p;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string PropName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }
    }
}
