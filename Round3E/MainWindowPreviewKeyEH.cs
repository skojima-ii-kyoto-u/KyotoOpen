using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Round3E
{
    public partial class MainWindow : Window
    {
        string explanation2 = "Shift+左右：線の選択 Shift+O：正解、線を追加";

        public void WindowPreviewKeyDownHandler(object sender, KeyEventArgs e)
        {
            if(Keyboard.Modifiers == ModifierKeys.Shift)
            {
                switch (e.Key)
                {
                    case Key.LeftShift:
                    case Key.RightShift:
                        showWindowBoadPanel.SwitchOpacity(450);
                        break;
                    case Key.Right:
                        this.Message.Text = showWindowBoadPanel.SelectLineForward(rule.Boad);
                        break;
                    case Key.Left:
                        this.Message.Text = showWindowBoadPanel.SelectLineBack(rule.Boad);
                        break;
                    case Key.O:
                        Logic.Action act = new Logic.Action(
                            plist[selecting],
                            select1 ? Logic.Action.O1 : Logic.Action.O2);
                        act.Line = showWindowBoadPanel.Selecting;
                        int result = rule.AddAction(act);
                        if (result == 0)
                        {
                            rule.Next();

                            writer.AddLog(string.Format(
                                "{0}\t{1}\tLine{2}\t{3}pts",
                                plist[selecting].ToString(),
                                select1 ? "O1" : "O2",
                                act.Line,
                                plist[selecting].PointToString()));

                            for (int i = 0; i < plist.Length; i++)
                            {
                                playerVM[i].Player = plist[i];
                            }
                            showWindowBoadPanel.ColorChange(rule.Boad);
                        }
                        else
                        {
                            Console.WriteLine("Informal input");
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void WindowPreviewKeyUpHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                showWindowBoadPanel.SwitchOpacity(450);
            }
        }
    }
}
