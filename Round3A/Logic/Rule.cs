using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointViewerUtility;

namespace Round3A.Logic
{
    public class Rule : RuleBase
    {
        static private field thisfield = new field();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pl"></param>
        /// <param name="norma">ノルマ設定。0:5o2x/1:4o2x:/2:5o3x/3:4o3x</param>
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
                Player target = (Player)UtilityMethod.Target(plist, acts[0].Player);

                switch (act.State)
                {
                    case (Action.O1):
                        target.O1++;
                        break;
                    case (Action.O2):
                        target.O2++;
                        break;
                    case (Action.X1):
                        target.X1++;
                        target.SleepCount = 2;
                        break;
                    case (Action.X2):
                        target.X2++;
                        target.SleepCount = 2;
                        break;
                    case (Action.FuncSet):
                        target.FuncNum = act.FuncNum;
                        break;
                    default:
                        break;
                }
            }
        }

        public override bool Judge_win(PlayerBase player, FieldBase field)
        {
            return false;
        }

        public override bool Judge_lose(PlayerBase player, FieldBase field)
        {
            return (((Player)player).FuncNum == 5) && (((Player)player).X > 3);
        }

        public override double EvaluateValue(PlayerBase player)
        {
            return player.Point * 0.1 + (99 - player.X) * 0.001;
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
