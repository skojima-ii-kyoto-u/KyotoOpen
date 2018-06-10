using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointViewerUtility;

namespace Round2
{
    public class Player : PlayerBase
    {
        public string TeamName { get; }
        public string Player1 { get; }
        public string Player2 { get; }

        private int winPoint = int.MaxValue;
        public int WinPoint
        {
            get
            {
                return winPoint;
            }

            set
            {
                if (winPoint == int.MaxValue)
                    winPoint = value;
            }
        }

        private int loseX = int.MaxValue;
        public int LoseX
        {
            get
            {
                return loseX;
            }

            set
            {
                if (loseX == int.MaxValue)
                    loseX = value;
            }
        }

        public int O1 = 0, O2 = 0, X1 = 0, X2 = 0;

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

        public bool Standup1 = false, Standup2 = false;
        public bool BonusAdded = false;

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
            return String.Format("{0} - {1}", Point, X1 + X2);
        }
    }
}
