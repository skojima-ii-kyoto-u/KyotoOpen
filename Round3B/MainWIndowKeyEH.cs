using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Round3B.Logic;

namespace Round3B
{
    public partial class MainWindow : Window
    {
        int selecting = 0;
        bool select1 = true;

        string explanation = "上下キー:解答者選択 O:正解 X:誤答 T:スルー U:元に戻す 数字キー:連想クイズの点数を入力（Backspaceで1字消す） Enter:連想クイズの点数を反映 Esc:ログに結果を追加して終了";
        
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
                case Key.O:
                    result = rule.AddAction(
                        new Logic.Action(
                            plist[selecting], 
                            select1 ? Logic.Action.O1 : Logic.Action.O2));
                    if(result == 0)
                    {
                        rule.Next();

                        writer.AddLog(string.Format(
                            "{0}\t{1}\t{2}",
                            plist[selecting].ToString(),
                            select1 ? "O1" : "O2",
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
                case Key.Enter:
                    Logic.Action act = new Logic.Action(plist[selecting], Logic.Action.A);
                    {
                        int value;
                        if(int.TryParse(BonusValue.Text, out value))
                        {
                            act.Point = value;
                        }
                        else
                        {
                            act.Point = 0;
                        }
                        BonusValue.Text = "";
                    }
                    result = rule.AddAction(act);
                    if (result == 0)
                    {
                        rule.Next();

                        writer.AddLog(string.Format(
                            "{0}\t{1}\t{2}",
                            plist[selecting].ToString(),
                            "Bonus" + act.Point.ToString(),
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
                case Key.Back:
                    {
                        int value;
                        if(int.TryParse(BonusValue.Text, out value))
                        {
                            BonusValue.Text = (value / 10).ToString();
                        }
                        else
                        {
                            BonusValue.Text = "";
                        }
                    }
                    break;
                case Key.D0:
                case Key.NumPad0:
                    updateBonusValue(0);
                    break;
                case Key.D1:
                case Key.NumPad1:
                    updateBonusValue(1);
                    break;
                case Key.D2:
                case Key.NumPad2:
                    updateBonusValue(2);
                    break;
                case Key.D3:
                case Key.NumPad3:
                    updateBonusValue(3);
                    break;
                case Key.D4:
                case Key.NumPad4:
                    updateBonusValue(4);
                    break;
                case Key.D5:
                case Key.NumPad5:
                    updateBonusValue(5);
                    break;
                case Key.D6:
                case Key.NumPad6:
                    updateBonusValue(6);
                    break;
                case Key.D7:
                case Key.NumPad7:
                    updateBonusValue(7);
                    break;
                case Key.D8:
                case Key.NumPad8:
                    updateBonusValue(8);
                    break;
                case Key.D9:
                case Key.NumPad9:
                    updateBonusValue(9);
                    break;

                default:
                    break;
            }

            this.Focus();
        }

        private void updateBonusValue(int n)
        {
            int value;
            if(int.TryParse(BonusValue.Text, out value))
            {
                BonusValue.Text = (value * 10 + n).ToString();
            }
            else
            {
                BonusValue.Text = n.ToString();
            }
        }
    }
}
