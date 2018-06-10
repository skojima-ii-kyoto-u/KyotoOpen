using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Round3Select
{
    public partial class MainWindow : Window
    {
        private class log
        {
            public Player Team;
            public CourseViewModel Course;

            public log(Player p, CourseViewModel c)
            {
                Team = p;
                Course = c;
            }
        }

        private List<log> loglist = new List<log>();

        public void WindowKeyDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    for(int i = 0; i < courseVM.Count; i++)
                    {
                        IO.FileWriter.WritePlayerList(
                            courseVM[i].CourseTeam,
                            string.Format("player_{0}.txt", (i + 1)));
                    }
                    this.Close();
                    break;
                case Key.M:
                    if (this.WindowState == WindowState.Maximized)
                    {
                        this.WindowState = WindowState.Normal;
                        this.WindowStyle = WindowStyle.SingleBorderWindow;
                        this.MainPanel.RenderTransform = new ScaleTransform(1, 1);
                    }
                    else
                    {
                        double beforeWidth = this.MainPanel.Width;
                        double beforeHeight = this.MainPanel.Height;
                        this.SizeToContent = SizeToContent.Manual;
                        this.WindowState = WindowState.Maximized;
                        this.WindowStyle = WindowStyle.None;
                        
                        this.MainPanel.RenderTransform = new ScaleTransform
                            (this.Width / beforeWidth,
                            this.Height / beforeHeight);
                    }
                    break;
                case Key.U:
                    if(loglist.Count > 0)
                    {
                        loglist[0].Course.Undo();
                        nextTeamsVM.Undo();
                        loglist.RemoveAt(0);
                    }
                    break;
                case Key.D1:
                    AddTeam(0);
                    break;
                case Key.NumPad1:
                    AddTeam(0);
                    break;
                case Key.D2:
                    AddTeam(1);
                    break;
                case Key.NumPad2:
                    AddTeam(1);
                    break;
                case Key.D3:
                    AddTeam(2);
                    break;
                case Key.NumPad3:
                    AddTeam(2);
                    break;
                case Key.D4:
                    AddTeam(3);
                    break;
                case Key.NumPad4:
                    AddTeam(3);
                    break;
                case Key.D5:
                    AddTeam(4);
                    break;
                case Key.NumPad5:
                    AddTeam(4);
                    break;
                default:
                    break;
            }
        }

        private void AddTeam(int courseidx)
        {
            if (!courseVM[courseidx].IsFull())
            {
                Player team = nextTeamsVM.Dequeue();
                courseVM[courseidx].AddTeam(team);

                loglist.Insert(0, new log(team, courseVM[courseidx]));
            }
                
        }
    }
}
