using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointViewerUtility;

namespace RoundSF2.Logic
{
    public class Player : PlayerBase
    {
        public string TeamName { get; }
        public string Player1 { get; }
        public string Player2 { get; }
        
        public int O1 = 0, O2 = 0, X1 = 0, X2 = 0, B1 = 0, B2 = 0;
        
        public new int O
        {
            get
            {
                return O1 + O2;
            }
        }

        public new int X
        {
            get
            {
                return X1 + X2;
            }
        }

        public int B
        {
            get
            {
                return B1 + B2;
            }
        }

        public int InitialPoint { get; set; }

        public int AdditionalPoint { get; set; }
        
        public Player(string teamname, string player1, string player2)
        {
            TeamName = teamname;
            Player1 = player1;
            Player2 = player2;
        }

        public override string MainKey()
        {
            return TeamName;
        }

        public override string ToString()
        {
            return string.Format(
                "{0}（{1}/{2}）",
                TeamName, Player1, Player2);
        }

        public override string PointToString()
        {
            return String.Format("{0}", Point);
        }
    }
}
