using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round3Select
{
    class CourseViewModel : INotifyPropertyChanged
    {
        public string CourseName { get; }

        private List<Player> plist = new List<Player>();
        public List<Player> CourseTeam
        {
            get
            {
                return plist;
            }
        }
        
        public string Team1
        {
            get
            {
                return (plist.Count > 0) ? plist[0].TeamName : "";
            }
        }

        public string Team2
        {
            get
            {
                return (plist.Count > 1) ? plist[1].TeamName : "";
            }
        }

        public string Team3
        {
            get
            {
                return (plist.Count > 2) ? plist[2].TeamName : "";
            }
        }

        public string Team4
        {
            get
            {
                return (plist.Count > 3) ? plist[3].TeamName : "";
            }
        }

        public string Team5
        {
            get
            {
                return (plist.Count > 4) ? plist[4].TeamName : "";
            }
        }

        public string Team6
        {
            get
            {
                return (plist.Count > 5) ? plist[5].TeamName : "";
            }
        }
        
        public void AddTeam(Player team)
        {
            if(!IsFull())
            {
                plist.Add(team);
                for (int i = 1; i <= 6; i++)
                    RaisePropertyChanged("Team" + i.ToString());
            }
        }

        public bool IsFull()
        {
            return plist.Count == 6;
        }

        public void Undo()
        {
            if(plist.Count > 0)
            {
                plist.RemoveAt(plist.Count - 1);
                for (int i = 1; i <= 6; i++)
                    RaisePropertyChanged("Team" + i.ToString());
            }
        }

        public CourseViewModel(string name)
        {
            CourseName = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string PropName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }
    }
}
