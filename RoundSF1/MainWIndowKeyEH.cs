using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using RoundSF1.Logic;

namespace RoundSF1
{
    public partial class MainWindow : Window
    {
        int selecting = 0;
        bool select1 = true;

        string explanation = "上下キー:解答者選択 数字:ポイントを入力 U:元に戻す Esc:ログに結果を追加して終了";
        
        public void WindowKeyDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    {
                        string finallog = "End\n";
                        foreach(Player p in plist)
                        {
                            finallog += string.Format(
                                "Rank:{0}\t{1}P\t{2}\n",
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
                case Key.U:
                    PointViewerUtility.PlayerBase[] newplist = rule.Undo();
                    for (int i = 0; i < plist.Length; i++)
                    {
                        plist[i] = playerVM[i].Player = (Player)newplist[i];
                    }

                    writer.AddLog("** Undo **");

                    break;
                case Key.D0:
                    setPoint(0);
                    break;
                case Key.NumPad0:
                    setPoint(0);
                    break;
                case Key.D1:
                    setPoint(1);
                    break;
                case Key.NumPad1:
                    setPoint(1);
                    break;
                case Key.D2:
                    setPoint(2);
                    break;
                case Key.NumPad2:
                    setPoint(2);
                    break;
                case Key.D3:
                    setPoint(3);
                    break;
                case Key.NumPad3:
                    setPoint(3);
                    break;
                case Key.D4:
                    setPoint(4);
                    break;
                case Key.NumPad4:
                    setPoint(4);
                    break;
                case Key.D5:
                    setPoint(5);
                    break;
                case Key.NumPad5:
                    setPoint(5);
                    break;
                case Key.D6:
                    setPoint(6);
                    break;
                case Key.NumPad6:
                    setPoint(6);
                    break;
                case Key.D7:
                    setPoint(7);
                    break;
                case Key.NumPad7:
                    setPoint(7);
                    break;
                case Key.D8:
                    setPoint(8);
                    break;
                case Key.NumPad8:
                    setPoint(8);
                    break;
                case Key.D9:
                    setPoint(9);
                    break;
                case Key.NumPad9:
                    setPoint(9);
                    break;
                default:
                    break;
            }

            this.Focus();
        }
        
        private void setPoint(int point)
        {
            Logic.Action a = new Logic.Action(plist[selecting], Logic.Action.P);
            a.Point = plist[selecting].Point * 10 + point;

            if (rule.AddAction(a) == 0)
            {
                rule.Next();
                
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
