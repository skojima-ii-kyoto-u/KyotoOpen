using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointViewerUtility;

namespace Round2
{
    public class Rule : RuleBase
    {
        private const int winPoint = 5;
        private const int losePoint = 2;

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
            if(acts.Count == 1 && f is field)
            {
                Player target = (Player)UtilityMethod.Target(plist, acts[0].Player);
                Action act = (Action)acts[0];

                switch (act.State)
                {
                    case (Action.O1):
                        target.O1++;
                        if (target.Standup2)
                        {
                            target.Standup2 = false;
                            target.BonusAdded = true;
                        }
                        if (!target.BonusAdded && !target.Standup1){
                            target.Standup1 = true;
                        }

                        target.Point = target.O1 + target.O2;
                        if (target.BonusAdded)
                            target.Point++;

                        break;
                    case (Action.O2):
                        target.O2++;
                        if (target.Standup1)
                        {
                            target.Standup1 = false;
                            target.BonusAdded = true;
                        }
                        if (!target.BonusAdded && !target.Standup2)
                        {
                            target.Standup2 = true;
                        }

                        target.Point = target.O1 + target.O2;
                        if (target.BonusAdded)
                            target.Point++;

                        break;
                    case (Action.X1):
                        target.X1++;
                        break;
                    case (Action.X2):
                        target.X2++;
                        break;
                    default:
                        break;
                }
            }
            else if(acts.Count > 1)
            {
                foreach(Action act in acts)
                {
                    Player p = (Player)PointViewerUtility.UtilityMethod.Target(plist, act.Player);
                    switch (act.State)
                    {
                        case Action.SET4o2x:
                            p.WinPoint = winPoint - 1;
                            p.LoseX = losePoint;
                            break;
                        case Action.SET4o3x:
                            p.WinPoint = winPoint - 1;
                            p.LoseX = losePoint + 1;
                            break;
                        case Action.SET5o2x:
                            p.WinPoint = winPoint;
                            p.LoseX = losePoint;
                            break;
                        case Action.SET5o3x:
                            p.WinPoint = winPoint;
                            p.LoseX = losePoint + 1;
                            break;
                        default:
                            p.WinPoint = winPoint;
                            p.LoseX = losePoint;
                            break;
                    }
                }
            }
        }

        public void SetNorma(Player[] pl, List<int> norma)
        {
            for (int i = 0; i < pl.Length; i++)
            {
                if (norma.Count > 0)
                {
                    switch (norma[0])
                    {
                        case 0:
                            AddAction(new Action(pl[i], Action.SET5o2x));
                            break;
                        case 1:
                            AddAction(new Action(pl[i], Action.SET4o2x));
                            break;
                        case 2:
                            AddAction(new Action(pl[i], Action.SET5o3x));
                            break;
                        case 3:
                            AddAction(new Action(pl[i], Action.SET4o3x));
                            break;
                        default:
                            AddAction(new Action(pl[i], Action.SET5o2x));
                            break;
                    }
                    norma.RemoveAt(0);
                }
                else
                {
                    AddAction(new Action(pl[i], Action.SET5o2x));
                }
            }

            Next();
        }

        public override bool Judge_win(PlayerBase player, FieldBase field)
        {
            return player.Point >= ((Player)player).WinPoint;
        }

        public override bool Judge_lose(PlayerBase player, FieldBase field)
        {
            return (((Player)player).X1 + ((Player)player).X2) >= ((Player)player).LoseX;
        }

        public override double EvaluateValue(PlayerBase player)
        {
            return player.Point * 0.1;
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
