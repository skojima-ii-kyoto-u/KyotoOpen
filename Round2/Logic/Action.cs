using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointViewerUtility;

namespace Round2
{
    public class Action : ActionBase
    {
        public const int
            SET4o3x = 43, SET4o2x = 42, SET5o3x = 53, SET5o2x = 52,
            O1 = 1, O2 = 2,
            X1 = -1, X2 = -2;

        public Action(Player p, int s) : base(p, s)
        {

        }

        public override string ToString()
        {
            string state_str = "";

            switch (State)
            {
                case SET4o2x:
                    state_str = "SET4o2x";
                    break;
                case SET4o3x:
                    state_str = "SET4o3x";
                    break;
                case SET5o2x:
                    state_str = "SET5o2x";
                    break;
                case SET5o3x:
                    state_str = "SET5o3x";
                    break;
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
