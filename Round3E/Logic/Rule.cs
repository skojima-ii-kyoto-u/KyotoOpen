using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointViewerUtility;

namespace Round3E.Logic
{
    public class Rule : RuleBase
    {
        private const int addPoint = 1;
        private const int sleep = 3;

        public Boad Boad
        {
            get
            {
                return thisfield.FieldBoad;
            }
        }
        static private field thisfield = new field();

        private Dictionary<string, int> playerIndex = new Dictionary<string, int>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pl"></param>
        /// <param name="outpath"></param>
        public Rule(Player[] pl, string outpath) : base(thisfield, pl, outpath, false)
        {
            for(int i = 0; i < pl.Length; i++)
            {
                playerIndex.Add(pl[i].MainKey(), i + 1);
            }
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


            Player target = (Player)UtilityMethod.Target(plist, acts[0].Player);
            Action act = (Action)acts[0];

            switch (act.State)
            {
                case (Action.O1):
                    target.O1++;
                    ((field)f).FieldBoad.ChooseLine(act.Line, playerIndex[target.MainKey()]);
                    {
                        var pointTable = ((field)f).FieldBoad.BoadPointTable();
                        for(int i = 0; i < plist.Length; i++)
                        {
                            int value;
                            var p = ((Player)plist[i]);
                            if (pointTable.TryGetValue(i + 1, out value))
                            {
                                p.FieldPoint = value;
                            }
                            else
                            {
                                p.FieldPoint = 0;
                            }

                            p.Point = p.O + p.FieldPoint;
                        }
                    }
                    break;
                case (Action.O2):
                    target.O2++;
                    ((field)f).FieldBoad.ChooseLine(act.Line, playerIndex[target.MainKey()]);
                    {
                        var pointTable = ((field)f).FieldBoad.BoadPointTable();
                        for (int i = 0; i < plist.Length; i++)
                        {
                            int value;
                            var p = ((Player)plist[i]);
                            if (pointTable.TryGetValue(i + 1, out value))
                            {
                                p.FieldPoint = value;
                            }
                            else
                            {
                                p.FieldPoint = 0;
                            }

                            p.Point = p.O + p.FieldPoint;
                        }
                    }
                    break;
                case (Action.X1):
                    target.X1++;
                    target.SleepCount = sleep;
                    break;
                case (Action.X2):
                    target.X2++;
                    target.SleepCount = sleep;
                    break;
                default:
                    break;
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
            public Boad FieldBoad;

            public field()
            {
                FieldBoad = new Logic.Boad();
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
                this.FieldBoad = original.FieldBoad.Clone();

                return this;
            }
        }
    }
}
