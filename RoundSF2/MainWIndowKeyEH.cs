using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using RoundSF2.Logic;

namespace RoundSF2
{
    public partial class MainWindow : Window
    {
        int selecting = 0;
        bool select1 = true;

        string explanation = "上下キー:解答者選択 O:ボタン点灯（正誤入力に移行）/正解 X:誤答 T:スルー（正誤入力に移行） U:元に戻す Esc:ログに結果を追加して終了";

        bool buttonPushed = false;
        int buttonPlayer = -1;
        List<Logic.Action> actionList = new List<Logic.Action>();

        public void WindowKeyDownHandler(object sender, KeyEventArgs e)
        {
            int result = 0;
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
                    if (!buttonPushed)
                    {
                        buttonPlayer = selecting * 2 + (select1 ? 0 : 1);
                        buttonPushed = true;

                        Message.Text = "正誤入力中:\n";
                    }
                    else
                    {
                        int actionState;
                        if(buttonPushed && buttonPlayer == (selecting * 2 + (select1 ? 0 : 1)))
                        {
                            actionState = select1 ? Logic.Action.B1 : Logic.Action.B2;
                        }
                        else
                        {
                            actionState = select1 ? Logic.Action.O1 : Logic.Action.O2;
                        }
                        Logic.Action act = new Logic.Action(
                            plist[selecting],
                            actionState);
                        result += rule.AddAction(act);

                        actionList.Add(act);
                        Message.Text += act.ToString() + "\n";
                    }
                    break;
                case Key.X:
                    if (buttonPushed && buttonPlayer == (selecting * 2 + (select1 ? 0 : 1)))
                    {
                        Logic.Action act = new Logic.Action(
                            plist[selecting],
                            select1 ? Logic.Action.X1 : Logic.Action.X2);

                        result += rule.AddAction(act);

                        actionList.Add(act);
                        Message.Text += act.ToString() + "\n";
                    }
                    break;
                case Key.T:
                    if (!buttonPushed)
                    {
                        buttonPlayer = -1;
                        buttonPushed = true;

                        Message.Text = "正誤入力中:\n";
                    }
                    break;
                case Key.Enter:
                    if(result == 0)
                    {
                        rule.Next();

                        string log = "{\n";
                        while(actionList.Count > 0)
                        {
                            var act = actionList[0];
                            actionList.RemoveAt(0);

                            log += act.ToString() + "\n";
                        }
                        log += "}";

                        writer.AddLog(log);

                        for (int i = 0; i < plist.Length; i++)
                        {
                            playerVM[i].Player = plist[i];
                        }

                        buttonPushed = false;
                        buttonPlayer = -1;

                        Message.Text = "";
                    }
                    else
                    {
                        Console.WriteLine("Informal input");
                    }
                    break;
                case Key.U:
                    if (buttonPushed)
                    {
                        rule.Next();

                        buttonPushed = false;
                        buttonPlayer = -1;
                        actionList.RemoveAll(x => true);

                        Message.Text = "";

                        writer.AddLog("正誤入力リセット");
                    }

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
