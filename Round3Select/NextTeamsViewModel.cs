using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round3Select
{
    class NextTeamsViewModel : INotifyPropertyChanged
    {
        private List<Player> teams = new List<Player>();
        private List<Player> dequeued = new List<Player>();

        public string Team1
        {
            get
            {
                return (teams.Count > 0)? teams[0].TeamName : "";
            }
        }

        public string Team2
        {
            get
            {
                return (teams.Count > 1) ? teams[1].TeamName : "";
            }
        }

        public string Team3
        {
            get
            {
                return (teams.Count > 2) ? teams[2].TeamName : "";
            }
        }

        public string Team4
        {
            get
            {
                return (teams.Count > 3) ? teams[3].TeamName : "";
            }
        }

        public string Team5
        {
            get
            {
                return (teams.Count > 4) ? teams[4].TeamName : "";
            }
        }

        public Player Dequeue()
        {
            Player result;
            if (teams.Count > 0)
            {
                result = teams[0];
                dequeued.Insert(0, teams[0]);
                teams.RemoveAt(0);
                for (int i = 1; i < 6; i++)
                {
                    RaisePropertyChanged("Team" + i.ToString());
                }
            }
            else
                result = null;

            return result;
        }

        public void Undo()
        {
            if(dequeued.Count > 0)
            {
                teams.Insert(0, dequeued[0]);
                dequeued.RemoveAt(0);
                for (int i = 1; i < 6; i++)
                {
                    RaisePropertyChanged("Team" + i.ToString());
                }
            }
        }

        public NextTeamsViewModel(List<Player> teams)
        {
            this.teams.AddRange(teams);
            for(int i = 1; i < 6; i++)
            {
                RaisePropertyChanged("Team" + i.ToString());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string PropName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }
    }
}
