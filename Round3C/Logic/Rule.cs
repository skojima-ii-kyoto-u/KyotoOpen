using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointViewerUtility;

namespace Round3C.Logic
{
    public class Rule : RuleBase
    {
        private const int winPoint = 15;
        private const int loseX = 4;

        static private field thisfield = new field();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pl"></param>
        /// <param name="outpath"></param>
        public Rule(Player[] pl, string outpath) : base(thisfield, pl, outpath, false)
        {
            
        }

        public override void Update(List<PointViewerUtility.ActionBase> acts, PlayerBase[] plist, FieldBase f)
        {
            if(acts.Count == 1 && f is field)
            {
                Player target = (Player)UtilityMethod.Target(plist, acts[0].Player);
                Action act = (Action)acts[0];

                switch (act.State)
                {
                    case (Action.O1):
                        target.O1++;
                        target.P1++;
                        target.Point += target.P2 + 1;
                        break;
                    case (Action.O2):
                        target.O2++;
                        target.P2++;
                        target.Point += target.P1 + 1;
                        break;
                    case (Action.X1):
                        target.X1++;
                        target.P1 = 0;
                        break;
                    case (Action.X2):
                        target.X2++;
                        target.P2 = 0;
                        break;
                    default:
                        break;
                }
            }
        }

        public override bool Judge_win(PlayerBase player, FieldBase field)
        {
            return player.Point >= winPoint;
        }

        public override bool Judge_lose(PlayerBase player, FieldBase field)
        {
            return ((Player)player).X >= loseX;
        }

        public override double EvaluateValue(PlayerBase player)
        {
            return player.Point * 0.1 + player.O * 0.01 + (99 - player.X) * 0.0001;
        }

        public override string PrintPlayer(PlayerBase player)
        {
            return string.Format(
                "{0}<{1}> {2}",
                player.ToString(),
                player.Rank,
                player.PointToString());
        }

        class field : FieldBase
        {
            public field()
            {
            }

            public override FieldBase Clone()
            {
                field f = new field();
                f.Copy(this);
                return f;
            }

            public override FieldBase Copy(FieldBase original)
            {
                if (original is field)
                    return copy((field)original);
                else
                    throw new ArgumentException();
            }

            private FieldBase copy(field original)
            {
                this.Win = original.Win;
                this.Lose = original.Lose;

                return this;
            }
        }
    }
}
