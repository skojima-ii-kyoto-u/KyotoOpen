using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointViewerUtility;

namespace RoundSF1.Logic
{
    public class Rule : RuleBase
    {
        private const int addPoint = 2;
        private const int doubleBonus = 1;

        public bool IsDirectPoint = false;

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
            if (!(f is field))
            {
                Console.Error.WriteLine("パラメータ FieldBase f はfield型である必要があります");
                return;
            }

            if (acts.Count == 0)
            {
                return;
            }
            
            foreach(Action act in acts)
            {
                Player target = (Player)UtilityMethod.Target(plist, act.Player);

                if (IsDirectPoint)
                {
                    if(act.State == Action.P)
                    {
                        target.Point = act.Point;
                    }
                }
                else
                {
                    switch (act.State)
                    {
                        case (Action.O1):
                            target.O1++;
                            target.Point += addPoint;
                            break;
                        case (Action.O2):
                            target.O2++;
                            target.Point += addPoint;
                            break;
                        case (Action.OO):
                            target.O1++;
                            target.O2++;
                            target.Point += addPoint + doubleBonus;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public override bool Judge_win(PlayerBase player, FieldBase field)
        {
            return false;
        }

        public override bool Judge_lose(PlayerBase player, FieldBase field)
        {
            return false;
        }

        public override double EvaluateValue(PlayerBase player)
        {
            return player.Point * 0.1 + player.O * 0.01;
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
