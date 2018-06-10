using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointViewerUtility;

namespace Round3D.Logic
{
    public class Rule : RuleBase
    {
        private const int defaultAP = 3;

        private const int healLP = 2;
        private const int reinforceAP = 1;
        private const int penaltyLP = 2;

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
            if(!(f is field))
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
                    if (target.IsP1Attacker)
                    {
                        if(act.Kind == Action.Attack)
                        {
                            foreach (Player p in plist)
                            {
                                if (target != p)
                                    p.Point -= target.AP;
                            }
                        }
                    }
                    else
                    {
                        if(act.Kind == Action.Heal)
                        {
                            target.Point += healLP;
                        }
                        else if(act.Kind == Action.Reinforce)
                        {
                            target.AP += reinforceAP;
                        }
                    }
                    break;
                case (Action.O2):
                    target.O2++;
                    if (!target.IsP1Attacker)
                    {
                        if (act.Kind == Action.Attack)
                        {
                            foreach (Player p in plist)
                            {
                                if (target != p)
                                    p.Point -= target.AP;
                            }
                        }
                    }
                    else
                    {
                        if (act.Kind == Action.Heal)
                        {
                            target.Point += healLP;
                        }
                        else if (act.Kind == Action.Reinforce)
                        {
                            target.AP += reinforceAP;
                        }
                    }
                    break;
                case (Action.X1):
                    target.X1++;
                    if(target.AP == defaultAP)
                    {
                        target.Point -= penaltyLP;
                    }
                    else
                    {
                        target.AP = defaultAP;
                    }
                    break;
                case (Action.X2):
                    target.X2++;
                    if (target.AP == defaultAP)
                    {
                        target.Point -= penaltyLP;
                    }
                    else
                    {
                        target.AP = defaultAP;
                    }
                    break;
                case (Action.Switch):
                    target.SwitchAttacker();
                    target.SleepCount = 1;
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
            return player.Point <= 0;
        }

        public override double EvaluateValue(PlayerBase player)
        {
            return player.Point * 0.1 + ((Player)player).AP * 0.01 + (99 - player.X) * 0.0001;
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
