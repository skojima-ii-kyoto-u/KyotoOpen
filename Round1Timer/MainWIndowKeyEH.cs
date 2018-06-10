using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Round1Timer
{
    public partial class MainWindow : Window
    {
        string explanation = "上下:時間セット Enter:タイマースタート T:一時停止 Esc:ログに結果を追加して終了";

        int minute = 15;
        int second = 0;
        bool selectingMinute = true;
        
        public void WindowKeyDownHandler(object sender, KeyEventArgs e)
        {
            int result;
            switch (e.Key)
            {
                case Key.Escape:
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
                case Key.Up:
                    if(selectingMinute)
                        minute = (minute + 1) % 60;
                    else
                        second = (second + 1) % 60;
                    mainWindowVM.Time = string.Format(
                        "{0}:{1}", minute.ToString("00"), second.ToString("00"));
                    break;
                case Key.Down:
                    if (selectingMinute)
                        minute = (minute + 59) % 60;
                    else
                        second = (second + 59) % 60;
                    mainWindowVM.Time = string.Format(
                        "{0}:{1}", minute.ToString("00"), second.ToString("00"));
                    break;
                case Key.Right:
                case Key.Left:
                    selectingMinute = !selectingMinute;
                    break;
                case Key.Enter:
                    timer.StartTimer(minute, second);
                    break;
                case Key.T:
                    timer.Switch();
                    break;

                default:
                    break;
            }

            this.Focus();
        }
    }
}
