using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Round3C.Logic;

namespace Round3C.View
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
                RaisePropertyChanged("TeamX");
                RaisePropertyChanged("TextColor");
            }
        }
        
        public string OX1
        {
            get
            {
                return string.Format(
                    "{0}○",
                    player.P1);
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
                    "{0}○",
                    player.P2);
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

        public string TeamX
        {
            get
            {
                return string.Format("{0}×", player.X);
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
                {
                    if (player.Point >= 15)
                        return solidbrushOrange;
                    else
                        return solidbrushGray;
                }
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
