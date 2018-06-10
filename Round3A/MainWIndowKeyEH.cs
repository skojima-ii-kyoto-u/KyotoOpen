using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Round3A.Logic;

namespace Round3A
{
    public partial class MainWindow : Window
    {
        int selecting = 0;
        bool select1 = true;

        string explanation = "上下キー:解答者選択 O:正解 X:誤答 T:スルー U:元に戻す 1～6:関数選択 Esc:ログに結果を追加して終了";
        
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
                        new Round3A.Logic.Action(
                            plist[selecting], 
                            select1 ? Round3A.Logic.Action.O1 : Round3A.Logic.Action.O2));
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
                        new Round3A.Logic.Action(
                            plist[selecting],
                            select1 ? Round3A.Logic.Action.X1 : Round3A.Logic.Action.X2));
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
                        playerVM[i].RaiseFunctionChanged();
                    }

                    writer.AddLog("** Undo **");

                    break;
                case Key.D1:
                    setFunction(1);
                    break;
                case Key.NumPad1:
                    setFunction(1);
                    break;
                case Key.D2:
                    setFunction(2);
                    break;
                case Key.NumPad2:
                    setFunction(2);
                    break;
                case Key.D3:
                    setFunction(3);
                    break;
                case Key.NumPad3:
                    setFunction(3);
                    break;
                case Key.D4:
                    setFunction(4);
                    break;
                case Key.NumPad4:
                    setFunction(4);
                    break;
                case Key.D5:
                    setFunction(5);
                    break;
                case Key.NumPad5:
                    setFunction(5);
                    break;
                case Key.D6:
                    setFunction(6);
                    break;
                case Key.NumPad6:
                    setFunction(6);
                    break;
                default:
                    break;
            }

            this.Focus();
        }

        private void setFunction(int funcNum)
        {
            Logic.Action a = new Logic.Action(plist[selecting], Logic.Action.FuncSet);
            a.FuncNum = funcNum;

            if (rule.AddAction(a) == 0)
            {
                rule.Next();
                playerVM[selecting].RaiseFunctionChanged();

                writer.AddLog(string.Format(
                    "{0}\t{1}\t{2}",
                    plist[selecting].ToString(),
                    "FuncSet",
                    funcNum));

                for (int i = 0; i < plist.Length; i++)
                {
                    playerVM[i].Player = plist[i];
                }
            }
            else
            {
                Console.WriteLine("Informal input");
            }
        }
    }
}
