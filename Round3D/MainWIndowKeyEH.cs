using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Round3D.Logic;

namespace Round3D
{
    public partial class MainWindow : Window
    {
        int selecting = 0;
        bool select1 = true;

        string explanation = "上下キー:解答者選択 A:攻撃 H:LP+2 R:AP+1 C:役職入れ替え X:誤答 T:スルー U:元に戻す Esc:ログに結果を追加して終了";
        
        public void WindowKeyDownHandler(object sender, KeyEventArgs e)
        {
            int result;
            switch (e.Key)
            {
                case Key.Escape:
                    {
                        string finallog = "End\n";
                        foreach(Player p in plist)
                        {
                            finallog += string.Format(
                                "Rank:{0}({1})\t{2}\n",
                                p.Rank, p.PointToString(), p.ToString());
                        }
                        writer.AddLog(finallog);
                    }
                    showWindow.Close();
                    this.Close();
                    break;
                case Key.M:
                    if (showWindow.WindowState == WindowState.Maximized)
                    {
                        showWindow.WindowState = WindowState.Normal;
                        showWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                        showWindow.ShowWindowPanel.RenderTransform = new ScaleTransform(1, 1);
                    }
                    else
                    {
                        double beforeWidth = showWindow.ShowWindowPanel.Width;
                        double beforeHeight = showWindow.ShowWindowPanel.Height;
                        showWindow.SizeToContent = SizeToContent.Manual;
                        showWindow.WindowState = WindowState.Maximized;
                        showWindow.WindowStyle = WindowStyle.None;
                        
                        showWindow.ShowWindowPanel.RenderTransform = new ScaleTransform
                            (showWindow.Width / beforeWidth,
                            showWindow.Height / beforeHeight);
                    }
                    break;
                case Key.Down:
                    if (select1)
                    {
                        mainWindowVM.Switch();
                        select1 = false;
                    }
                    else
                    {
                        playerSVM[selecting].Color = new SolidColorBrush(Colors.Gray);
                        selecting = (selecting + 1) % plist.Length;
                        playerSVM[selecting].Color = new SolidColorBrush(Colors.Black);

                        mainWindowVM.Player1 = plist[selecting].Player1;
                        mainWindowVM.Player2 = plist[selecting].Player2;

                        mainWindowVM.Switch();
                        select1 = true;
                    }
                    break;
                case Key.Up:
                    if (!select1)
                    {
                        mainWindowVM.Switch();
                        select1 = true;
                    }
                    else
                    {
                        playerSVM[selecting].Color = new SolidColorBrush(Colors.Gray);
                        selecting = (selecting + plist.Length - 1) % plist.Length;
                        playerSVM[selecting].Color = new SolidColorBrush(Colors.Black);

                        mainWindowVM.Player1 = plist[selecting].Player1;
                        mainWindowVM.Player2 = plist[selecting].Player2;

                        mainWindowVM.Switch();
                        select1 = false;
                    }
                    break;
                case Key.A:
                    if((select1 && plist[selecting].IsP1Attacker)||
                        (!select1 && !plist[selecting].IsP1Attacker))
                    {
                        Logic.Action act = new Logic.Action(
                            plist[selecting],
                            select1 ? Logic.Action.O1 : Logic.Action.O2,
                            Logic.Action.Attack);
                        result = rule.AddAction(act);
                    }
                    else
                    {
                        result = -2;
                    }

                    if(result == 0)
                    {
                        rule.Next();

                        writer.AddLog(string.Format(
                            "{0}\t{1}\t{2}",
                            plist[selecting].ToString(),
                            "Attack",
                            plist[selecting].PointToString()));

                        for (int i = 0; i < plist.Length; i++)
                        {
                            playerVM[i].Player = plist[i];
                        }
                    }
                    else
                    {
                        Console.WriteLine("Informal input");
                    }
                    break;
                case Key.R:
                    if ((select1 && !plist[selecting].IsP1Attacker) ||
                        (!select1 && plist[selecting].IsP1Attacker))
                    {
                        Logic.Action act = new Logic.Action(
                            plist[selecting],
                            select1 ? Logic.Action.O1 : Logic.Action.O2,
                            Logic.Action.Reinforce);
                        result = rule.AddAction(act);
                    }
                    else
                    {
                        result = -2;
                    }
                    if (result == 0)
                    {
                        rule.Next();

                        writer.AddLog(string.Format(
                            "{0}\t{1}\t{2}",
                            plist[selecting].ToString(),
                            "Reinforce",
                            plist[selecting].PointToString()));

                        for (int i = 0; i < plist.Length; i++)
                        {
                            playerVM[i].Player = plist[i];
                        }
                    }
                    else
                    {
                        Console.WriteLine("Informal input");
                    }
                    break;
                case Key.H:
                    if ((select1 && !plist[selecting].IsP1Attacker) ||
                        (!select1 && plist[selecting].IsP1Attacker))
                    {
                        Logic.Action act = new Logic.Action(
                            plist[selecting],
                            select1 ? Logic.Action.O1 : Logic.Action.O2,
                            Logic.Action.Heal);
                        result = rule.AddAction(act);
                    }
                    else
                    {
                        result = -2;
                    }
                    if (result == 0)
                    {
                        rule.Next();

                        writer.AddLog(string.Format(
                            "{0}\t{1}\t{2}",
                            plist[selecting].ToString(),
                            "Heal",
                            plist[selecting].PointToString()));

                        for (int i = 0; i < plist.Length; i++)
                        {
                            playerVM[i].Player = plist[i];
                        }
                    }
                    else
                    {
                        Console.WriteLine("Informal input");
                    }
                    break;
                case Key.X:
                    result = rule.AddAction(
                        new Logic.Action(
                            plist[selecting],
                            select1 ? Logic.Action.X1 : Logic.Action.X2));
                    if (result == 0)
                    {
                        rule.Next();

                        writer.AddLog(string.Format(
                            "{0}\t{1}\t{2}",
                            plist[selecting].ToString(),
                            select1 ? "X1" : "X2",
                            plist[selecting].PointToString()));

                        for (int i = 0; i < plist.Length; i++)
                        {
                            playerVM[i].Player = plist[i];
                        }
                    }
                    else
                    {
                        Console.WriteLine("Informal input");
                    }
                    break;
                case Key.C:
                    result = rule.AddAction(
                        new Logic.Action(
                            plist[selecting],
                            Logic.Action.Switch));
                    if (result == 0)
                    {
                        rule.Next();

                        writer.AddLog(string.Format(
                            "{0}\t{1}",
                            plist[selecting].ToString(),
                            "SwitchJob"));

                        for (int i = 0; i < plist.Length; i++)
                        {
                            playerVM[i].Player = plist[i];
                        }
                    }
                    else
                    {
                        Console.WriteLine("Informal input");
                    }
                    break;
                case Key.T:
                    rule.Next();

                    writer.AddLog("スルー");

                    for (int i = 0; i < plist.Length; i++)
                    {
                        playerVM[i].Player = plist[i];
                    }

                    break;
                case Key.U:
                    PointViewerUtility.PlayerBase[] newplist = rule.Undo();
                    for (int i = 0; i < plist.Length; i++)
                    {
                        plist[i] = playerVM[i].Player = (Player)newplist[i];
                    }

                    writer.AddLog("** Undo **");

                    break;

                default:
                    break;
            }

            this.Focus();
        }
    }
}
