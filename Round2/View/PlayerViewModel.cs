using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Round2.View
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
                RaisePropertyChanged("TeamOX");
                RaisePropertyChanged("TextColor");
            }
        }

        public string Norma
        {
            get
            {
                if (player.WinPoint == int.MaxValue)
                    return "";
                else
                    return string.Format(
                        "{0}○{1}×",
                        player.WinPoint,
                        player.LoseX);
            }
        }
        public void RaiseNormaChanged()
        {
            RaisePropertyChanged("Norma");
        }

        public string OX1
        {
            get
            {
                return string.Format(
                    "{0}",
                    player.O1);
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
                    "{0}",
                    player.O2);
            }
            set
            {
                RaisePropertyChanged("OX2");
            }
        }

        public string TeamOX
        {
            get
            {
                return string.Format(
                    "{0}○{1}×",
                    player.Point,
                    player.X);
            }
            set
            {
                RaisePropertyChanged("TeamOX");
            }
        }

        private Brush solidbrushOrange = new SolidColorBrush(Colors.Orange);
        private Brush solidbrushGray = new SolidColorBrush(Colors.Gray);
        private Brush solidbrushWhite = new SolidColorBrush(Colors.White);
        public Brush TextColor
        {
            get
            {
                if (!player.IsAnswerable)
                    if (player.Point >= player.WinPoint)
                        return solidbrushOrange;
                    else
                        return solidbrushGray;
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
