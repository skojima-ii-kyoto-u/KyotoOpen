using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointViewerUtility;

namespace Round3D.Logic
{
    public class Action : ActionBase
    {
        public const int
            O1 = 1, O2 = 2,
            X1 = -1, X2 = -2,
            Attack = 3, Heal = 4, Reinforce = 5,
            Switch = 0;

        private int kind;
        public int Kind
        {
            get
            {
                return kind;
            }
        }

        public Action(Player p, int s) : base(p, s)
        {
            if(s == O1 || s == O2)
            {
                //エラー
                s = int.MinValue;
            }
        }

        public Action(Player p, int s, int k) : base(p, s)
        {
            if (s == O1 || s == O2)
            {
                if(k != Attack && k != Heal && k != Reinforce)
                {
                    //エラー
                    s = int.MinValue;
                }
                else
                {
                    kind = k;
                }
            }
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
                case Switch:
                    state_str = "Switch";
                    break;
                default:
                    state_str = "ERROR";
                    break;
            }

            string kind_str = "\t";

            switch (kind)
            {
                case Attack:
                    kind_str += "Attack";
                    break;
                case Heal:
                    kind_str += "Heal";
                    break;
                case Reinforce:
                    kind_str += "Reinforce";
                    break;
                default:
                    kind_str = "";
                    break;
            }

            return string.Format(
                "{0}\t{1}{2}",
                Player.ToString(),
                state_str,
                kind_str);
        }
    }
}
