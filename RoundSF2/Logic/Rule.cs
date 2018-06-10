using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointViewerUtility;

namespace RoundSF2.Logic
{
    public class Rule : RuleBase
    {
        private const int
            bPoint = 3, oPoint = 1, penalty = 3;

        static private field thisfield = new field();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pl"></param>
        /// <param name="outpath"></param>
        public Rule(Player[] pl, int[] initPoint, string outpath) : base(thisfield, pl, outpath, false)
        {
            if(pl.Length == initPoint.Length)
            {
                for(int i = 0; i < pl.Length; i++)
                {
                    pl[i].Point = initPoint[i];
                }
            }
            else
            {
                Console.Error.WriteLine("初期ポイント設定エラー");
            }
        }

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
                switch (act.State)
                {
                    case (Action.O1):
                        target.O1++;
                        target.AdditionalPoint += oPoint;
                        break;
                    case (Action.O2):
                        target.O2++;
                        target.AdditionalPoint += oPoint;
                        break;
                    case (Action.X1):
                        target.X1++;
                        target.AdditionalPoint -= penalty;
                        break;
                    case (Action.X2):
                        target.X2++;
                        target.AdditionalPoint -= penalty;
                        break;
                    case (Action.B1):
                        target.B1++;
                        target.AdditionalPoint += bPoint;
                        break;
                    case (Action.B2):
                        target.B2++;
                        target.AdditionalPoint += bPoint;
                        break;
                    default:
                        break;
                }
                target.Point = target.InitialPoint + target.AdditionalPoint;
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
