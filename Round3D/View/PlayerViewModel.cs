using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Round3D.Logic;

namespace Round3D.View
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
                RaisePropertyChanged("Job1");
                RaisePropertyChanged("Job2");
                RaisePropertyChanged("Job1Foreground");
                RaisePropertyChanged("Job2Foreground");
                RaisePropertyChanged("AP");
                RaisePropertyChanged("TextColor");
            }
        }
        
        public string OX1
        {
            get
            {
                return string.Format(
                    "{0}○",
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
                    "{0}○",
                    player.O2);
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

        public string Job1
        {
            get
            {
                return player.IsP1Attacker ? "攻" : "助";
            }
        }

        public string Job2
        {
            get
            {
                return !player.IsP1Attacker ? "攻" : "助";
            }
        }

        private Brush jobbrushSilver = new SolidColorBrush(Colors.Silver);
        private Brush jobbrushGold = new SolidColorBrush(Colors.Gold);

        public Brush Job1Foreground
        {
            get
            {
                return player.IsP1Attacker ? jobbrushSilver : jobbrushGold;
            }
        }

        public Brush Job2Foreground
        {
            get
            {
                return !player.IsP1Attacker ? jobbrushSilver : jobbrushGold;
            }
        }

        public string AP
        {
            get
            {
                return string.Format("{0}AP", player.AP);
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
                    return solidbrushGray;
                }
                else if(player.SleepCount > 0)
                {
                    return solidbrushBlue;
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
