using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointViewerUtility;

namespace Round3C.Logic
{
    public class Action : ActionBase
    {
        public const int
            O1 = 1, O2 = 2,
            X1 = -1, X2 = -2;

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
                case X1:
                    state_str = "X1";
                    break;
                case X2:
                    state_str = "X2";
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
