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
    public class PlayerSelectViewModel : INotifyPropertyChanged
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
            }
        }

        private Brush _color;
        public Brush Color
        {
            get
            {
                return _color;
            }
            set
            {
                if(_color != value)
                {
                    _color = value;
                    RaisePropertyChanged("Color");
                }
            }
        }

        public PlayerSelectViewModel(Player p)
        {
            Player = p;
            Color = new SolidColorBrush(Colors.Gray);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string PropName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }
    }
}
