using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointViewerUtility;

namespace RoundSF1.Logic
{
    public class Action : ActionBase
    {
        public const int
            O1 = 1, O2 = 2, OO = 3,
            P = 0;

        private int point;
        public int Point
        {
            get
            {
                return point;
            }
            set
            {
                point = value;
            }
        }

        public string Line = "";

        public Action(Player p, int s) : base(p, s)
        {

        }

        public override string ToString()
        {
            string state_str = "";

            switch (State)
            {
                case O1:
                    state_str = "O1";
                    break;
                case O2:
                    state_str = "O2";
                    break;
                case OO:
                    state_str = "OO";
                    break;
                case P:
                    state_str = "P\t" + Point.ToString();
                    break;
                default:
                    state_str = "ERROR";
                    break;
            }

            return string.Format(
                "{0}\t{1}",
                Player.ToString(),
                state_str);
        }
    }
}
