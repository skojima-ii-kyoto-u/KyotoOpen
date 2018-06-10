using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Round2
{
    public partial class MainWindow : Window
    {
        string explanation = "上下キー:解答者選択 O:正解 X:誤答 T:スルー U:元に戻す S:ノルマ設定画面表示 Esc:ログに結果を追加して終了";

        int selecting = 0;
        bool select1 = true;
        public void WindowKeyDownHandler(object sender, KeyEventArgs e)
        {
            int result;
            bool norma_setting = false;
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
                    normaSetWindow.Close();
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
                        new Round2.Action(
                            plist[selecting], 
                            select1 ? Round2.Action.O1 : Round2.Action.O2));
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
                        new Round2.Action(
                            plist[selecting],
                            select1 ? Round2.Action.X1 : Round2.Action.X2));
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

                    break;
                case Key.U:
                    PointViewerUtility.PlayerBase[] newplist = rule.Undo();
                    for (int i = 0; i < plist.Length; i++)
                    {
                        plist[i] = playerVM[i].Player = (Player)newplist[i];
                        playerVM[i].RaiseNormaChanged();
                    }

                    writer.AddLog("** Undo **");

                    break;
                case Key.S:
                    normaSetWindow.OriginalShow(plist);
                    norma_setting = true;
                    break;
                default:
                    break;
            }
            if(!norma_setting)
                this.Focus();

            norma_setting = false;
        }
    }
}
