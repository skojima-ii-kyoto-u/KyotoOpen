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
                RaisePropertyChanged("FurtherPoint");
                RaisePropertyChanged("TeamP");
                RaisePropertyChanged("TextColor");
            }
        }
        
        public string FurtherPoint
        {
            get
            {
                return string.Format(
                    "{0} + {1}",
                    player.InitialPoint, player.AdditionalPoint);
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
