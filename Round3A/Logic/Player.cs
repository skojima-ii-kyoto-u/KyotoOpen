using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointViewerUtility;

namespace Round3A.Logic
{
    public class Player : PlayerBase
    {
        public string TeamName { get; }
        public string Player1 { get; }
        public string Player2 { get; }
        
        public int O1 = 0, O2 = 0, X1 = 0, X2 = 0;

        public new int X
        {
            get
            {
                return X1 + X2;
            }
        }

        private int funcNum = 0;
        public int FuncNum
        {
            get
            {
                return funcNum;
            }
            set
            {
                if (funcNum == 0)
                    funcNum = value;
            }
        }

        public new int Point
        {
            get
            {
                switch (funcNum)
                {
                    case 1:
                        return 4 * (O1 - X1) + O2;
                    case 2:
                        return O1 + 4 * (O2 - X2);
                    case 3:
                        return O1 * O2 + 4;
                    case 4:
                        return (O1 + O2 - 2) * (O1 + O2 - 2);
                    case 5:
                        return myPow(O1 * O2, 3 - X1 - X2);
                    case 6:
                        return (int)Math.Floor((double)myFactorial(O1 + O2) / (X1 + X2 + 1));
                    default:
                        return O1 + O2;
                }
            }
        }

        private int myPow(int b, int n)
        {
            if (n < 0)
                return 0;
            else if (n == 0)
                return 1;
            else
                return b * myPow(b, n - 1);
        }

        private int myFactorial(int n)
        {
            if (n < 0)
                return 0;
            else if (n == 0)
                return 1;
            else
            {
                return n * myFactorial(n - 1);
            }
        }

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
